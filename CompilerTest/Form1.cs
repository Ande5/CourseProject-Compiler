using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Compiler
{
    public partial class Form1 : Form
    {
        public string Str = "";
        public struct MyWord
        {
            public MyWord(int l, string w)
            {
                L = l;
                W = w;
                T = "";
            }

            public int L;
            public string W, T;
        }
        public struct MyRule
        {
            public MyRule(int p, int[] m)
            {
                P = p;
                M = m;
            }
            public int P;
            public int L => M.Length;
            public int[] M;
        }
        public struct MyUpStr
        {
            public int I;
            public string Name;
        }

        public int IndS;

        public List<MyWord> SplittedWords = new List<MyWord>();

        public MyWord[] ArrS = new MyWord[1000];

        public MyRule[] Rule =
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

        public MyWord[] ArrWords =
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

        public List<int> Rules = new List<int>();

        public int[,] ArrZ = {
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

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = @"set k 6E-1 for neq div k 9E-3 5E-2 set k mult k 3E-15 set x [ mult k k ] div mult x [ div k k ] or y y mult c sqrt z [ k ] set i 5E-1 for neq i 4E-17 set i div i 5E-8 set y [ div i mult i i ] div mult x [ div i i ] mult 5E-2 sqrt y [ i ] not y [ i ]";
        }

        public void Print(int yy, int zz, int xx)
        {
            string s1 = "";
            int i2 = yy;
            while (i2 < SplittedWords.Count)
            {
                s1 = s1 + ArrWords[SplittedWords[i2].L].W + " ";
                i2 = i2 + 1;
            }
            richTextBox2.Text += @"Строка:" + s1 + '\n';
            string s2 = "";
            i2 = 0;
            while (i2 <= zz)
            {
                s2 = s2 + ArrWords[ArrS[i2].L].W + " ";
                i2 = i2 + 1;
            }
            richTextBox2.Text += @"Магазин:" + s2 + '\n';
            string s3 = "";
            i2 = 1;
            while (i2 < xx)
            {
                s3 = s3 + (Rules[i2] + 1).ToString() + " ";
                i2 = i2 + 1;
            }
            richTextBox2.Text += @"Правила:" + s3 + '\n';
            richTextBox2.Text += @"      " + '\n';
        }

        public void MyCompil(int pp, int ts1)
        {
            pp = pp + 1;
            if (pp == 1)
                ArrS[ts1 - 5].T = "for ( " + ArrS[ts1 - 5].T + ArrS[ts1 - 4].T + "; " + ArrS[ts1 - 2].T + "; " + ArrS[ts1 - 1].T + " ) {" + ArrS[ts1].T + "} ;";
            if (pp == 2)
                ArrS[ts1 - 6].T = "for ( " + ArrS[ts1 - 6].T + ArrS[ts1 - 5].T + "; " + ArrS[ts1 - 3].T + "; " + ArrS[ts1 - 2].T + " ) {" + ArrS[ts1 - 1].T + "} ;" + Environment.NewLine + ArrS[ts1].T;
            if (pp == 4)
                ArrS[ts1 - 4].T = ArrS[ts1 - 3].W + "[ " + ArrS[ts1 - 1].T + " ] = ";
            if (pp == 3)
                ArrS[ts1 - 1].T = ArrS[ts1].W + " = ";
            if (pp == 6)
                ArrS[ts1 - 1].T = ArrS[ts1 - 1].T + " " + ArrS[ts1].T;
            if (pp == 8)
                ArrS[ts1].T = ArrS[ts1].W;
            if (pp == 7)
                ArrS[ts1].T = ArrS[ts1].W;
            if (pp == 11)
                ArrS[ts1 - 1].T = "! ( " + ArrS[ts1].T + " )";
            if (pp == 10)
                ArrS[ts1 - 1].T = "sqrt ( " + ArrS[ts1].T + " )";
            if (pp == 12)
                ArrS[ts1 - 1].T = ArrS[ts1 - 1].T + " " + ArrS[ts1].T;
            if (pp == 9)
                ArrS[ts1 - 3].T = ArrS[ts1 - 3].W + "[ " + ArrS[ts1 - 1].T + " ] ";
            if (pp == 15)
                ArrS[ts1 - 1].T = ArrS[ts1].T + " != ";
            if (pp == 16)
                ArrS[ts1 - 1].T = ArrS[ts1].T + " ^ ";
            if (pp == 13)
                ArrS[ts1 - 1].T = ArrS[ts1].T + " * ";
            if (pp == 14)
                ArrS[ts1 - 1].T = ArrS[ts1].T + " / ";

        }

        public void Algorithm()
        {
            int tm = 0;
            int ts = 0;
            int go = 0;
            ArrS[0].L = 18;
            ArrS[0].W = "$";
            Print(tm, ts, Rules.Count);
            while (tm <= SplittedWords.Count)
            {
                if (SplittedWords[tm].L == 18)
                {
                    if (ts == 1)
                    {
                        if ((ArrS[ts].L == 0) && (ArrS[ts - 1].L == 18))
                        {
                            go = 4;
                        }
                    }
                }
                if (((ArrZ[ArrS[ts].L, SplittedWords[tm].L] == 1) && go != 4) || ((ArrZ[ArrS[ts].L, SplittedWords[tm].L] == 2) && go != 4))
                {
                    ts = ts + 1;
                    ArrS[ts].L = SplittedWords[tm].L;
                    ArrS[ts].W = SplittedWords[tm].W;
                    tm = tm + 1;
                    Print(tm, ts, Rules.Count);
                    go = 2;
                }
                if ((ArrZ[ArrS[ts].L, SplittedWords[tm].L] == 3) && go != 2 && go != 4)
                {
                    int koli = 0;
                    while (ArrZ[ArrS[ts - koli].L, ArrS[ts - koli + 1].L] != 1)
                    {
                        koli = koli + 1;
                    }
                    for (var p10 = 0; p10 < 16; p10++)
                    {
                        if (Rule[p10].L == koli && go != 2)
                        {
                            int pr11 = 0;
                            for (var pr12 = 0; pr12 < koli; pr12++)
                            {
                                if (Rule[p10].M[pr12] == ArrS[ts - koli + 1 + pr12].L)
                                {
                                    pr11 = pr11 + 1;
                                }
                            }
                            if (pr11 == koli)
                            {
                                MyCompil(p10, ts);
                                ts = ts - koli + 1;
                                ArrS[ts].L = Rule[p10].P - 1;
                                ArrS[ts].W = ArrWords[(ArrS[ts].L)].W;
                                Rules.Add(p10);
                                Print(tm, ts, Rules.Count);
                                go = 2;
                            }
                        }
                    }
                }
                if (go != 2 && go != 4)
                {
                    richTextBox2.Text = @"Ошибка при выполнении восходящего разбора!";
                    textBox2.Text = "";
                    go = 3;
                    tm = 10000;
                    for (int i = 0; i < ArrS.Length; i++)
                    {
                        ArrS[i].L = 0;
                        ArrS[i].T = "";
                        ArrS[i].W = "";
                    }
                    SplittedWords.Clear();
                    for (int i = 0; i < Rules.Count; i++)
                    {
                        Rules[i] = 0;
                    }
                    Str = "";
                }
                if (go != 3)
                {
                    textBox2.Text = ArrS[1].T;
                }
                if (go == 4)
                {
                    tm = 10000;
                    for (int i = 0; i < ArrS.Length; i++)
                    {
                        ArrS[i].L = 0;
                        ArrS[i].T = "";
                        ArrS[i].W = "";
                    }
                    SplittedWords.Clear();
                    for (int i = 0; i < Rules.Count; i++)
                    {
                        Rules[i] = 0;
                    }
                    Str = "";
                }
                go = 0;
            }
        }
        public int IsNumber(string str2, string dopstr, int k)
        {
            int pr2 = 0;
            for (var i = 0; i < dopstr.Length; i++)
            {
                if (str2[k + 1] == i) pr2 = pr2 + 1;
            }
            if (pr2 == 0) return 0;
            else return 1;
        }

        public int IsThisNumber(string str1, int nach1, int kon1)
        {
            if (str1.Substring(nach1, kon1 + 1 - nach1) == " true" || str1.Substring(nach1, kon1 + 1 - nach1) == " false")
            {
                SplittedWords.Add(new MyWord(17, str1.Substring(nach1, kon1 + 1 - nach1)));
                return 1;
            }
            else
            if (IsNumber(str1, "0123456789", nach1) == 1)
            {
                int pr3 = 0;
                for (var jjj = nach1; jjj < kon1; jjj++)
                {
                    if (IsNumber(str1, "0123456789.Ee-", jjj) == 1) pr3 = pr3 + 1;
                }
                if (pr3 == kon1 - nach1)
                {
                    SplittedWords.Add(new MyWord(17, str1.Substring(nach1, kon1 + 1 - nach1)));

                    return 1;
                }
                else
                {
                    MessageBox.Show(@"Нужно вводить вещественные" + '\n' + @" числа с порядком!" + '\r' + @"Ошибка --> " + str1.Substring(nach1, kon1 + 1 - nach1));
                    Str = "";
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
        public int IsThisOperator(string str, int startPos, int endPos)
        {
            if (str[startPos] == ' ') startPos += 1;
            for (var i = 6; i < 16; i++)
            {
                if (endPos - startPos + 1 == (ArrWords[i].W).Length)
                {
                    int kol = 0;
                    for (var j = 1; j <= (ArrWords[i].W).Length; j++)
                    {
                        if (str[startPos + j - 1] == ArrWords[i].W[j - 1]) kol = kol + 1;
                    }
                    if (endPos - startPos + 1 == kol)
                    {
                        SplittedWords.Add(new MyWord(i, ArrWords[i].W));
                        return i;
                    }
                }
            }
            return 0;
        }

        public void Up()
        {
            int k = 0;
            ArrS[1].T = "";
            richTextBox2.Clear();
            textBox2.Clear();
            Str += textBox1.Text;
            Str += " ";
            int nach = 0;
            int probel = 1;
            for (var i = 0; i < Str.Length; i++)
            {
                if (Str[i] == ' ')
                    if (probel == 0)
                    {
                        probel = 1;
                        if (IsThisOperator(Str, nach, i - 1) == 0)
                        {
                            int dop1 = IsThisNumber(Str, nach, i - 1);
                            if (dop1 == 0)
                            {
                                if ((Str.Substring(nach + 1, i - nach - 1)).Length <= 8 && (Str.Substring(nach + 1, i - nach - 1)).Length > 0)
                                {
                                    SplittedWords.Add(new MyWord(16, Str.Substring(nach, i + 1 - nach)));
                                }
                                else
                                {
                                    if ((Str.Substring(nach + 1, i - nach - 1)).Length > 8)
                                    {
                                        k = 1;
                                        MessageBox.Show(@"Длина идентификатора не может быть больше 8 символов!" + '\n' + @"Ошибка --> " + Str.Substring(nach, i + 1 - nach));
                                    }
                                    if ((Str.Substring(nach + 1, i - nach - 1)).Length == 0)
                                    {
                                        k = 1;
                                        MessageBox.Show(@"Длина идентификатора не может быть меньше 0 символов!" + '\n' + @"Сивмол № " + (i + 1).ToString() + @" является пробелом.");
                                    }
                                }
                            }
                            if (dop1 == -1)
                            {
                                k = 1;
                            }
                        }
                    }
                if (k != 1)
                {
                    if (probel == 1) nach = i;
                    probel = 0;
                }
            }
            if (k != 1)
            {
                SplittedWords.Add(new MyWord(18, "$"));
                Algorithm();
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Str = "";
            
            for (int i = 0; i < ArrS.Length; i++)
            {
                ArrS[i].L = 0;
                ArrS[i].T = "";
                ArrS[i].W = "";
            }
            SplittedWords.Clear();
            Rules.Clear();
            Str = "";
            Up();
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
