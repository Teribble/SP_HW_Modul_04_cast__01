using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task4
{
    public partial class Form1 : Form
    {
        Mutex mutex;

        public Form1()
        {
            InitializeComponent();

            mutex = new Mutex();
        }

        private void TextChangedTextBox1(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox1.Text))
            {
                label2.Visible = true;

                label2.Text = "      Доступно";

                label2.ForeColor = Color.Green;

                button1.Enabled = true;
            }
            else
            {
                label2.Visible = true;

                label2.Text = "Не Доступно";

                label2.ForeColor = Color.Red;

                button1.Enabled = false;
            }
        }

        private void OnButton1Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            button3.Enabled = false;

            button4.Enabled = false;

            new Thread(GenerateFile).Start();

            Thread.Sleep(500);

            new Thread(ReadAndAnalisFile).Start();
            
            Thread.Sleep(500);

            new Thread(ReadAndAnalisFile1).Start();
        }

        private void GenerateFile()
        {
            mutex.WaitOne();

            string message = string.Empty;

            string path = textBox1.Text;

            string fileName = "text.txt";

            for (int i = 0; i <= 1000; i++)
            {
                if (i % 10 is 0)
                {
                    label4.Invoke(new Action(() =>
                    {
                        label4.Text = (i / 10).ToString();
                    }));
                }

                message += new Random().Next(1, 7000) + " ";

                Thread.Sleep(1);
            }

            using (var sw = new StreamWriter(path + "\\" + fileName))
            {
                sw.WriteLine(message);
            }

            listBox1.Invoke(new Action(() =>
            {
                listBox1.Items.Add("Числа добавлены в файл");
            }));

            button2.Invoke(new Action(() =>
            {
                button2.Enabled = true;
            }));

            mutex.ReleaseMutex();
        }

        private void OnButton2Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text + "\\" + "text.txt"))
            {
                Process.Start(textBox1.Text + "\\" + "text.txt");
            }
            else
            {
                MessageBox.Show("Файла не существует");
            }
        }

        private bool IsPrime(int number)
        {
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        private void ReadAndAnalisFile()
        {
            mutex.WaitOne();

            string message = string.Empty;

            string path = textBox1.Text;

            string newName = "text1.txt";

            string fileName = "text.txt";

            using (var sr = new StreamReader(path + "\\" + fileName))
            {
                message = sr.ReadToEnd();
            }

            var newMessage = message.Split(' ').ToList();

            var num = new List<int>();

            for (int i = 0; i < newMessage.Count; i++)
            {
                if (Int32.TryParse(newMessage[i], out int a))
                {
                    if (IsPrime(a))
                    {
                        num.Add(a);
                    }
                }
            }

            num.Sort();

            message = String.Empty;

            foreach (var item in num)
            {
                message += item + " ";
            }

            using (var sw = new StreamWriter(path + "\\" + newName))
            {
                sw.WriteLine(message);
            }

            listBox1.Invoke(new Action(() =>
            {
                listBox1.Items.Add($"Анализ выполнен, найдено {num.Count()} простых чисел");
            }));

            button3.Invoke(new Action(() =>
            {
                button3.Enabled = true;
            }));

            mutex.ReleaseMutex();
        }
        private void ReadAndAnalisFile1()
        {
            mutex.WaitOne();
            int count = 0;

            string message = string.Empty;

            string path = textBox1.Text;

            string newName = "text2.txt";

            string fileName = "text.txt";

            using (var sr = new StreamReader(path + "\\" + fileName))
            {
                message = sr.ReadToEnd();
            }

            var newMessage = message.Split(' ').ToList();

            var num = new List<int>();

            for (int i = 0; i < newMessage.Count; i++)
            {
                if (Int32.TryParse(newMessage[i], out int a))
                {
                    if (IsPrime(a))
                    {
                        num.Add(a);
                    }
                }
            }

            num.Sort();

            message = String.Empty;

            foreach (var item in num)
            {
                if (item % 10 == 7)
                {
                    message += item + " ";

                    count++;
                }
            }

            using (var sw = new StreamWriter(path + "\\" + newName))
            {
                sw.WriteLine(message);
            }

            listBox1.Invoke(new Action(() =>
            {
                listBox1.Items.Add($"Анализ выполнен, найдено {count} чисел, последняя цифра которых 7");
            }));

            button4.Invoke(new Action(() =>
            {
                button4.Enabled = true;
            }));

            mutex.ReleaseMutex();
        }

        private void OnButton3Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text + "\\" + "text1.txt"))
            {
                Process.Start(textBox1.Text + "\\" + "text1.txt");
            }
            else
            {
                MessageBox.Show("Файла не существует");
            }
        }

        private void OnButton4Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text + "\\" + "text2.txt"))
            {
                Process.Start(textBox1.Text + "\\" + "text2.txt");
            }
            else
            {
                MessageBox.Show("Файла не существует");
            }
        }
    }
}
