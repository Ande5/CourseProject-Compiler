using System;
using System.Collections.Generic;

namespace LR
{
    public class UpAnalysis
    {
        public UpAnalysis(MyRule[] ruleses, MyWord[] words, int[,] table, int countOfRules)
        {
            Rules = ruleses;
            ArrWords = words;
            ArrZ = table;
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
        public MyWord[] ArrWords;

        /// <summary>
        /// Управляющая таблица восходящего разбора
        /// </summary>
        public int[,] ArrZ;

        /// <summary>
        /// Количество правил
        /// </summary>
        public int CountOfRules;

        /// <summary>
        /// Коллекций номеров правил
        /// </summary>
        public List<int> RulesFounded = new List<int>();

        public MyWord[] ArrS = new MyWord[1000];

        public void PrintInfo(int zz, int xx, Queue<MyWord> splittedWords)
        {
            string s1 = "";
            foreach (var word in splittedWords)
            {
                s1 += ArrWords[word.L].W + " ";
            }
            PrintCompileInfo.Invoke(@"Строка:" + s1 + '\n');
            string s2 = "";
            int i2 = 0;
            while (i2 <= zz)
            {
                s2 = s2 + ArrWords[ArrS[i2].L].W + " ";
                i2 = i2 + 1;
            }
            PrintCompileInfo.Invoke(@"Магазин:" + s2 + '\n');
            string s3 = "";
            i2 = 1;
            while (i2 < xx)
            {
                s3 = s3 + (RulesFounded[i2] + 1) + " ";
                i2 = i2 + 1;
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
                ArrS[ts1 - 5].T = "for ( " + ArrS[ts1 - 5].T + ArrS[ts1 - 4].T + "; " + ArrS[ts1 - 2].T + "; " + ArrS[ts1 - 1].T + " ) {" + ArrS[ts1].T + "} ;";
            if (ruleNumber == 2)
                ArrS[ts1 - 6].T = "for ( " + ArrS[ts1 - 6].T + ArrS[ts1 - 5].T + "; " + ArrS[ts1 - 3].T + "; " + ArrS[ts1 - 2].T + " ) {" + ArrS[ts1 - 1].T + "} ;" + Environment.NewLine + ArrS[ts1].T;
            if (ruleNumber == 4)
                ArrS[ts1 - 4].T = ArrS[ts1 - 3].W + "[ " + ArrS[ts1 - 1].T + " ] = ";
            if (ruleNumber == 3)
                ArrS[ts1 - 1].T = ArrS[ts1].W + " = ";
            if (ruleNumber == 6)
                ArrS[ts1 - 1].T = ArrS[ts1 - 1].T + " " + ArrS[ts1].T;
            if (ruleNumber == 8)
                ArrS[ts1].T = ArrS[ts1].W;
            if (ruleNumber == 7)
                ArrS[ts1].T = ArrS[ts1].W;
            if (ruleNumber == 11)
                ArrS[ts1 - 1].T = "! ( " + ArrS[ts1].T + " )";
            if (ruleNumber == 10)
                ArrS[ts1 - 1].T = "sqrt ( " + ArrS[ts1].T + " )";
            if (ruleNumber == 12)
                ArrS[ts1 - 1].T = ArrS[ts1 - 1].T + " " + ArrS[ts1].T;
            if (ruleNumber == 9)
                ArrS[ts1 - 3].T = ArrS[ts1 - 3].W + "[ " + ArrS[ts1 - 1].T + " ] ";
            if (ruleNumber == 15)
                ArrS[ts1 - 1].T = ArrS[ts1].T + " != ";
            if (ruleNumber == 16)
                ArrS[ts1 - 1].T = ArrS[ts1].T + " ^ ";
            if (ruleNumber == 13)
                ArrS[ts1 - 1].T = ArrS[ts1].T + " * ";
            if (ruleNumber == 14)
                ArrS[ts1 - 1].T = ArrS[ts1].T + " / ";

        }

        public void Algorithm(Queue<MyWord> splittedWords)
        {
            int currentOut = 0;
            ArrS[currentOut].L = 18;
            ArrS[currentOut].W = "$";
            PrintInfo(currentOut, RulesFounded.Count, splittedWords);
            while (splittedWords.Count > 0)
            {
                int go = 0;
                MyWord word = splittedWords.Peek();
                if (word.L == 18)
                {
                    if (currentOut == 1)
                    {
                        if ((ArrS[currentOut].L == 0) && (ArrS[currentOut - 1].L == 18))
                        {
                            // Тут можно делать выход из алгоритма
                            go = 4;
                        }
                    }
                }

                int row = ArrS[currentOut].L;
                int col = word.L;

                if (((ArrZ[row, col] == 1) && go != 4) || ((ArrZ[row, col] == 2) && go != 4))
                {
                    currentOut++;
                    MyWord tmpWord = splittedWords.Dequeue();
                    ArrS[currentOut] = tmpWord;
                    PrintInfo(currentOut, RulesFounded.Count, splittedWords);
                    go = 2;
                }
                if ((ArrZ[row, col] == 3) && go != 2 && go != 4)
                {
                    int koli = 0;
                    while (ArrZ[ArrS[currentOut - koli].L, ArrS[currentOut - koli + 1].L] != 1)
                    {
                        koli++;
                    }
                    //TODO: Количество правил
                    for (var ruleNumber = 0; ruleNumber < Rules.Length; ruleNumber++)
                    {
                        if (Rules[ruleNumber].L == koli && go != 2)
                        {
                            int pr11 = 0;
                            for (var pr12 = 0; pr12 < koli; pr12++)
                            {
                                if (Rules[ruleNumber].M[pr12] == ArrS[currentOut - koli + 1 + pr12].L)
                                {
                                    pr11++;
                                }
                            }
                            if (pr11 == koli)
                            {
                                MyCompil(ruleNumber, currentOut);
                                currentOut = currentOut - koli + 1;
                                // Присваиваем правило
                                ArrS[currentOut].L = Rules[ruleNumber].P - 1;
                                ArrS[currentOut].W = ArrWords[(ArrS[currentOut].L)].W;
                                RulesFounded.Add(ruleNumber);
                                PrintInfo(currentOut, RulesFounded.Count, splittedWords);
                                go = 2;
                                //TODO: Сделать выход из цикла. Зря тратися время
                            }
                        }
                    }
                }
                if (go != 2 && go != 4)
                {
                    PrintCompileInfo.Invoke(@"Ошибка при выполнении восходящего разбора!");
                    PrintCompileResult.Invoke("");
                    //TODO: return
                    return;
                }
                if (go != 3)
                {
                    PrintCompileResult.Invoke(ArrS[currentOut].T);
                }
                if (go == 4)
                {
                    // финальная точка
                    // очистка нафиг не нужна
                    //TODO: return
                    PrintInfo(currentOut, RulesFounded.Count, splittedWords);
                    return;
                }
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
            if (str.Substring(startPos, endPos + 1 - startPos) == " true" || str.Substring(startPos, endPos + 1 - startPos) == " false")
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
                PrintMessage.Invoke(@"Нужно вводить вещественные" + '\n' + @" числа с порядком!" + '\r' + @"Ошибка --> " + str.Substring(startPos, endPos + 1 - startPos));
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// Метод поиска (парсинга) входной строки
        /// Заполняется массив <see cref="ArrWords"/> нашими символами грамматики (:=, if, и т.д.)
        /// </summary>
        /// <param name="str">Строка, в которой осуществляется поиск</param>
        /// <param name="startPos">Начальная позиция, для поиска</param>
        /// <param name="endPos">Конечная позиция, для поиска</param>
        /// <param name="splittedWords"></param>
        /// <returns> Возвращяется номер символа в грамматике </returns>
        public int IsThisOperator(string str, int startPos, int endPos, Queue<MyWord> splittedWords)
        {
            if (str[startPos] == ' ') startPos += 1;
            for (var i = 6; i < 16; i++)
            {
                if (endPos - startPos + 1 == (ArrWords[i].W).Length)
                {
                    int srchLength = 0;
                    for (var j = 1; j <= (ArrWords[i].W).Length; j++)
                    {
                        if (str[startPos + j - 1] == ArrWords[i].W[j - 1]) srchLength = srchLength + 1;
                    }
                    if (endPos - startPos + 1 == srchLength)
                    {
                        splittedWords.Enqueue(new MyWord(i, ArrWords[i].W));
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
        ArrS[1].T = "";

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
                                if ((str.Substring(nach + 1, i - nach - 1)).Length <= 8 && (str.Substring(nach + 1, i - nach - 1)).Length > 0)
                                {
                                    splittedWords.Enqueue(new MyWord(16, str.Substring(nach, i + 1 - nach)));
                                }
                                else
                                {
                                    if ((str.Substring(nach + 1, i - nach - 1)).Length > 8)
                                    {
                                        fail = true;
                                        PrintMessage.Invoke(@"Длина идентификатора не может быть больше 8 символов!" + '\n' + @"Ошибка --> " + str.Substring(nach, i + 1 - nach));
                                    }
                                    if ((str.Substring(nach + 1, i - nach - 1)).Length == 0)
                                    {
                                        fail = true;
                                        PrintMessage.Invoke(@"Длина идентификатора не может быть меньше 0 символов!" + '\n' + @"Сивмол № " + (i + 1).ToString() + @" является пробелом.");
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
                splittedWords.Enqueue(new MyWord(18, "$"));
                return splittedWords;
            }
            return null;
        }

        public void Run(string str)
        {
            //TODO: Убрать очистку
            for (var i = 0; i < ArrS.Length; i++)
            {
                ArrS[i]= new MyWord(0, "");
            }
            RulesFounded.Clear();

            Queue<MyWord> words = Up(str);
            if (words != null)
            {
                Algorithm(words);
            }
            
        }
    }
}
