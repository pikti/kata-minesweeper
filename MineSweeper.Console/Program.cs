using System;
using System.Collections.Generic;
using MineSweeper.Services;

namespace MineSweeper.IO
{
    class Program
    {
        static void Main(string[] args)
        {
            var mineSweeper = new MineSweeperService();
            Console.WriteLine(mineSweeper.GetFields(new List<string[]>()
                {
                    new string[]{"4 4", "..**", "..*.", "**..", "...*"}
                }));
        }
    }
}
