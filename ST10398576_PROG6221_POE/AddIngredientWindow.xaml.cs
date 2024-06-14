
using System.Windows;

namespace ST10398576_PROG6221_POE
{
    public partial class AddIngredientWindow : Window
    {
        public Ingredient Ingredient { get; set; }

        public AddIngredientWindow()
        {
            InitializeComponent();
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            Ingredient = new Ingredient
            {
                Name = NameTextBox.Text,
                Amount = double.Parse(AmountTextBox.Text),
                Measurement = MeasurementTextBox.Text,
                Calories = double.Parse(CaloriesTextBox.Text),
                FoodGroup = FoodGroupTextBox.Text 
            };

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