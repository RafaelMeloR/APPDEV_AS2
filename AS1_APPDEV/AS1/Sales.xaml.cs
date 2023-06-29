using AS1.Models;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Net.Http.Json;

namespace AS1
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sale : Window
    {
        double total = 0;
        static HttpClient httpClient;
        public Sale()
        {
            InitializeComponent();
            _ = utilities.AS1.showAsync(grid);
            httpClient = utilities.API.conn();
        }
        private void clean()
        {
            producName.Text = string.Empty;
            Amount.Text = string.Empty;
            Price.Text = string.Empty;
            ProductId.Text = string.Empty;
            quantity.Text = string.Empty;  
            
        }
        private void createBtn_Click_1(object sender, RoutedEventArgs e)
        {
           
            if (quantity.Text != "")
            {
                gridBuy.Items.Add(new
                {
                    ProductId = ProductId.Text,
                    producName = producName.Text,
                    Amount = Amount.Text,
                    Price = Price.Text,
                    quantity = quantity.Text

                });
                total=total+double.Parse(quantity.Text)*double.Parse(Price.Text);
                totalT.Text =Convert.ToString( total);
                clean();
            }
            else
            {
                MessageBox.Show("You must type the desired quantity ");
            }
        }
        private void CalendarButton_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void CalendarButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Products selected_row = grid.SelectedItem as Products;
            if (selected_row != null)
            {
                utilities.AS1.copyToTextBox(this, selected_row);  
            }
        }

        private void quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            utilities.tools.numberValidation(e);
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            dynamic selected = gridBuy.SelectedItem;
            double itemPrice = double.Parse(selected.Price);
            double itemQuantity = double.Parse(selected.quantity);
            total = total - (itemPrice * itemQuantity);
            totalT.Text=Convert.ToString(total);

            Button deleteButton = (Button)sender;
                dynamic selectedItem = deleteButton.Tag;

                if (selectedItem != null)
                {
                    gridBuy.Items.Remove(selectedItem);
                }
                
            
        }

        private async void buyBtn_Click(object sender, RoutedEventArgs e)
        {
                double total = 0; 
                foreach (var item in gridBuy.Items)
                { 
                    Models.Sales sale = new Models.Sales();
                      sale.Id=int.Parse(((dynamic)item).ProductId);
                      sale.Date= DateTime.Now;
                      sale.Product = ((dynamic)item).producName;
                      sale.Quantity = double.Parse(((dynamic)item).quantity);
                      sale.Price = double.Parse(((dynamic)item).Price);
                      sale.Total=sale.Quantity*sale.Price;
                      var response = await httpClient.PutAsJsonAsync<Sales>("UpdateSales", sale);
                      //MessageBox.Show(response.ToString());
                       _ = utilities.AS1.showAsync(grid);
                   

                    response = await httpClient.PostAsJsonAsync<Sales>("InsertSales", sale);
                    //MessageBox.Show(response.ToString());
                    _ = utilities.AS1.showAsync(grid);
                    total += double.Parse(((dynamic)item).Price) * double.Parse(((dynamic)item).quantity);
                }
            clean();
            totalT.Text = string.Empty;
            gridBuy.Items.Clear();
            MessageBox.Show(Convert.ToString("Total amount to pay is:" + total)); 
            _ = utilities.AS1.showAsync(grid);
        }
    }
}
