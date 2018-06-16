﻿using System;

namespace ComposableDelegates2
{
    // using reference parameters enables results to be passed 
    // along from one delegate to the next
    public delegate void MyDelegate(int arg1, ref int arg2);

    class Program
    {
        static void Func1(int arg1, ref int arg2)
        {
            string result = (arg1 + arg2).ToString();
            arg2 += 20; // arg2 is a ref parameter, so this will change it
            Console.WriteLine("The number is: " + result);
        }
        static void Func2(int arg1, ref int arg2)
        {
            string result = (arg1 * arg2).ToString();
            Console.WriteLine("The number is: " + result);
        }

        static void Main(string[] args)
        {
            int a = 10, b = 10;
            MyDelegate f1 = Func1;
            MyDelegate f2 = Func2;
            MyDelegate combined = f1 + f2;

            Console.WriteLine("The value of b is: {0}", b);
            combined(a, ref b);
            Console.WriteLine("The value of b is: {0}", b);

            Console.WriteLine("\nPress Enter Key to Continue...");
            Console.ReadLine();
        }
    }
}
