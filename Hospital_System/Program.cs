using System;
using Hospital_System.Models;

namespace Hospital_System
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (SerializeToFIle.loadAll("hospital.xml"))
            {
                Console.WriteLine("Data saved and loaded successfully.");
            }
            else
            {
                Console.WriteLine("Error occurred while loading data.");
            }
        }
    }
}