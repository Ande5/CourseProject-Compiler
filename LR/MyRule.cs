namespace LR
{
    /// <summary>
    /// Структура правила
    /// </summary>
    public struct MyRule
    {
        public MyRule(int ruleNumber, int[] ruleList)
        {
            RuleNumber = ruleNumber;
            RuleList = ruleList;
        }

        public int RuleNumber;
        public int CountOfWords => RuleList.Length;
        public int[] RuleList;
    }
}

