using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LR;
using LR.Data.Up;

namespace LRTests
{
    [TestClass]
    public class UpAnalysisTests
    {
        [TestMethod]
        public void StringParseTest()
        {

            Rule[] rules =
            {
                new Rule(1, new[] {1, 7, 2}),         // S-> C if W
                new Rule(1, new[] {1, 7, 2, 0}),      // S-> C if W S
                new Rule(2, new[] {5, 4}),            // C -> L A
                new Rule(3, new[] {3, 4}),            // W -> X A
                new Rule(3, new[] {3, 5}),            // W -> X L
                new Rule(4, new[] {8, 9}),            // X -> := id
                new Rule(4, new[] {8, 9, 10, 4, 11}), // X -> := id [ A ]
                new Rule(5, new[] {9}),               // A -> id
                new Rule(5, new[] {12}),              // A -> const
                new Rule(5, new[] {9, 10, 4, 11}),    // A -> id [ A ]
                new Rule(5, new[] {13, 4}),           // A -> sqr A
                new Rule(5, new[] {6, 4}),            // A -> R A
                new Rule(6, new[] {14, 4}),            // L -> = A
                new Rule(6, new[] {15, 4}),           // L -> ! A
                new Rule(6, new[] {16, 4}),           // L -> > A
                new Rule(6, new[] {17, 4}),           // L -> < A
                new Rule(7, new[] {18, 4}),           // R -> - A
            };

            Word[] words =
            {
                new Word(0, "S"),
                new Word(1, "C"),
                new Word(2, "W"),
                new Word(3, "X"),
                new Word(4, "A"),
                new Word(5, "L"),
                new Word(6, "R"),
                new Word(7, "if"),
                new Word(8, ":="),
                new Word(9, "id"),
                new Word(10, "["),
                new Word(11, "]"),
                new Word(12, "const"),
                new Word(13, "sqr"),
                new Word(14, "="),
                new Word(15, "!"),
                new Word(16, ">"),
                new Word(17, "<"),
                new Word(18, "-"),
                new Word(19, "$")
            };

            UpTableLoader loader = new UpTableLoader();

            int[,] table = loader.LoadTable("tableMy.txt");

            LrParser analysis = new LrParser(rules, words, table, 7);

            Queue<Word> wordsActual =  analysis.Up("= k 12 if := num 10 = 3 3 if := id 6");

            Queue<Word> wordsExpected = new Queue<Word>(
                new[]
                {
                    new Word(14, "="),
                    new Word(9, "id") { Temp = "k"},
                    new Word(12, "const") {Temp = 10.ToString()},
                    new Word(7, "if"),
                    new Word(8, ":="),
                    new Word(9, "id") {Temp = "num"},
                    new Word(12, "const") {Temp = 8.ToString()},
                    new Word(14, "="),
                    new Word(12, "const") {Temp = 3.ToString()},
                    new Word(12, "const") {Temp = 3.ToString()},
                    new Word(7, "if"),
                    new Word(8, ":="),
                    new Word(9, "id") {Temp = "id"},
                    new Word(12, "const") {Temp = 6.ToString()},
                    new Word(19, "$")
                }
            );

            Assert.AreEqual(wordsExpected.Count, wordsActual.Count, $"Длинна очередей разная.\nОжидается: {wordsExpected.Count}\nТекущая: {wordsActual.Count}");

            int count = 0;

            while (wordsExpected.Count > 0)
            {
                Word expectedWord = wordsExpected.Dequeue();
                Word actualWord = wordsActual.Dequeue();
                Assert.AreEqual(expectedWord.Value, actualWord.Value, GetErrorMessage(expectedWord, actualWord, count));
                Assert.AreEqual(expectedWord.Number, actualWord.Number, GetErrorMessage(expectedWord, actualWord, count));
                Assert.AreEqual(expectedWord.Temp, actualWord.Temp, GetErrorMessage(expectedWord, actualWord, count));
                count++;
            }
        }

        private string GetErrorMessage(Word expectedWord, Word actualWord, int index)
        {
            string result = $"Индекс слова: {index}\n";
            result += $"Ожидается:  \nValue: {expectedWord.Value}\n Number: {expectedWord.Number}\n Temp: {expectedWord.Temp}\n";
            result += $"Фактически: \nValue: {actualWord.Value}\n   Number: {actualWord.Number}\n   Temp: {actualWord.Temp}";
            return result;
        }
    }
}
