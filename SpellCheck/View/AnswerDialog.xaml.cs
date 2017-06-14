using SpellCheck.ViewModel;
using System.Windows;

namespace SpellCheck.View
{
    /// <summary>
    /// Interaction logic for AnswerDialog.xaml
    /// </summary>
    public partial class AnswerDialog : Window
    {
        public AnswerDialog()
        {
            InitializeComponent();
        }

        public AnswerDialog(AnswerDialogViewModel advm)
        {
            this.DataContext = advm;
            InitializeComponent(); 
        }


    }
}
