using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework_2105_guessnum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Tick += (s, e) =>
            {
                label8.Text = $"{_starttime.Hour:00}:{_starttime.Minute:00}:{_starttime.Second:00}";
                _starttime = _starttime.AddSeconds(1);
            };
            StartNewGame();
            timer1.Start();
        }

        void GenerateNumber()
        {
            Random rand = new Random();
            _cnumber.Clear();
            int n = 0;
            #region
            //вариант1
            while (n < q)
            {
                int num = rand.Next(1, 10);
                if (!_cnumber.Contains(num))
                {
                    _cnumber.Add(num);
                    n++;
                }
            }
            #endregion

            #region
            //вариант 2
            //List<int> tmprndlist=new List<int>();
            //for (int i = 1; i < 10; i++)
            //{
            //    tmprndlist.Add(i);
            //}
            //for (int i = 0; i < 8; i++)
            //{
            //    int j = rand.Next(i + 1);
            //    var temp = tmprndlist[j];
            //    tmprndlist[j] = tmprndlist[i];
            //    tmprndlist[i] = temp;
            //}
            //for (int i = 0; i < q; i++)
            //{
            //    _cnumber.Add(tmprndlist[i]);
            //}
            #endregion
        }
        void StringToList()
        {
            int unum = int.Parse(_usertext);
            _unumber.Clear();
            for (int i = q-1; i >=0; i--)
            {
                _unumber.Add((int)(unum / Math.Pow(10, i)));
                unum = (int)(unum % Math.Pow(10, i));
            }
        }
        bool CheckUserNumDup()
        {
            IEnumerable<int> unumbers = _unumber.Distinct();
            if (unumbers.Count() == _unumber.Count)
            {
                return true;
            }
            else
            {
                return false;
            }    
        }
        void Contains()
        {
            _qcontainspos = _qcontainsnumber = 0;
            List<int> containspos = new List<int>();
            for (int i = 0; i < q; i++)
            {
                if (_cnumber.Contains(_unumber[i]))
                { 
                    _qcontainsnumber++;
                    containspos.Add(i);
                }
            }
            for (int i = 0; i < containspos.Count; i++)
            {
                if (_cnumber[containspos[i]] == _unumber[containspos[i]])
                    _qcontainspos++;
            }
        }
        void StartNewGame()
        {
            _cnumber.Clear();
            _unumber.Clear();
            _qcontainspos = 0;
            _qcontainsnumber = 0;
            _qtry = 0;
            label5.Text = _qcontainspos.ToString();
            label6.Text = "0";
            label7.Text = _qcontainsnumber.ToString();
            label10.Text = _qtry.ToString();
            _starttime = new DateTime(1, 1, 1, 0, 0, 0, 0);
            listBox1.Items.Clear();
            listBox1.Items.Add($" Число \t    Совпало Цифр\t       Совпало позиций");
            GenerateNumber();
            textBox1.Text = "";
        }


        List<int> _cnumber = new List<int>();
        List<int> _unumber = new List<int>();
        DateTime _starttime = new DateTime(1, 1, 1, 0, 0, 0, 0);
        int q = 3;
        string _usertext = String.Empty;
        int _qcontainspos;
        int _qcontainsnumber;
        int _qtry;

        

        private void button1_Click(object sender, EventArgs e)
        {
            _usertext = textBox1.Text;
            StringToList();
            if (!CheckUserNumDup())
            {
                MessageBox.Show("Число должно состоять только из уникальных цифр", "Ошибка");
                textBox1.Text = "";
            }
            else if (_usertext.Length < q)
            {
                MessageBox.Show($"Количество введенных чисел меньше {q}", "Ошибка");
                listBox1.Items.Add($" Число \t    Совпало Цифр\t       Совпало позиций");
            }
            else
            {
                label6.Text = textBox1.Text;
                _qtry++;
                Contains();
                listBox1.Items.Add($"{_usertext}\t\t{_qcontainsnumber}\t\t {_qcontainspos}");
                label5.Text = _qcontainspos.ToString();
                label7.Text = _qcontainsnumber.ToString();
                label10.Text = _qtry.ToString();
                textBox1.Text = "";
                if (q == _qcontainspos && q == _qcontainsnumber)
                {
                    MessageBox.Show($" Вы победили! Компьютер загадал число {_usertext}\n Попыток: {_qtry}\n Затрачено Времени: {label8.Text}\n", "Поздравляем!");
                    textBox1.Text = "";
                    StartNewGame();
                }
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '\b') || textBox1.TextLength >= q)
                e.Handled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            q = 3;
            GenerateNumber();
            StartNewGame();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            q = 5;
            GenerateNumber();
            StartNewGame();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            q = 7;
            GenerateNumber();
            StartNewGame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }
    }
}
