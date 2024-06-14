using System.Windows;

namespace ST10398576_PROG6221_POE
{
    public partial class AddRecipeWindow : Window
    {
        private List<Recipe> recipes;
        private Dictionary<string, double> originalQuantities;
        private Recipe newRecipe;

        public AddRecipeWindow(List<Recipe> recipes, Dictionary<string, double> originalQuantities)
        {
            InitializeComponent();
            this.recipes = recipes; //Refers to current instance of recipe
            this.originalQuantities = originalQuantities;
            newRecipe = new Recipe(); //Calls the new recipe moethod from App.xaml.cs
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            AddIngredientWindow addIngredientWindow = new AddIngredientWindow();
            addIngredientWindow.ShowDialog();

            if (addIngredientWindow.DialogResult == true)
            {
                Ingredient ingredient = addIngredientWindow.Ingredient;
                // Add the ingredient to the IngredientsListBox
                IngredientsListBox.Items.Add(ingredient);
            }
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            AddStepWindow addStepWindow = new AddStepWindow();
            addStepWindow.ShowDialog();

            if (addStepWindow.DialogResult == true)
            {
                string step = addStepWindow.Step;
                StepsListBox.Items.Add(step);
            }
        }

        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            newRecipe.Name = RecipeNameTextBox.Text;

            newRecipe.TotalCalories = newRecipe.Ingredients.Sum(i => i.Calories * i.Amount); //Scales the ingredient calorie with the ingredient

            if (newRecipe.TotalCalories > 300)
            {
                MessageBox.Show($"Warning: Total calories for '{newRecipe.Name}' exceeds 300", "Calorie Warning", MessageBoxButton.OK, MessageBoxImage.Warning); //Warning for recipe going over 300 calories
            }

            recipes.Add(newRecipe);

            MessageBox.Show("Recipe added successfully");

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}