using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper.Services
{
    public class MineSweeperService
    {
        public string GetFields(List<string[]> input)
        {
            if (input == null) 
                throw new ArgumentNullException("input should not be null");

            var result = new List<string>();
            for (int i = 0; i < input.Count; i++)
            {
                result.Add($"Field #{i+1}:");
                result.Add(GetField(input.ElementAt(i)));
            }

            return string.Join(Environment.NewLine, result);
        }

       private string GetField(string[] input)
        {
            if (input == null) 
                throw new ArgumentNullException("input should not be null");
            if (input.Length < 2) 
                throw new ArgumentException("input should contain a first line with integers and the lines related to the field");
            var fieldSize = input[0].Split(' ');
            int n = 0, m = 0;
            if (fieldSize?.Length != 2 || !int.TryParse(fieldSize[0], out n) || !int.TryParse(fieldSize[1], out m))
                throw new ArgumentException("input should contain a first line with 2 integers");
            if (input.Length != m + 1)
                throw new ArgumentException("After the first line, input should contain as many lines as the second integer");
            var list = input.ToList();
            list.RemoveAt(0);
            if (!list.TrueForAll(l => l.Length == n && l.All(s => ".*".Contains(s))))
                throw new ArgumentException("Each line should contain as many caracters as the first integer, and only . and *");

            // At this point, input is good, let's do it!
            var field = new List<char[]>();
            list.ForEach(s => field.Add(s.ToCharArray()));
            for (int y = 0; y < m; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (field.ElementAt(y)[x] == '.') 
                        field.ElementAt(y)[x] = '0';
                    else if (field.ElementAt(y)[x] == '*')
                    {
                        // We need to increment numbers around
                        if (x < n - 1) 
                        {
                            Count(field, y, x + 1);
                            if (y < m - 1) Count(field, y + 1, x + 1);
                            if (y > 0) Count(field, y - 1, x + 1);
                        }
                        if (x > 0) 
                        {
                            Count(field, y, x - 1);
                            if (y < m - 1) Count(field, y + 1, x - 1);
                            if (y > 0) Count(field, y - 1, x - 1);
                        }
                        if (y < m - 1) Count(field, y + 1, x);
                        if (y > 0) Count(field, y - 1, x);
                    }
                }
            }

            var result = new StringBuilder();
            field.ForEach(ac => result.AppendLine(string.Concat(ac)));
            return result.ToString();
        }

        private void Count(List<char[]> field, int y, int x)
        {
            if (field.ElementAt(y)[x].ToString() == "*") return;
            int temp = int.TryParse(field.ElementAt(y)[x].ToString(), out temp) ? temp + 1 : 1;
            field.ElementAt(y)[x] = Convert.ToChar(temp.ToString());
        }
    }
}
