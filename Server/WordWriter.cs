using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace ModuleSixApp2.Server
{
    public class WordWriter
    {
        private static int rowIndex = 3;

        public static void WriteToWord(
            string filePath,
            int tableIndex,
            int columnIndex,
            string data
            )
        {
            Word.Application application = new Word.Application();
            application.Visible = false;

            try
            {
                Word.Document document = application.Documents.Open(filePath);
                Word.Table table = document.Tables[tableIndex];

                if(columnIndex < 1 || columnIndex > table.Columns.Count)
                {
                    MessageBox.Show("Неправильный индекс колонки!");
                }
                else if(rowIndex <= table.Rows.Count)
                {
                    table.Cell(rowIndex, columnIndex).Range.Text = data;
                    rowIndex++;
                }
                else
                {
                    table.Rows.Add();
                    table.Cell(rowIndex, columnIndex).Range.Text = data;
                    rowIndex++;
                }
                document.Save();
                document.Close();

                MessageBox.Show("Результаты теста успешно записаны!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                application.Quit();
            }
        }
    }
}
