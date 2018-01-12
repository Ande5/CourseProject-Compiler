namespace LR
{
    /// <summary>
    /// Структура символов грамматики
    /// </summary>
    public class Word
    {
        public Word(int number, string value)
        {
            Number = number;
            Value = value;
            Temp = "";
        }

        public int Number;
        public string Value, Temp;
    }
}
