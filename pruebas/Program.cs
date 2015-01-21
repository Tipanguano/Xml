using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese dias de caducidad");
            string a =Console.ReadLine();
            int dias=Convert.ToInt32(a);
            DateTime nuevafecha;
            DateTime fecha=DateTime.Now;

            Console.WriteLine(DateTime.Now.ToString("d"));
            Console.WriteLine(DateTime.Now.ToString("D"));

            Console.WriteLine(DateTime.Now.AddDays(dias).ToString("D"));

            Console.ReadKey();
        }
    }
}
