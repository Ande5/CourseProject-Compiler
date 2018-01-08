namespace LR
{
    /// <summary>
    /// Структура правила
    /// </summary>
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
}

