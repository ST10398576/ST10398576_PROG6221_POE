﻿using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;

namespace ST10398576_PROG6221_POE
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes = new List<Recipe>();
        private Dictionary<string, double> originalQuantities = new Dictionary<string, double>();

        public MainWindow()
        {
            InitializeComponent();
            RecipeListBox.ItemsSource = recipes;
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow(recipes, originalQuantities);
            addRecipeWindow.ShowDialog(); //Shows the addRecipeWin window
            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = recipes; //The recipe list box will get the names from recipes
        }

        private void ScaleRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            ScaleRecipeWindow scaleRecipeWindow = new ScaleRecipeWindow(recipes, originalQuantities);
            scaleRecipeWindow.ShowDialog();
            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = recipes;
        }

        private void ResetRecipeButton_Click(object sender, RoutedEventArgs e) //Removes all recipes from the list
        {
            foreach (var recipe in recipes)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    ingredient.Amount = originalQuantities[ingredient.Name];
                }
                recipe.TotalCalories = recipe.Ingredients.Sum(ingredient => ingredient.Calories * ingredient.Amount); //Again ensures then the ingredient calories scales with the ingreident
            }
            MessageBox.Show("Recipe quantities have been reset to original values");
            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = recipes;
        }

        private void ClearRecipes_Click(object sender, RoutedEventArgs e)
        {
            recipes.Clear(); //Clears all recipes
            originalQuantities.Clear(); //Clears all quantities
            RecipeListBox.ItemsSource = null; //Clears the recipe list box
            MessageBox.Show("All Recipe data has been removed");
        }

        private void RecipeListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RecipeListBox.SelectedItem is Recipe selectedRecipe)
            {
                DisplayRecipeDetails(selectedRecipe);
            }
        }

        private void DisplayRecipeDetails(Recipe recipe)  //Displays the recipe details when clicked on
        {
            string details = $"Recipe: {recipe.Name}\n\nIngredients:\n";

            foreach (var ingredient in recipe.Ingredients)
            {
                details += $"{ingredient.Amount} {ingredient.Measurement} of {ingredient.Name} - Calories: {ingredient.Calories}, Food Group: {ingredient.FoodGroup}\n";
            }

            details += $"\nTotal Calories: {recipe.TotalCalories}\n\nSteps:\n";

            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                details += $"{i + 1}. {recipe.Steps[i]}\n";
            }

            RecipeDetailsTextBlock.Text = details;
        }

        private void FilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            var filteredRecipes = recipes.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(IngredientFilterTextBox.Text))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.Name.Equals(IngredientFilterTextBox.Text, StringComparison.OrdinalIgnoreCase))); //Filters recipes if the recipe name matches
            }

            if (!string.IsNullOrWhiteSpace(FoodGroupFilterTextBox.Text))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.FoodGroup.Equals(FoodGroupFilterTextBox.Text, StringComparison.OrdinalIgnoreCase)));  //Filters recipes if the ingreident name matches
            }

            if (!string.IsNullOrWhiteSpace(CaloriesFilterTextBox.Text) && double.TryParse(CaloriesFilterTextBox.Text, out double maxCalories))
            {
                filteredRecipes = filteredRecipes.Where(r => r.TotalCalories <= maxCalories); //Filters recipes if the calorie is BELOW the amount the user wants to filter by
            }

            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = filteredRecipes.ToList();
        }

        private void ClearFilters_Click(object sender, RoutedEventArgs e) //A button to clear all filters
        {
            IngredientFilterTextBox.Clear();
            FoodGroupFilterTextBox.Clear();
            CaloriesFilterTextBox.Clear();

            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = recipes;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}