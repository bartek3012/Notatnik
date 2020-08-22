using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;
using System.Drawing;
//using System.Drawing;

namespace JacekMatulewski.WpfUtils
{
    public class Printing
    {
        private static FlowDocument createFlowDocument(string []lines, double pageWidth)
        {
            FlowDocument fd = new FlowDocument();

            //fd.Background = System.Windows.Media.Brushes.White;
            //fd.Foreground = System.Windows.Media.Brushes.Black;

            //fd.ColumnGap = 0;
            fd.ColumnWidth = pageWidth;

            foreach(string line in lines)
            {
                Paragraph paragraph = new Paragraph(new Run(line));
                fd.Blocks.Add(paragraph);
            }
            return fd;
        }

        public static void PrintText(string[] line)
        {
            PrintDialog printDialog = new PrintDialog();
            if(printDialog.ShowDialog() == true)
            {
                FlowDocument fd = createFlowDocument(line, printDialog.PrintableAreaWidth);
                printDialog.PrintDocument((fd as IDocumentPaginatorSource).DocumentPaginator, fd.Name);
            }
        }

        public static void PrintText(string text)
        {
            
            string[] lines = text.Split('\n');
            for(int i =0; i<lines.Length; i++)
            {
                lines[i] = lines[i].TrimEnd('\r', ' ');
            }
            PrintText(lines);
        }
    }
}
