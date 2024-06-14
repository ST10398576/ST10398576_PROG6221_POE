using System.Windows;

namespace ST10398576_PROG6221_POE
{
    public partial class AddStepWindow : Window
    {
        public string Step { get; set; }

        public AddStepWindow()
        {
            InitializeComponent();
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            Step = StepTextBox.Text;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}