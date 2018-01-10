using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR.Data
{
    public class UpTableLoader
    {
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
