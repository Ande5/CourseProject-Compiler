﻿namespace LR
{
    /// <summary>
    /// Структура символов грамматики
    /// </summary>
    public class MyWord
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
}