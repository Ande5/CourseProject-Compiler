using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using LR;
using LR.Data;

namespace Compiler
{
    public partial class Form1 : Form
    {

        public UpAnalysis Analysis;

        public Form1()
        {
        InitializeComponent();
            textBox1.Text = @"set k 6E-1 for neq div k 9E-3 5E-2 set k mult k 3E-15 set x [ mult k k ] div mult x [ div k k ] or y y mult c sqrt z [ k ] set i 5E-1 for neq i 4E-17 set i div i 5E-8 set y [ div i mult i i ] div mult x [ div i i ] mult 5E-2 sqrt y [ i ] not y [ i ]";
            //textBox1.Text = @"set k 6E-1 for neq div k 9E-3 5E-2 set k mult k 3E-15 set x [ mult k k ] div mult x [ div k k ] or y y mult c sqrt z [ k ]";
            //textBox1.Text = @"set k 6E-1 for neq 9E-3 5E-2 set k 3E-15 set x sqrt z";
            //textBox1.Text = @"= const const if := const const";
            textBox1.Text = @"= const const if := id const = const const if := id const";

            //MyRule[] rules =
            //{
            //    new MyRule(1, new[] {1, 5, 6, 2, 3, 3}),
            //    new MyRule(1, new[] {1, 5, 6, 2, 3, 3, 0}),
            //    new MyRule(2, new[] {7, 16}),
            //    new MyRule(2, new[] {7, 16, 8, 2, 9}),
            //    new MyRule(3, new[] {5}),
            //    new MyRule(4, new[] {1, 5}),
            //    new MyRule(6, new[] {16}),
            //    new MyRule(6, new[] {17}),
            //    new MyRule(6, new[] {16, 8, 2, 9}),
            //    new MyRule(6, new[] {11, 5}),
            //    new MyRule(6, new[] {10, 5}),
            //    new MyRule(6, new[] {4, 5}),
            //    new MyRule(5, new[] {14, 5}),
            //    new MyRule(5, new[] {15, 5}),
            //    new MyRule(5, new[] {12, 5}),
            //    new MyRule(5, new[] {13, 5})
            //};

            MyRule[] rules =
            {
                new MyRule(1, new[] {1, 7, 2}),         // S-> C if W
                new MyRule(1, new[] {1, 7, 2, 0}),      // S-> C if W S
                new MyRule(2, new[] {5, 4}),            // C -> L A
                new MyRule(3, new[] {3, 4}),            // W -> X A
                new MyRule(3, new[] {3, 5}),            // W -> X L
                new MyRule(4, new[] {8, 9}),            // X -> := id
                new MyRule(4, new[] {8, 9, 10, 4, 11}), // X -> := id [ A ]
                new MyRule(5, new[] {9}),               // A -> id
                new MyRule(5, new[] {12}),              // A -> const
                new MyRule(5, new[] {9, 10, 4, 11}),    // A -> id [ A ]
                new MyRule(5, new[] {13, 4}),           // A -> sqr A
                new MyRule(5, new[] {6, 4}),            // A -> R A
                new MyRule(6, new[] {5, 4}),            // L -> = A
                new MyRule(6, new[] {13, 4}),           // L -> ! A
                new MyRule(6, new[] {14, 4}),           // L -> > A
                new MyRule(6, new[] {15, 5}),           // L -> < A
                new MyRule(7, new[] {16, 5}),           // R -> - A
            };

            //MyWord[] words =
            //{
            //    new MyWord(1, "S"),
            //    new MyWord(1, "X"),
            //    new MyWord(1, "Y"),
            //    new MyWord(1, "W"),
            //    new MyWord(1, "R"),
            //    new MyWord(1, "A"),
            //    new MyWord(1, "for"),
            //    new MyWord(1, "set"),
            //    new MyWord(1, "["),
            //    new MyWord(1, "]"),
            //    new MyWord(1, "not"),
            //    new MyWord(1, "sqrt"),
            //    new MyWord(1, "neq"),
            //    new MyWord(1, "or"),
            //    new MyWord(1, "mult"),
            //    new MyWord(1, "div"),
            //    new MyWord(1, "id"),
            //    new MyWord(1, "const"),
            //    new MyWord(1, "$")
            //};

            MyWord[] words =
            {
                new MyWord(1, "S"),
                new MyWord(1, "C"),
                new MyWord(1, "W"),
                new MyWord(1, "X"),
                new MyWord(1, "A"),
                new MyWord(1, "L"),
                new MyWord(1, "R"),
                new MyWord(1, "if"),
                new MyWord(1, ":="),
                new MyWord(1, "id"),
                new MyWord(1, "["),
                new MyWord(1, "]"),
                new MyWord(1, "const"),
                new MyWord(1, "sqr"),
                new MyWord(1, "="),
                new MyWord(1, "!"),
                new MyWord(1, ">"),
                new MyWord(1, "<"),
                new MyWord(1, "-"),
                new MyWord(1, "$")
            };

            // Юрьев

            //int[,] table = {
            //    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3},
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            //    {0, 1, 0, 2, 0, 0, 0, 1, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            //    {2, 1, 0, 2, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3},
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            //    {0, 0, 0, 0, 0, 0, 2, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            //    {0, 0, 2, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            //    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0},
            //    {0, 0, 2, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            //    {0, 0, 0, 0, 0, 0, 3, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            //    {0, 0, 0, 0, 0, 0, 3, 3, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            //    {0, 0, 0, 0, 0, 0, 3, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            //    {0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            //};



            UpTableLoader loader = new UpTableLoader();
            int[,] table = loader.LoadTable("tableMy.txt");


            //int[,] table = {
            //    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3},  //S
            //    {0, 0, 0, 0, 1, 2, 0, 3, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},  //X
            //    {0, 0, 0, 0, 0, 0, 2, 1, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0},  //Y
            //    {2, 3, 0, 0, 0, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3},  //W
            //    {0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},  //R
            //    {0, 0, 0, 0, 1, 0, 1, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},  //A
            //    {0, 3, 0, 2, 0, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},  //if
            //    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0},  //:=
            //    {0, 0, 2, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},  //[
            //    {0, 0, 0, 0, 0, 0, 0, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},  //]
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},  //=
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},  //!
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},  //>
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},  //>
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},  //- 
            //    {0, 0, 0, 0, 1, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},  //sqr
            //    {0, 3, 0, 0, 0, 0, 1, 3, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},  //id
            //    {0, 0, 0, 0, 0, 0, 1, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},  //const
            //    {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0}   //$
            //};

            Analysis = new UpAnalysis(rules, words, table, 7);

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
            if (!InvokeRequired)
                richTextBox2.Text += text;
            else
                Invoke(new UpAnalysis.PrintDelegate(AnalysisOnPrintCompileInfo), new object[] {text});
            
        }

        private void AnalysisOnPrintCompileResult(string text)
        {
            if (!InvokeRequired)
                textBox2.Text = text;
            else
                Invoke(new UpAnalysis.PrintDelegate(AnalysisOnPrintCompileResult), new object[] { text });
            
        }

        private void button1_Click(object sender, EventArgs e)
        {        
            richTextBox2.Clear();
            textBox2.Clear();

            string str = textBox1.Text;
            Thread compileThread = new Thread(new ParameterizedThreadStart(Analysis.Run));
            compileThread.Start(str);
            //Analysis.Run(str);
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
