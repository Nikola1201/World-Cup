﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i <5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.WriteLine("i= "+i);
                    Console.WriteLine("j= "+j);
                }
            }
            Console.ReadLine();

        }
    }
}
