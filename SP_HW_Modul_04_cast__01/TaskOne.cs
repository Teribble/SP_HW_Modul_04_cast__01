using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_HW_Modul_04_cast__01
{
    class TaskOne
    {
        // создаем семафор
        static Semaphore sem = new Semaphore(3, 3);

        Thread thread;

        int count = 1;

        public TaskOne(int i)
        {
            thread = new Thread(Work);

            thread.Name = $"Работник {i}";

            thread.Start();
        }

        public void Work()
        {
            while (count > 0)
            {
                sem.WaitOne();  // ожидаем, когда освободиться место

                Console.WriteLine($"{Thread.CurrentThread.Name} идет на работу");

                Console.WriteLine($"{Thread.CurrentThread.Name} работает");

                Thread.Sleep(1000);

                Console.WriteLine($"{Thread.CurrentThread.Name} уходит с работы");

                sem.Release();  // освобождаем место

                count--;

                Thread.Sleep(1000);
            }
        }
    }
}
