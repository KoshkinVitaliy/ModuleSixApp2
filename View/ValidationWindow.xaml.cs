using ModuleSixApp2.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ModuleSixApp2.View
{
    /// <summary>
    /// Логика взаимодействия для ValidationWindow.xaml
    /// </summary>
    public partial class ValidationWindow : Window
    {
        public ValidationWindow()
        {
            InitializeComponent();
        }

        private async void Get_Data_Button_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://localhost:4444/TransferSimulator/fullName";

            string data = await new ServerRequest().GetRequest(url);

            DataTextBlock.Text = GetDataFromJSON(data);
        }

        private string GetDataFromJSON(string data)
        {
            return data
                .Substring(data.IndexOf(":") + 2)
                .Replace("\"", "")
                .Replace("}", "");
        }

        private void Send_Result_Button_Click(object sender, RoutedEventArgs e)
        {
            if(DataTextBlock.Text.Equals(""))
            {
                MessageBox.Show("Данные с сервера ещё не получены!");
            }
            else
            {
                //записывать результаты теста
                string fullNamePattern = @"^[А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+$";

                CheckRegex(fullNamePattern, DataTextBlock.Text);
            }
        }

        private void CheckRegex(string fullNamePattern, string text)
        {
            if(!Regex.IsMatch(text, fullNamePattern))
            {
                WordWriter.WriteToWord(
                  "C:\\Users\\admin\\source\\repos\\ModuleSixApp2\\ТестКейс.docx",
                  1,
                  1,
                  DataTextBlock.Text);

                ResultTextBlock.Text = "Данные ФИО провалили валидацию!";
                ResultTextBlock.Foreground = Brushes.Red;
            }
            else
            {
                WordWriter.WriteToWord(
                 "C:\\Users\\admin\\source\\repos\\ModuleSixApp2\\ТестКейс.docx",
                 1,
                 1,
                 DataTextBlock.Text);

                ResultTextBlock.Text = "Данные ФИО успешно прошли валидацию!";
                ResultTextBlock.Foreground = Brushes.Green;
            }    
        }
    }
}
