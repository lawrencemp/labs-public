using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace dekker_cs
{
    class Resourse
    {
        public static void IsInCrit(string a, string nota, Thread_info res)
        {
            Console.WriteLine("Процесс №{0} находится в критическом участке", a);
            while (res.flag[Convert.ToInt32(a)] != 4)
            { }
            Console.WriteLine("Процесс №{0} выходит из критической зоны, IsUsed равно 0", a);
        }
    }
    class Thread_info
    {
        public int[] flag = new int[2] { 0, 0 };
        int isUsed;
        public int IsUsed
        {
            get
            {
                return isUsed;
            }
            set
            {
                isUsed = value;
            }

        }
        public Thread_info()
        {
            isUsed = 0;
        }


    }
    class Program
    {
        public static void reader(string s, Thread_info res)
        {
            if (s == "0" || s == "1")
                res.flag[Convert.ToInt32(s)] += 1;
            else Console.WriteLine("Неверный ввод");
        }
        public static void FuncThread(Thread_info res, string a, string nota)
        {
            while (res.flag[Convert.ToInt32(a)] == 0)
                Thread.Sleep(10);
            while (res.flag[Convert.ToInt32(a)] >= 1)
            {
                Console.WriteLine("Поток № {0} работает", a);
                while (res.flag[Convert.ToInt32(a)] != 2)
                    Thread.Sleep(10);
                Console.WriteLine("Процесс №{0} пытается войти в критическую зону", a);
                if (res.IsUsed == 1)
                {
                    Console.WriteLine("Общий ресурс недоступен");
                    res.flag[Convert.ToInt32(a)]--;
                    continue;
                }
                while (res.flag[Convert.ToInt32(a)] != 3)
                    Thread.Sleep(10);
                if (res.IsUsed == 1)
                {
                    Console.WriteLine("Общий ресурс недоступен");
                    res.flag[Convert.ToInt32(a)] -= 2;
                    continue;
                }
                if (res.flag[Convert.ToInt32(nota)] == 0 || res.flag[Convert.ToInt32(nota)] == 1 ||
                    res.flag[Convert.ToInt32(nota)] == 2 || Program.Priority == Convert.ToInt32(a))
                {
                    res.IsUsed = 1;
                    Resourse.IsInCrit(a, nota, res);
                    res.IsUsed = 0;
                    res.flag[Convert.ToInt32(a)] -= 3;
                }
                else
                    res.flag[Convert.ToInt32(a)]--;
            }
        }
        public static int Priority;
        static void Main(string[] args)
        {
            string t1 = "0";
            string t2 = "1";
            Thread_info res = new Thread_info();
            Console.WriteLine("Введите номер приоритетного потока");
            Program.Priority = Convert.ToInt32(Console.ReadLine());
            Thread th1 = new Thread(new ThreadStart(() => Program.FuncThread(res, t1, t2)));
            th1.Name = t1;
            Thread th2 = new Thread(new ThreadStart(() => Program.FuncThread(res, t2, t1)));
            th2.Name = t2;
            th1.Start();
            th2.Start();
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(20);
                Console.Write("vvod: ");
                reader(Console.ReadLine(), res);
            }
        }
    }
}

