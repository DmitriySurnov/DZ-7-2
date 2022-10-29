using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zadanie_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum Znacs
        {
            plus = 0, minus = 1, ymnoz = 2, delit = 3,
            pysto = 4, ravno = 5
        }
        private Znacs znac = Znacs.pysto;
        private bool drob = false;
        private string istoriy = "";
        private float namber;
        private bool isnamber = false;
        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement item in (Content as Grid).Children)
            {
                if (item is Button)
                    try
                    {
                        uint namber = Convert.ToUInt32((item as Button).Content);
                        (item as Button).Click += Button_Click;
                    }
                    catch (Exception) { }
            }
        }

        private bool IsPysto(TextBox pole)
        {
            string str = pole.Text;
            if (str.Length == 0)
                return true;
            else
                return false;
        }

        private void Button_toch_Click(object sender, RoutedEventArgs e)
        {
            if (drob)
                return;
            if (IsPysto(textBox_tec) || znac == Znacs.ravno)
                AddNamber(0);
            textBox_tec.Text += ",";
            drob = true;
        }
        private void AddNamber(uint namber)
        {
            if (textBox_tec.Text == "0" || znac == Znacs.ravno)
                textBox_tec.Text = namber.ToString();
            else
                textBox_tec.Text += namber;
            if (znac == Znacs.ravno)
                znac = Znacs.pysto;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNamber(Convert.ToUInt32((sender as Button).Content));
        }
        private void Button_del_Click(object sender, EventArgs e)
        {
            if (znac == Znacs.ravno)
            {
                textBox_tec.Text = "";
                return;
            }
            string str = textBox_tec.Text;
            if (str.Length == 0)
                return;
            textBox_tec.Text = "";
            for (int i = 0; i < str.Length - 1; i++)
                textBox_tec.Text += str[i];
            if (str[str.Length - 1] == ',')
                drob = false;
        }
        private void Button_C_Click(object sender, EventArgs e)
        {
            Stor_tec();
            textBox_histori.Text = "";
        }
        private void Stor_tec()
        {
            textBox_tec.Text = "";
            drob = false;
        }
        private void Button_CE_Click(object sender, EventArgs e)
        {
            Stor_tec();
        }
        private void Button_plus_Click(object sender, EventArgs e)
        {
            Button_znac(" + ", Znacs.plus);
        }
        private void Button_znac(string text, Znacs zn)
        {
            if (IsPysto(textBox_tec))
            {
                if (!IsPysto(textBox_histori) && znac != Znacs.pysto)
                {
                    textBox_histori.Text = istoriy + text;
                    znac = zn;
                }
            }
            else if (isnamber)
            {
                float d = Convert.ToSingle(textBox_tec.Text);
                namber = Deistv(d);
                znac = zn;
                istoriy = textBox_histori.Text;
                istoriy += textBox_tec.Text;
                textBox_histori.Text = istoriy + text;
                Stor_tec();
            }
            else if (znac == Znacs.ravno)
            {
                isnamber = true;
                istoriy += "  " + namber;
                znac = zn;
                textBox_histori.Text = istoriy + text;
                Stor_tec();
            }
            else if (znac == Znacs.pysto)
            {
                namber = Convert.ToSingle(textBox_tec.Text);
                znac = zn;
                istoriy += "  " + textBox_tec.Text;
                textBox_histori.Text = istoriy + text;
                isnamber = true;
                Stor_tec();
            }
        }
        private float Deistv(float namber1)
        {
            switch (znac)
            {
                case 0:
                    return namber + namber1;
                case (Znacs)1:
                    return namber - namber1;
                case (Znacs)2:
                    return namber * namber1;
                case (Znacs)3:
                    return namber / namber1;
                default:
                    return -1;
            }
        }
        private void Button_rovno_Click(object sender, EventArgs e)
        {
            if (znac == Znacs.ravno)
                return;
            else if (isnamber && !IsPysto(textBox_tec))
            {
                float d = Convert.ToSingle(textBox_tec.Text);
                namber = Deistv(d);
                isnamber = false;
                znac = Znacs.ravno;
                istoriy = textBox_histori.Text;
                istoriy += textBox_tec.Text + " = " + namber;
                textBox_histori.Text = istoriy;
                textBox_tec.Text = " = " + namber;
                drob = false;
            }
        }
        private void Button_minus_Click(object sender, EventArgs e)
        {
            Button_znac(" - ", Znacs.minus);
        }
        private void Button_ymnoz_Click(object sender, EventArgs e)
        {
            Button_znac(" * ", Znacs.ymnoz);
        }
        private void Button_delit_Click(object sender, EventArgs e)
        {
            Button_znac(" / ", Znacs.delit);
        }
    }
}
