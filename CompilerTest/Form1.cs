using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using LR;

namespace Compiler
{
    public partial class Form1 : Form
    {

        public UpAnalysis Analysis = new UpAnalysis();

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = @"set k 6E-1 for neq div k 9E-3 5E-2 set k mult k 3E-15 set x [ mult k k ] div mult x [ div k k ] or y y mult c sqrt z [ k ] set i 5E-1 for neq i 4E-17 set i div i 5E-8 set y [ div i mult i i ] div mult x [ div i i ] mult 5E-2 sqrt y [ i ] not y [ i ]";
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
