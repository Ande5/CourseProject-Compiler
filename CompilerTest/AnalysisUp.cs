using System;
using System.Windows.Forms;

namespace Colibri_compiler
{
    class AnalysisUp
    {
        RichTextBox richTextBox2;
        TextBox textBox2;
        public string str = "";
        public struct MyWord
        {
            public int l;
            public string w, t;
        }
        public struct MyRule
        {
            public int p, l;
            public int[] m;
        }
        public int indarr, indR;
        public MyWord[] arr = new MyWord[1000];
        public MyWord[] arrS = new MyWord[1000];
        
        public MyRule[] Rule = new MyRule[16];
        public MyWord[] arrWords = new MyWord[19];
        public int[] Rules = new int[1000];
        public int[,] arrZ = new int[19, 19]  {{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                                               {0,0,0,0,1,2,0,0,0,0,1,1,1,1,1,1,1,1,0},
                                               {0,1,0,2,0,0,0,1,0,2,0,0,0,0,0,0,0,0,0},
                                               {2,1,0,2,0,0,0,1,0,0,0,0,0,0,0,0,0,0,3},
                                               {0,0,0,0,1,2,0,0,0,0,1,1,1,1,1,1,1,1,0},
                                               {0,0,0,0,0,0,2,3,0,3,3,3,3,3,3,3,3,3,3},
                                               {0,0,2,0,1,1,0,0,0,0,1,1,1,1,1,1,1,1,0},
                                               {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0},
                                               {0,0,2,0,1,1,0,0,0,0,1,1,1,1,1,1,1,1,0},
                                               {0,0,0,0,0,0,3,3,0,3,3,3,3,3,3,3,3,3,3},
                                               {0,0,0,0,1,2,0,0,0,0,1,1,1,1,1,1,1,1,0},
                                               {0,0,0,0,1,2,0,0,0,0,1,1,1,1,1,1,1,1,0},
                                               {0,0,0,0,1,2,0,0,0,0,1,1,1,1,1,1,1,1,0},
                                               {0,0,0,0,1,2,0,0,0,0,1,1,1,1,1,1,1,1,0},
                                               {0,0,0,0,1,2,0,0,0,0,1,1,1,1,1,1,1,1,0},
                                               {0,0,0,0,1,2,0,0,0,0,1,1,1,1,1,1,1,1,0},
                                               {0,0,0,0,0,0,3,3,2,3,3,3,3,3,3,3,3,3,3},
                                               {0,0,0,0,0,0,3,3,0,3,3,3,3,3,3,3,3,3,3},
                                               {0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0}};
        public void Initalize()
        {
            MyRule myElem = new MyRule();
            //S->w if Y S
            int[] b = { 3, 6, 2, 0 };
            myElem.p = 1;
            myElem.l = 4;
            myElem.m = b;
            Rule[0] = myElem;
            //
            myElem.p = 1;
            myElem.l = 7;
            int[] c = { 3, 6, 2 };
            myElem.m = c;
            Rule[1] = myElem;
            myElem.p = 1;
            myElem.l = 3;
            int[] d = { 7, 16, 0, 0, 0, 0 };
            myElem.m = d;
            Rule[2] = myElem;
            myElem.p = 2;
            myElem.l = 5;
            int[] q = { 7, 16, 8, 2, 9, 0 };
            myElem.m = q;
            Rule[3] = myElem;
            myElem.p = 3;
            myElem.l = 1;
            int[] w = { 5, 0, 0, 0, 0, 0 };
            myElem.m = w;
            Rule[4] = myElem;
            myElem.p = 4;
            myElem.l = 2;
            int[] e = { 1, 5, 0, 0, 0, 0 };
            myElem.m = e;
            Rule[5] = myElem;
            myElem.p = 6;
            myElem.l = 1;
            int[] a = { 16, 0, 0, 0, 0, 0 };
            myElem.m = a;
            Rule[6] = myElem;
            myElem.p = 6;
            myElem.l = 1;
            int[] a1 = { 17, 0, 0, 0, 0, 0 };
            myElem.m = a1;
            Rule[7] = myElem;
            myElem.p = 6;
            myElem.l = 4;
            int[] a2 = { 16, 8, 2, 9, 0, 0 };
            myElem.m = a2;
            Rule[8] = myElem;
            myElem.p = 6;
            myElem.l = 2;
            int[] a3 = { 11, 5, 0, 0, 0 };
            myElem.m = a3;
            Rule[9] = myElem;
            myElem.p = 6;
            myElem.l = 2;
            int[] a4 = { 10, 5, 0, 0, 0, 0 };
            myElem.m = a4;
            Rule[10] = myElem;
            myElem.p = 6;
            myElem.l = 2;
            int[] a5 = { 4, 5, 0, 0, 0, 0 };
            myElem.m = a5;
            Rule[11] = myElem;
            myElem.p = 5;
            myElem.l = 2;
            int[] a6 = { 14, 5, 0, 0, 0, 0 };
            myElem.m = a6;
            Rule[12] = myElem;
            myElem.p = 5;
            myElem.l = 2;
            int[] a7 = { 15, 5, 0, 0, 0, 0 };
            myElem.m = a7;
            Rule[13] = myElem;
            myElem.p = 5;
            myElem.l = 2;
            int[] a8 = { 12, 5, 0, 0, 0, 0 };
            myElem.m = a8;
            Rule[14] = myElem;
            myElem.p = 5;
            myElem.l = 2;
            int[] a9 = { 13, 5, 0, 0, 0, 0 };
            myElem.m = a9;
            Rule[15] = myElem;

            MyWord myElemw = new MyWord();
            myElemw.l = 1;
            myElemw.w = "S";
            arrWords[0] = myElemw;
            myElemw.l = 1;
            myElemw.w = "X";
            arrWords[1] = myElemw;
            myElemw.l = 1;
            myElemw.w = "Y";
            arrWords[2] = myElemw;
            myElemw.l = 1;
            myElemw.w = "w";
            arrWords[3] = myElemw;
            myElemw.l = 1;
            myElemw.w = "R";
            arrWords[4] = myElemw;
            myElemw.l = 1;
            myElemw.w = "A";
            arrWords[5] = myElemw;
            myElemw.l = 1;
            myElemw.w = "for";
            arrWords[6] = myElemw;
            myElemw.l = 1;
            myElemw.w = "set";
            arrWords[7] = myElemw;
            myElemw.l = 1;
            myElemw.w = "[";
            arrWords[8] = myElemw;
            myElemw.l = 1;
            myElemw.w = "]";
            arrWords[9] = myElemw;
            myElemw.l = 1;
            myElemw.w = "not";
            arrWords[10] = myElemw;
            myElemw.l = 1;
            myElemw.w = "sqrt";
            arrWords[11] = myElemw;
            myElemw.l = 1;
            myElemw.w = "neq";
            arrWords[12] = myElemw;
            myElemw.l = 1;
            myElemw.w = "or";
            arrWords[13] = myElemw;
            myElemw.l = 1;
            myElemw.w = "mult";
            arrWords[14] = myElemw;
            myElemw.l = 1;
            myElemw.w = "div";
            arrWords[15] = myElemw;
            myElemw.l = 1;
            myElemw.w = "id";
            arrWords[16] = myElemw;
            myElemw.l = 1;
            myElemw.w = "const";
            arrWords[17] = myElemw;
            myElemw.l = 1;
            myElemw.w = "$";
            arrWords[18] = myElemw;
        }
        //Мето выввода результата
        public void StartUP(RichTextBox richtext, TextBox text, string text_start)
        {
            richTextBox2 = richtext;
            textBox2 = text;
            //richTextBox2.BeginInvoke(new MethodInvoker(delegate
            //{
            //    richTextBox2 = richtext;
            //}));
            //textBox2.BeginInvoke(new MethodInvoker(delegate
            //{

            //    textBox2 = text;
            //}));
            Initalize();
            for (int i = 0; i < arrS.Length; i++)
            {
                arrS[i].l = 0;
                arrS[i].t = "";
                arrS[i].w = "";
            }
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].l = 0;
                arr[i].t = "";
                arr[i].w = "";
            }
            for (int i = 0; i < Rules.Length; i++)
            {
                Rules[i] = 0;
            }
            str = "";
            Up(text_start);
        }
        public void Print(int yy, int zz, int xx)
        {
            int i2;
            string s1, s2, s3;
            s1 = "";
            i2 = yy;
            while (i2 <= indarr)
            {
                s1 = s1 + arrWords[arr[i2].l].w + " ";
                i2 = i2 + 1;
            }
          //  richTextBox2.Text += "Строка:" + s1 + '\n';
            richTextBox2.BeginInvoke(new MethodInvoker(delegate
            {
                richTextBox2.Text += "Строка:" + s1 + '\n';
            }));
           
            s2 = "";
            i2 = 0;
            while (i2 <= zz)
            {
                s2 = s2 + arrWords[arrS[i2].l].w + " ";
                i2 = i2 + 1;
            }
            richTextBox2.BeginInvoke(new MethodInvoker(delegate
            {
                richTextBox2.Text += "Магазин:" + s2 + '\n';
            }));
            //richTextBox2.Text += "Магазин:" + s2 + '\n';
            s3 = "";
            i2 = 1;
            while (i2 <= xx)
            {
                s3 = s3 + (Rules[i2] + 1).ToString() + " ";
                i2 = i2 + 1;
            }
            richTextBox2.BeginInvoke(new MethodInvoker(delegate
            {
                richTextBox2.Text += "Правила:" + s3 + '\n';
                richTextBox2.Text += "      " + '\n';
            }));
           // richTextBox2.Text += "Правила:" + s3 + '\n';
          //  richTextBox2.Text += "      " + '\n';
        }

        public void MyCompil(int pp, int ts1)
        {
            pp = pp + 1;
            if (pp == 1)
                arrS[ts1 - 5].t = "for ( " + arrS[ts1 - 5].t + arrS[ts1 - 4].t + "; " + arrS[ts1 - 2].t + "; " + arrS[ts1 - 1].t + " ) {" + arrS[ts1].t + "} ;";
            if (pp == 2)
                arrS[ts1 - 6].t = "for ( " + arrS[ts1 - 6].t + arrS[ts1 - 5].t + "; " + arrS[ts1 - 3].t + "; " + arrS[ts1 - 2].t + " ) {" + arrS[ts1 - 1].t + "} ;" + Environment.NewLine + arrS[ts1].t;
            if (pp == 4)
                arrS[ts1 - 4].t = arrS[ts1 - 3].w + "[ " + arrS[ts1 - 1].t + " ] = ";
            if (pp == 3)
                arrS[ts1 - 1].t = arrS[ts1].w + " = ";
            if (pp == 6)
                arrS[ts1 - 1].t = arrS[ts1 - 1].t + " " + arrS[ts1].t;
            if (pp == 8)
                arrS[ts1].t = arrS[ts1].w;
            if (pp == 7)
                arrS[ts1].t = arrS[ts1].w;
            if (pp == 11)
                arrS[ts1 - 1].t = "! ( " + arrS[ts1].t + " )";
            if (pp == 10)
                arrS[ts1 - 1].t = "sqrt ( " + arrS[ts1].t + " )";
            if (pp == 12)
                arrS[ts1 - 1].t = arrS[ts1 - 1].t + " " + arrS[ts1].t;
            if (pp == 9)
                arrS[ts1 - 3].t = arrS[ts1 - 3].w + "[ " + arrS[ts1 - 1].t + " ] ";
            if (pp == 15)
                arrS[ts1 - 1].t = arrS[ts1].t + " != ";
            if (pp == 16)
                arrS[ts1 - 1].t = arrS[ts1].t + " ^ ";
            if (pp == 13)
                arrS[ts1 - 1].t = arrS[ts1].t + " * ";
            if (pp == 14)
                arrS[ts1 - 1].t = arrS[ts1].t + " / ";

        }
        public void algorithm()
        {
            int Tm, Ts, koli, p10, pr11, pr12;
            Tm = 0;
            Ts = 0;
            int go = 0;
            arrS[0].l = 18;
            arrS[0].w = "$";
            indR = 0;
            Print(Tm, Ts, indR);
            while (Tm <= indarr)
            {
                if (arr[Tm].l == 18)
                {
                    if (Ts == 1)
                    {
                        if ((arrS[Ts].l == 0) && (arrS[Ts - 1].l == 18))
                        {
                            go = 4;
                        }
                    }
                }
                if (((arrZ[arrS[Ts].l, arr[Tm].l] == 1) && go != 4) || ((arrZ[arrS[Ts].l, arr[Tm].l] == 2) && go != 4))
                {
                    Ts = Ts + 1;
                    arrS[Ts].l = arr[Tm].l;
                    arrS[Ts].w = arr[Tm].w;
                    Tm = Tm + 1;
                    Print(Tm, Ts, indR);
                    go = 2;
                }
                if ((arrZ[arrS[Ts].l, arr[Tm].l] == 3) && go != 2 && go != 4)
                {
                    koli = 0;
                    while (arrZ[arrS[Ts - koli].l, arrS[Ts - koli + 1].l] != 1)
                    {
                        koli = koli + 1;
                    }
                    for (p10 = 0; p10 < 16; p10++)
                    {
                        if (Rule[p10].l == koli && go != 2)
                        {
                            pr11 = 0;
                            for (pr12 = 0; pr12 < koli; pr12++)
                            {
                                if (Rule[p10].m[pr12] == arrS[Ts - koli + 1 + pr12].l)
                                {
                                    pr11 = pr11 + 1;
                                }
                            }
                            if (pr11 == koli)
                            {
                                MyCompil(p10, Ts);
                                Ts = Ts - koli + 1;
                                arrS[Ts].l = Rule[p10].p - 1;
                                arrS[Ts].w = arrWords[(arrS[Ts].l)].w;
                                indR = indR + 1;
                                Rules[indR] = p10;
                                Print(Tm, Ts, indR);
                                go = 2;
                            }
                        }
                    }
                }
                if (go != 2 && go != 4)
                {

                   // richTextBox2.Text = "Ошибка при выполнении восходящего разбора!";
                    //textBox2.Text = "";
                    richTextBox2.BeginInvoke(new MethodInvoker(delegate
                    {
                        richTextBox2.Text = "Ошибка при выполнении восходящего разбора!";
                    }));
                    textBox2.BeginInvoke(new MethodInvoker(delegate
                    {

                        textBox2.Text = "";
                    }));
                    go = 3;
                    Tm = 10000;
                    for (int i = 0; i < arrS.Length; i++)
                    {
                        arrS[i].l = 0;
                        arrS[i].t = "";
                        arrS[i].w = "";
                    }
                    for (int i = 0; i < arr.Length; i++)
                    {
                        arr[i].l = 0;
                        arr[i].t = "";
                        arr[i].w = "";
                    }
                    for (int i = 0; i < Rules.Length; i++)
                    {
                        Rules[i] = 0;
                    }
                    str = "";
                }
                if (go != 3)
                {
                   // textBox2.Text = arrS[1].t;
                    textBox2.BeginInvoke(new MethodInvoker(delegate
                    {

                        textBox2.Text = arrS[1].t;
                    }));
                }
                if (go == 4)
                {
                    Tm = 10000;
                    for (int i = 0; i < arrS.Length; i++)
                    {
                        arrS[i].l = 0;
                        arrS[i].t = "";
                        arrS[i].w = "";
                    }
                    for (int i = 0; i < arr.Length; i++)
                    {
                        arr[i].l = 0;
                        arr[i].t = "";
                        arr[i].w = "";
                    }
                    for (int i = 0; i < Rules.Length; i++)
                    {
                        Rules[i] = 0;
                    }
                    str = "";
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
            if (str1.Substring(nach1, kon1 + 1 - nach1) == " true" ||
                str1.Substring(nach1, kon1 + 1 - nach1) == " false")
            {
                MyWord myElemwt = new MyWord();
                myElemwt.l = 17;
                myElemwt.w = str1.Substring(nach1, kon1 + 1 - nach1);
                arr[indarr] = myElemwt;
                indarr = indarr + 1;
                return 1;
            }
            else if (IsNumber(str1, "0123456789", nach1) == 1)
            {
                pr3 = 0;
                for (jjj = nach1; jjj < kon1; jjj++)
                {
                    if (IsNumber(str1, "0123456789.Ee-", jjj) == 1) pr3 = pr3 + 1;
                }
                if (pr3 == kon1 - nach1)
                {
                    MyWord myElemwt = new MyWord();
                    myElemwt.l = 17;
                    myElemwt.w = str1.Substring(nach1, kon1 + 1 - nach1);
                    arr[indarr] = myElemwt;
                    indarr = indarr + 1;
                    return 1;
                }
                else
                {
                    MessageBox.Show("Нужно вводить вещественные" + '\n' + " числа с порядком!" + '\r' + "Ошибка --> " +
                                    str1.Substring(nach1, kon1 + 1 - nach1));
                    str = "";
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
        //Метод Оператора
        public int IsThisOperator(string str1, int nach1, int kon1)
        {
            int ii, jj, kol;
            if (str1[nach1] == ' ') nach1 += 1;
            for (ii = 6; ii < 16; ii++)
            {
                if (kon1 - nach1 + 1 == (arrWords[ii].w).Length)
                {
                    kol = 0;
                    for (jj = 1; jj <= (arrWords[ii].w).Length; jj++)
                    {
                        if (str1[nach1 + jj - 1] == arrWords[ii].w[jj - 1]) kol = kol + 1;
                    }
                    if (kon1 - nach1 + 1 == kol)
                    {
                        MyWord myElemwt = new MyWord();
                        myElemwt.l = ii;
                        myElemwt.w = arrWords[ii].w;
                        arr[indarr] = myElemwt;
                        indarr = indarr + 1;
                        return ii;
                    }
                }
            }
            return 0;
        }
        // Метод восходящего разбора
        public void Up(string text)
        {
            int i, nach, probel, dop1, k = 0;
            arrS[1].t = "";
            richTextBox2.BeginInvoke(new MethodInvoker(delegate
                {
                    richTextBox2.Clear();
                }));
            textBox2.BeginInvoke(new MethodInvoker(delegate
            {
                
                textBox2.Clear();
            }));
            str += text;
            str += " ";
            indarr = 0;
            indR = 0;
            nach = 0;
            probel = 1;
            for (i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                    if (probel == 0)
                    {
                        probel = 1;
                        if (IsThisOperator(str, nach, i - 1) == 0)
                        {
                            dop1 = IsThisNumber(str, nach, i - 1);
                            if (dop1 == 0)
                            {
                                if ((str.Substring(nach + 1, i - nach - 1)).Length <= 8 && (str.Substring(nach + 1, i - nach - 1)).Length > 0)
                                {
                                    MyWord myElemwt = new MyWord();
                                    myElemwt.l = 16;
                                    myElemwt.w = str.Substring(nach, i + 1 - nach);
                                    arr[indarr] = myElemwt;
                                    indarr = indarr + 1;
                                }
                                else
                                {
                                    if ((str.Substring(nach + 1, i - nach - 1)).Length > 8)
                                    {
                                        k = 1;
                                        MessageBox.Show("Длина идентификатора не может быть больше 8 символов!" + '\n' + "Ошибка --> " + str.Substring(nach, i + 1 - nach));
                                    }
                                    if ((str.Substring(nach + 1, i - nach - 1)).Length == 0)
                                    {
                                        k = 1;
                                        MessageBox.Show("Длина идентификатора не может быть меньше 0 символов!" + '\n' + "Сивмол № " + (i + 1).ToString() + " является пробелом.");
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
                MyWord myElemwt = new MyWord();
                myElemwt.l = 18;
                myElemwt.w = "$";
                arr[indarr] = myElemwt;
                algorithm();
            }
        }
    }
}
