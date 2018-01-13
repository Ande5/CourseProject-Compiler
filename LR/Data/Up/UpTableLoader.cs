using System;
using System.IO;

namespace LR.Data.Up
{
    public class UpTableLoader
    {
        /// <summary>
        /// Загружает управляющую таблицу из файла
        /// </summary>
        /// <param name="path">Пкть до файла управляющей таблицы</param>
        /// <returns>Управляющая таблица</returns>
        public int[,] LoadTable(string path)
        {
            int[,] arr;
            using (StreamReader sr = new StreamReader(path))
            {
                int count = Int32.Parse(sr.ReadLine());
                arr = new int[count,count];
                for (int row = 0; row < count; row++)
                {
                    string str = sr.ReadLine();
                    for (int col = 0; col < count; col++)
                    {
                        arr[row, col] = Convert.ToInt32(str[col].ToString());
                    }
                }
            }
            return arr;
        }
    }
}
