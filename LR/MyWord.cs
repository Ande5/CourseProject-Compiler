namespace LR
{
    /// <summary>
    /// Структура символов грамматики
    /// </summary>
    public class MyWord
    {
        public MyWord(int number, string value)
        {
            Number = number;
            Value = value;
            T = "";
        }

        public int Number;
        public string Value, T;
    }
}
