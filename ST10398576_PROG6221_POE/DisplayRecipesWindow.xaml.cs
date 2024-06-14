using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ST10398576_PROG6221_POE
{
    public partial class DisplayRecipesWindow : Window
    {
        public DisplayRecipesWindow(List<Recipe> recipes)
        {
            InitializeComponent();

            // Sort the recipes alphabetically by name
            recipes.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase));

            // Populate the listbox with the recipes
            RecipesListBox.ItemsSource = recipes;
        }

        // Event handler for when a recipe is selected in the listbox
        private void RecipesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected recipe
            Recipe selectedRecipe = (Recipe)RecipesListBox.SelectedItem;

            // Check if a recipe is selected
            if (selectedRecipe != null)
            {
                // Create a new RecipeWindow and pass the selected recipe
                //RecipeWindow recipeWindow = new RecipeWindow(selectedRecipe);
                //recipeWindow.ShowDialog();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}