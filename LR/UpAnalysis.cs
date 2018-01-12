using System;
using System.Collections.Generic;

namespace LR
{
    public class UpAnalysis
    {
        public UpAnalysis(MyRule[] ruleses, MyWord[] words, int[,] ruleTable, int countOfRules)
        {
            Rules = ruleses;
            Words = words;
            RuleRuleTable = ruleTable;
            CountOfRules = countOfRules;
        }

        /// <summary>
        /// Делегат, для методов информирования
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        public delegate void PrintDelegate(string text);

        public event PrintDelegate PrintCompileInfo = (str) => { };
        public event PrintDelegate PrintCompileResult = (str) => { };
        public event PrintDelegate PrintMessage = (str) => { };

        /// <summary>
        /// Коллекция правил, для компиляции
        /// </summary>
        public MyRule[] Rules;

        /// <summary>
        /// Коллекция символов грамматики, для компиляции
        /// </summary>
        public MyWord[] Words;

        /// <summary>
        /// Управляющая таблица восходящего разбора
        /// </summary>
        public int[,] RuleRuleTable;

        /// <summary>
        /// Количество правил
        /// </summary>
        public int CountOfRules;

        public MyWord[] ArrS = new MyWord[1000];

        public void PrintInfo(int zz, List<int> rulesFounded, Queue<MyWord> splittedWords)
        {
            string s1 = "";
            foreach (var word in splittedWords)
            {
                s1 += Words[word.Number].Value + " ";
            }
            PrintCompileInfo.Invoke(@"Строка:" + s1 + '\n');
            string s2 = "";
            int i2 = 0;
            while (i2 <= zz)
            {
                s2 = s2 + Words[ArrS[i2].Number].Value + " ";
                i2 = i2 + 1;
            }
            PrintCompileInfo.Invoke(@"Магазин:" + s2 + '\n');
            string s3 = "";

            foreach (var rule in rulesFounded)
            {
                s3 = s3 + (rule + 1) + " ";
            }
            PrintCompileInfo.Invoke(@"Правила:" + s3 + '\n');
            PrintCompileInfo.Invoke(@"      " + '\n');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruleNumber">Номер правила</param>
        /// <param name="ts1"></param>
        public void MyCompil(int ruleNumber, int ts1)
        {
            ruleNumber = ruleNumber + 1;
            if (ruleNumber == 1)
                ArrS[ts1 - 2].T = $"{ArrS[ts1 - 1].Value} ( {ArrS[ts1 - 2].T} ) {{{ArrS[ts1].T};}}";
            if (ruleNumber == 2)
                ArrS[ts1 - 3].T =
                    $"{ArrS[ts1 - 2].Value} ( {ArrS[ts1 - 3].T} ) {{{ArrS[ts1 - 1].T};}}{Environment.NewLine}{ArrS[ts1].T}";
            if (ruleNumber == 4)
                ArrS[ts1 - 1].T = $"{ArrS[ts1 - 1].T} {ArrS[ts1].T}";
            if (ruleNumber == 3)
                ArrS[ts1 - 1].T = $"{ArrS[ts1 - 1].T} {ArrS[ts1].T}";
            if (ruleNumber == 6)
                //ArrS[ts1 - 1].T = $"{ArrS[ts1 - 1].Value} {ArrS[ts1].T}";
                ArrS[ts1 - 1].T = $"{ArrS[ts1].Value} =";
            if (ruleNumber == 8)
                ArrS[ts1].T = ArrS[ts1].Value;
            if (ruleNumber == 7)
                ArrS[ts1].T = ArrS[ts1].Value;
            if (ruleNumber == 11)
                ArrS[ts1 - 1].T = "! ( " + ArrS[ts1].T + " )";
            if (ruleNumber == 10)
                ArrS[ts1 - 1].T = "sqrt ( " + ArrS[ts1].T + " )";
            if (ruleNumber == 12)
                ArrS[ts1 - 1].T = ArrS[ts1].T + " == ";
            if (ruleNumber == 9)
                ArrS[ts1].T = $"{ArrS[ts1].Value}";
            if (ruleNumber == 15)
                ArrS[ts1 - 1].T = $"{ArrS[ts1].T} == ";
            if (ruleNumber == 16)
                ArrS[ts1 - 1].T = ArrS[ts1].T + " ^ ";
            if (ruleNumber == 13)
                ArrS[ts1 - 1].T = ArrS[ts1].T + " * ";
            if (ruleNumber == 14)
                ArrS[ts1 - 1].T = ArrS[ts1].T + " / ";

        }

        public void Algorithm(Queue<MyWord> splittedWords)
        {
            // Коллекций номеров правил
            List<int> rulesFounded = new List<int>();
            int currentOut = 0;
            ArrS[currentOut].Number = 19;
            ArrS[currentOut].Value = "$";
            PrintInfo(currentOut, rulesFounded, splittedWords);
            while (splittedWords.Count > 0)
            {
                CompileActions action = CompileActions.Start;
                MyWord word = splittedWords.Peek();
                if (word.Number == 19)
                {
                    if (currentOut == 1)
                    {
                        // Сначала символ в конце, потом в начале
                        // Смотрится, что бы первым был символ $ а потом цепочка S
                        // Если условие выполнено, то это конец разбора
                        if ((ArrS[currentOut].Number == 0) && (ArrS[currentOut - 1].Number == 19))
                        {
                            //PrintInfo(currentOut, rulesFounded, splittedWords);
                            return;
                        }
                    }
                }

                int row = ArrS[currentOut].Number;
                int col = word.Number;

                if ((RuleRuleTable[row, col] == 1 && action != CompileActions.Error) ||
                    (RuleRuleTable[row, col] == 2 && action != CompileActions.Error))
                {
                    currentOut++;
                    MyWord tmpWord = splittedWords.Dequeue();
                    ArrS[currentOut] = tmpWord;
                    //PrintInfo(currentOut, rulesFounded, splittedWords);
                    action = CompileActions.Next;
                }
                if ((RuleRuleTable[row, col] == 3) && action != CompileActions.Next && action != CompileActions.Error)
                {
                    // Количество символов, для свертки, ищем правило, где 2 символа грамматики в нем
                    int countOfWords = 0;
                    // Ищется правило для 2 последних символов в выходной цепочке
                    while (RuleRuleTable[ArrS[currentOut - countOfWords].Number,
                               ArrS[currentOut - countOfWords + 1].Number] != 1)
                    {
                        countOfWords++;
                    }
                    //TODO: Количество правил
                    for (var ruleNumber = 0; ruleNumber < Rules.Length; ruleNumber++)
                    {
                        if (Rules[ruleNumber].CountOfWords == countOfWords)
                        {
                            int countOfRules = 0;
                            // Можно изменить способ сверки
                            for (var i = 0; i < countOfWords; i++)
                            {
                                // Сверяет последовательность в цепочке с прпвилами из списка правил
                                if (Rules[ruleNumber].RuleList[i] == ArrS[currentOut - countOfWords + 1 + i].Number)
                                {
                                    countOfRules++;
                                }
                            }
                            // Если количество сошлось, то это то правило
                            if (countOfRules == countOfWords)
                            {
                                MyCompil(ruleNumber, currentOut);
                                // Сдвигаем правило на следующее, исключая символы, которые были свернуты
                                currentOut = currentOut - countOfWords + 1;
                                // Присваиваем правило на текущее место
                                ArrS[currentOut].Number = Rules[ruleNumber].RuleNumber - 1;
                                ArrS[currentOut].Value = Words[ArrS[currentOut].Number].Value;
                                rulesFounded.Add(ruleNumber);
                                //PrintInfo(currentOut, rulesFounded, splittedWords);
                                action = CompileActions.Next;
                                break;
                            }
                        }
                    }
                }
                if (action != CompileActions.Next)
                {
                    PrintCompileInfo.Invoke(@"Ошибка при выполнении восходящего разбора!");
                    PrintCompileResult.Invoke("");
                    //TODO: return
                    return;
                }
                PrintInfo(currentOut, rulesFounded, splittedWords);
                PrintCompileResult.Invoke(ArrS[currentOut].T);
            }
        }

        public bool IsNumber(string str2, string dopstr, int k)
        {
            int pr2 = 0;
            for (var i = 0; i < dopstr.Length; i++)
            {
                if (str2[k + 1] == i) pr2 = pr2 + 1;
            }
            return pr2 == 0 ? false : true;
        }

        /// <summary>
        /// Проверяет значение на тип
        /// </summary>
        /// <param name="str">Строка, в которой осуществляется поиск</param>
        /// <param name="startPos">Начальная позиция, для поиска</param>
        /// <param name="endPos">Конечная позиция, для поиска</param>
        /// <param name="splittedWords"></param>
        /// <returns></returns>
        public int IsThisNumber(string str, int startPos, int endPos, Queue<MyWord> splittedWords)
        {
            //Проверка булевых переменных
            if (str.Substring(startPos, endPos + 1 - startPos) == " true" ||
                str.Substring(startPos, endPos + 1 - startPos) == " false")
            {
                splittedWords.Enqueue(new MyWord(17, str.Substring(startPos, endPos + 1 - startPos)));
                return 1;
            }
            //Проверка на число
            if (IsNumber(str, "0123456789", startPos))
            {
                int srchLength = 0;
                for (var i = startPos; i < endPos; i++)
                {
                    if (IsNumber(str, "0123456789.Ee-", i))
                        srchLength = srchLength + 1;
                }
                if (srchLength == endPos - startPos)
                {
                    splittedWords.Enqueue(new MyWord(17, str.Substring(startPos, endPos + 1 - startPos)));

                    return 1;
                }
                PrintMessage.Invoke(@"Нужно вводить вещественные" + '\n' + @" числа с порядком!" + '\r' +
                                    @"Ошибка --> " + str.Substring(startPos, endPos + 1 - startPos));
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// Метод поиска (парсинга) входной строки
        /// Заполняется массив <see cref="Words"/> нашими символами грамматики (:=, if, и т.д.)
        /// </summary>
        /// <param name="str">Строка, в которой осуществляется поиск</param>
        /// <param name="startPos">Начальная позиция, для поиска</param>
        /// <param name="endPos">Конечная позиция, для поиска</param>
        /// <param name="splittedWords"></param>
        /// <returns> Возвращяется номер символа в грамматике </returns>
        public int IsThisOperator(string str, int startPos, int endPos, Queue<MyWord> splittedWords)
        {
            if (str[startPos] == ' ') startPos += 1;
            for (var i = CountOfRules; i < Rules.Length; i++)
            {
                if (endPos - startPos + 1 == (Words[i].Value).Length)
                {
                    int srchLength = 0;
                    for (var j = 1; j <= (Words[i].Value).Length; j++)
                    {
                        if (str[startPos + j - 1] == Words[i].Value[j - 1]) srchLength = srchLength + 1;
                    }
                    if (endPos - startPos + 1 == srchLength)
                    {
                        splittedWords.Enqueue(new MyWord(i, Words[i].Value));
                        return i;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Парсит входную строку и вызывает алгоритм
        /// </summary>
        /// <param name="str">Строка, для компиляции</param>
        public Queue<MyWord> Up(string str)
        {
            // Коллекция строк, которые были получены в результате парсинга строки
            Queue<MyWord> splittedWords = new Queue<MyWord>();

            str += " ";
            int nach = 0;
            int probel = 1;

            // Флаг ошибки при компиляции
            bool fail = false;

            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                    if (probel == 0)
                    {
                        probel = 1;
                        // Если вернулся 0, то будет производиться поиск на число или булевский тип
                        if (IsThisOperator(str, nach, i - 1, splittedWords) == 0)
                        {
                            int dop1 = IsThisNumber(str, nach, i - 1, splittedWords);
                            if (dop1 == 0)
                            {
                                if ((str.Substring(nach + 1, i - nach - 1)).Length <= 8 &&
                                    (str.Substring(nach + 1, i - nach - 1)).Length > 0)
                                {
                                    splittedWords.Enqueue(new MyWord(16, str.Substring(nach, i + 1 - nach)));
                                }
                                else
                                {
                                    if ((str.Substring(nach + 1, i - nach - 1)).Length > 8)
                                    {
                                        fail = true;
                                        PrintMessage.Invoke(@"Длина идентификатора не может быть больше 8 символов!" +
                                                            '\n' + @"Ошибка --> " + str.Substring(nach, i + 1 - nach));
                                    }
                                    if ((str.Substring(nach + 1, i - nach - 1)).Length == 0)
                                    {
                                        fail = true;
                                        PrintMessage.Invoke(@"Длина идентификатора не может быть меньше 0 символов!" +
                                                            '\n' + @"Сивмол № " + (i + 1).ToString() +
                                                            @" является пробелом.");
                                    }
                                }
                            }
                            if (dop1 == -1)
                            {
                                fail = true;
                            }
                        }
                    }
                if (!fail)
                {
                    if (probel == 1) nach = i;
                    probel = 0;
                }
            }
            if (!fail)
            {
                splittedWords.Enqueue(new MyWord(19, "$"));
                return splittedWords;
            }
            return null;
        }

        public void Run(string str)
        {
            //TODO: Убрать очистку
            for (var i = 0; i < ArrS.Length; i++)
            {
                ArrS[i] = new MyWord(0, "");
            }

            Queue<MyWord> words = Up(str);
            if (words != null)
            {
                Algorithm(words);
            }

        }

        public void Run(object obj)
        {
            if (obj is string str)
            {
                Run(str);
            }
        }
    }
}
