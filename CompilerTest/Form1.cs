using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using LR;

namespace Compiler
{
    public partial class Form1 : Form
    {

        public UpAnalysis Analysis;

        public Form1()
        {
        InitializeComponent();
            textBox1.Text = @"set k 6E-1 for neq div k 9E-3 5E-2 set k mult k 3E-15 set x [ mult k k ] div mult x [ div k k ] or y y mult c sqrt z [ k ] set i 5E-1 for neq i 4E-17 set i div i 5E-8 set y [ div i mult i i ] div mult x [ div i i ] mult 5E-2 sqrt y [ i ] not y [ i ]";

            MyRule[] rules =
            {
                new MyRule(1, new[] {1, 5, 6, 2, 3, 3}),
                new MyRule(1, new[] {1, 5, 6, 2, 3, 3, 0}),
                new MyRule(2, new[] {7, 16}),
                new MyRule(2, new[] {7, 16, 8, 2, 9}),
                new MyRule(3, new[] {5}),
                new MyRule(4, new[] {1, 5}),
                new MyRule(6, new[] {16}),
                new MyRule(6, new[] {17}),
                new MyRule(6, new[] {16, 8, 2, 9}),
                new MyRule(6, new[] {11, 5}),
                new MyRule(6, new[] {10, 5}),
                new MyRule(6, new[] {4, 5}),
                new MyRule(5, new[] {14, 5}),
                new MyRule(5, new[] {15, 5}),
                new MyRule(5, new[] {12, 5}),
                new MyRule(5, new[] {13, 5})
            };

            MyWord[] words =
            {
                new MyWord(1, "S"),
                new MyWord(1, "X"),
                new MyWord(1, "Y"),
                new MyWord(1, "W"),
                new MyWord(1, "R"),
                new MyWord(1, "A"),
                new MyWord(1, "for"),
                new MyWord(1, "set"),
                new MyWord(1, "["),
                new MyWord(1, "]"),
                new MyWord(1, "not"),
                new MyWord(1, "sqrt"),
                new MyWord(1, "neq"),
                new MyWord(1, "or"),
                new MyWord(1, "mult"),
                new MyWord(1, "div"),
                new MyWord(1, "id"),
                new MyWord(1, "const"),
                new MyWord(1, "$")
            };

            int[,] table = {
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3},
                {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 1, 0, 2, 0, 0, 0, 1, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {2, 1, 0, 2, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3},
                {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 0, 0, 2, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 2, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0},
                {0, 0, 2, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 0, 0, 3, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 0, 0, 3, 3, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 0, 0, 0, 0, 0, 3, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            };

            Analysis = new UpAnalysis(rules, words, table, 6);

            Analysis.PrintCompileInfo += AnalysisOnPrintCompileInfo;
            Analysis.PrintCompileResult += AnalysisOnPrintCompileResult;
            Analysis.PrintMessage += AnalysisOnPrintMessage;
        }

        private void AnalysisOnPrintMessage(string text)
        {
            MessageBox.Show(text);
        }

        private void AnalysisOnPrintCompileInfo(string text)
        {
            richTextBox2.Text += text;
        }

        private void AnalysisOnPrintCompileResult(string text)
        {
            textBox2.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {        
            richTextBox2.Clear();
            textBox2.Clear();

            string str = textBox1.Text;
            Analysis.Run(str);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            richTextBox2.SelectionStart = richTextBox2.Text.Length;
            richTextBox2.ScrollToCaret();
        }
    }
}
