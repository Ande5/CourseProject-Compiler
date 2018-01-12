using System;
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

            textBox1.Text = @"= const const if := id const = const const if := id const";

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
                new Word(1, "S"),
                new Word(1, "C"),
                new Word(1, "W"),
                new Word(1, "X"),
                new Word(1, "A"),
                new Word(1, "L"),
                new Word(1, "R"),
                new Word(1, "if"),
                new Word(1, ":="),
                new Word(1, "id"),
                new Word(1, "["),
                new Word(1, "]"),
                new Word(1, "const"),
                new Word(1, "sqr"),
                new Word(1, "="),
                new Word(1, "!"),
                new Word(1, ">"),
                new Word(1, "<"),
                new Word(1, "-"),
                new Word(19, "$")
            };

            UpTableLoader loader = new UpTableLoader();
            int[,] table = loader.LoadTable("tableMy.txt");

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
            Thread compileThread = new Thread(Analysis.Run);
            compileThread.Start(str);
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
