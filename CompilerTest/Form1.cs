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

        //public int Indarr;
        public int IndS;
        public int IndR;

        public List<MyWord> Arr = new List<MyWord>();

        //public MyWord[] Arr = new MyWord[1000];
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

        public int[] Rules = new int[1000];

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

        public MyUpStr[] ArrNt = new MyUpStr[13];

        public MyUpStr[] ArrStr = new MyUpStr[1000];
        public string[] ArrM = new string[1000];
        public int Ind;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = @"set k 6E-1 for neq div k 9E-3 5E-2 set k mult k 3E-15 set x [ mult k k ] div mult x [ div k k ] or y y mult c sqrt z [ k ] set i 5E-1 for neq i 4E-17 set i div i 5E-8 set y [ div i mult i i ] div mult x [ div i i ] mult 5E-2 sqrt y [ i ] not y [ i ]";

            


            
        }

        public void Print(int yy, int zz, int xx)
        {
            int i2;
            string s1, s2, s3;
            s1 = "";
            i2 = yy;
            while (i2 < Arr.Count)
            {
                s1 = s1 + ArrWords[Arr[i2].L].W + " ";
                i2 = i2 + 1;
            }
            richTextBox2.Text += @"Строка:" + s1 + '\n';
            s2 = "";
            i2 = 0;
            while (i2 <= zz)
            {
                s2 = s2 + ArrWords[ArrS[i2].L].W + " ";
                i2 = i2 + 1;
            }
            richTextBox2.Text += @"Магазин:" + s2 + '\n';
            s3 = "";
            i2 = 1;
            while (i2 <= xx)
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
            int Tm, Ts, koli, p10, pr11, pr12;
            Tm = 0;
            Ts = 0;
            int go = 0;
            ArrS[0].L = 18;
            ArrS[0].W = "$";
            IndR = 0;
            Print(Tm, Ts, IndR);
            while (Tm <= Arr.Count)
            {
                if (Arr[Tm].L == 18)
                {
                    if (Ts == 1)
                    {
                        if ((ArrS[Ts].L == 0) && (ArrS[Ts - 1].L == 18))
                        {
                            go = 4;
                        }
                    }
                }
                if (((ArrZ[ArrS[Ts].L, Arr[Tm].L] == 1) && go != 4) || ((ArrZ[ArrS[Ts].L, Arr[Tm].L] == 2) && go != 4))
                {
                    Ts = Ts + 1;
                    ArrS[Ts].L = Arr[Tm].L;
                    ArrS[Ts].W = Arr[Tm].W;
                    Tm = Tm + 1;
                    Print(Tm, Ts, IndR);
                    go = 2;
                }
                if ((ArrZ[ArrS[Ts].L, Arr[Tm].L] == 3) && go != 2 && go != 4)
                {
                    koli = 0;
                    while (ArrZ[ArrS[Ts - koli].L, ArrS[Ts - koli + 1].L] != 1)
                    {
                        koli = koli + 1;
                    }
                    for (p10 = 0; p10 < 16; p10++)
                    {
                        if (Rule[p10].L == koli && go != 2)
                        {
                            pr11 = 0;
                            for (pr12 = 0; pr12 < koli; pr12++)
                            {
                                if (Rule[p10].M[pr12] == ArrS[Ts - koli + 1 + pr12].L)
                                {
                                    pr11 = pr11 + 1;
                                }
                            }
                            if (pr11 == koli)
                            {
                                MyCompil(p10, Ts);
                                Ts = Ts - koli + 1;
                                ArrS[Ts].L = Rule[p10].P - 1;
                                ArrS[Ts].W = ArrWords[(ArrS[Ts].L)].W;
                                IndR = IndR + 1;
                                Rules[IndR] = p10;
                                Print(Tm, Ts, IndR);
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
                    Tm = 10000;
                    for (int i = 0; i < ArrS.Length; i++)
                    {
                        ArrS[i].L = 0;
                        ArrS[i].T = "";
                        ArrS[i].W = "";
                    }
                    Arr.Clear();
                    for (int i = 0; i < Rules.Length; i++)
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
                    Tm = 10000;
                    for (int i = 0; i < ArrS.Length; i++)
                    {
                        ArrS[i].L = 0;
                        ArrS[i].T = "";
                        ArrS[i].W = "";
                    }
                    Arr.Clear();
                    for (int i = 0; i < Rules.Length; i++)
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
            int iii, pr2;
            pr2 = 0;
            for (iii = 0; iii < dopstr.Length; iii++)
            {
                if (str2[k + 1] == dopstr[iii]) pr2 = pr2 + 1;
            }
            if (pr2 == 0) return 0;
            else return 1;
        }

        public int IsThisNumber(string str1, int nach1, int kon1)
        {
            int jjj, pr3;
            if (str1.Substring(nach1, kon1 + 1 - nach1) == " true" || str1.Substring(nach1, kon1 + 1 - nach1) == " false")
            {
                Arr.Add(new MyWord(17, str1.Substring(nach1, kon1 + 1 - nach1)));
                return 1;
            }
            else
            if (IsNumber(str1, "0123456789", nach1) == 1)
            {
                pr3 = 0;
                for (jjj = nach1; jjj < kon1; jjj++)
                {
                    if (IsNumber(str1, "0123456789.Ee-", jjj) == 1) pr3 = pr3 + 1;
                }
                if (pr3 == kon1 - nach1)
                {
                    Arr.Add(new MyWord(17, str1.Substring(nach1, kon1 + 1 - nach1)));

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
        public int IsThisOperator(string str1, int nach1, int kon1)
        {
            int ii, jj, kol;
            if (str1[nach1] == ' ') nach1 += 1;
            for (ii = 6; ii < 16; ii++)
            {
                if (kon1 - nach1 + 1 == (ArrWords[ii].W).Length)
                {
                    kol = 0;
                    for (jj = 1; jj <= (ArrWords[ii].W).Length; jj++)
                    {
                        if (str1[nach1 + jj - 1] == ArrWords[ii].W[jj - 1]) kol = kol + 1;
                    }
                    if (kon1 - nach1 + 1 == kol)
                    {
                        Arr.Add(new MyWord(ii, ArrWords[ii].W));
                        return ii;
                    }
                }
            }
            return 0;
        }

        public void Up()
        {
            int i, nach, probel, dop1, k = 0;
            ArrS[1].T = "";
            richTextBox2.Clear();
            textBox2.Clear();
            Str += textBox1.Text;
            Str += " ";
            IndR = 0;
            nach = 0;
            probel = 1;
            for (i = 0; i < Str.Length; i++)
            {
                if (Str[i] == ' ')
                    if (probel == 0)
                    {
                        probel = 1;
                        if (IsThisOperator(Str, nach, i - 1) == 0)
                        {
                            dop1 = IsThisNumber(Str, nach, i - 1);
                            if (dop1 == 0)
                            {
                                if ((Str.Substring(nach + 1, i - nach - 1)).Length <= 8 && (Str.Substring(nach + 1, i - nach - 1)).Length > 0)
                                {
                                    Arr.Add(new MyWord(16, Str.Substring(nach, i + 1 - nach)));
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
                Arr.Add(new MyWord(18, "$"));
                Algorithm();
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            ArrStr = new MyUpStr[1000];
            for (int i = 0; i < ArrStr.Length; i++)
            {
                ArrStr[i].I = 0;
                ArrStr[i].Name = "";
            }
            for (int i = 0; i < ArrM.Length; i++)
            {
                ArrM[i] = "";
            }
            Str = "";
            
            for (int i = 0; i < ArrS.Length; i++)
            {
                ArrS[i].L = 0;
                ArrS[i].T = "";
                ArrS[i].W = "";
            }
            Arr.Clear();
            for (int i = 0; i < Rules.Length; i++)
            {
                Rules[i] = 0;
            }
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
