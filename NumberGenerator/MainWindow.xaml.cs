using NumberGenerator.Models;
using System.Windows;
using System.Windows.Documents;

namespace NumberGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Paragraph paragraph;

        public MainWindow()
        {
            InitializeComponent();
            paragraph = new Paragraph();
            RitchBox.Document = new FlowDocument(paragraph);
            DataContext = new WindowModel(paragraph);

            //paragraph.Inlines.Add(new Bold(new Run($"{i}"))
            //{
            //    Foreground = Brushes.Green
            //});

            //paragraph.Inlines.Add(new Bold(new Run($" , ")));

            //paragraph.Inlines.Add(new Bold(new Run($"{b}"))
            //{
            //    Foreground = Brushes.Red
            //});
        }
    }    
}
