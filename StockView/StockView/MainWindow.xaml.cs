using StockView.Models;
using StockView.Presenters;
using StockView.Services;
using StockView.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;

namespace StockView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly System.Windows.Forms.ContextMenu contextMenu1;
        private readonly System.Windows.Forms.MenuItem menuItem1;
        private readonly IContainer components;
        private readonly System.Windows.Forms.NotifyIcon sysTrayIcon;
        private string searchedText { get; set; }
        private StockPresenter service { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            components = new Container();
            contextMenu1 = new System.Windows.Forms.ContextMenu();
            menuItem1 = new System.Windows.Forms.MenuItem();

            contextMenu1.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { menuItem1 });

            menuItem1.Index = 0;
            menuItem1.Text = "E&xit";
            menuItem1.Click += new EventHandler(menuItem1_Click);
            sysTrayIcon = new System.Windows.Forms.NotifyIcon(components)
            {
                Icon = Properties.Resources.Stock_icon,
                ContextMenu = contextMenu1,
                Text = Properties.Resources.NotifyIconText,
                Visible = true
            };

            // Handle the DoubleClick event to activate the form.
            sysTrayIcon.DoubleClick += new EventHandler(sysTrayIcon_Click);

            service = new StockPresenter(this);
            WatchListButton.Visibility = Visibility.Collapsed;
            Closing += (object sender, CancelEventArgs e) =>
            {
                sysTrayIcon.Visible = false;
                sysTrayIcon.Icon = null;
                sysTrayIcon.Dispose();
            };
        }

        void MyNotifyIcon_MouseDoubleClick(object sender,
   System.Windows.Forms.MouseEventArgs e)
        {
            WindowState = WindowState.Normal;
        }



        private void sysTrayIcon_Click(object Sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                WindowState = WindowState.Normal;

            // Activate the form.
            Activate();
        }

        private void menuItem1_Click(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            Close();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            service.DisableTimer();
            LoadingLabel.Visibility = Visibility.Visible;
            WatchListButton.Visibility = Visibility.Collapsed;
            searchedText = searchbox.Text;
            await Task.Delay(1000 * 5);
            var stocks = await service.SearchBySymbol(searchedText);
            await UpdateStockDataIntoSearchGrid(stocks);
            LoadingLabel.Visibility = Visibility.Collapsed;
            WatchListButton.Visibility = Visibility.Visible;
        }

        public void UpdateStockDataIntoGrid(List<StockItems> stocks, Boolean isWatchList)
        {
            if ((stocks == null) || (stocks.Count == 0))
            {
                MessageBox.Show("No data found!!");
            }
            else
            {
                var rowCount = 0;
                var columns = Constants.NumberOfWaitListItemColumns;

                // cleanup of previous data
                if (ResultGrid.ColumnDefinitions.Count > 0)
                {
                    ResultGrid.ColumnDefinitions.RemoveRange(0, ResultGrid.ColumnDefinitions.Count);
                }
                if (ResultGrid.RowDefinitions.Count > 0)
                {
                    ResultGrid.RowDefinitions.RemoveRange(0, ResultGrid.RowDefinitions.Count);
                }
                if (ResultGrid.Children.Count > 0)
                {
                    ResultGrid.Children.RemoveRange(0, ResultGrid.Children.Count);
                }

                // creation of columns and rows of table for new data
                for (int i = 0; i < columns; i++)
                {
                    if (i == 0)
                    {
                        ResultGrid.ColumnDefinitions.Add(new ColumnDefinition()
                        {
                            Width = new GridLength(225) // for first column which is name can be greater in size
                        });
                    }
                    else
                    {
                        ResultGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    }
                }

                for (int i = 0; i <= stocks.Count; i++)
                {
                    var newRow = new RowDefinition()
                    {
                        MaxHeight = 40.0,
                        MinHeight = 40.0,
                    };
                    ResultGrid.RowDefinitions.Add(newRow);
                }

                // setting column name
                setLabel("Name", rowCount, 0);
                setLabel("Symbol", rowCount, 1);
                setLabel("Exchange", rowCount, 2);
                setLabel("52-W High", rowCount, 3);
                setLabel("52-W Low", rowCount, 4);
                setLabel("Price", rowCount, 5);
                setLabel("% Change", rowCount, 6);

                rowCount++;

                foreach (var stock in stocks)
                {
                    Brush labelBackground = null;
                    if ((stock.changesPercentage ?? 0) > 0)
                    {
                        labelBackground = new SolidColorBrush(Colors.LimeGreen);
                    }
                    else if ((stock.changesPercentage ?? 0) < 0)
                    {
                        labelBackground = new SolidColorBrush(Colors.Red);
                    }
                    setLabel(stock.name, rowCount, 0);
                    setLabel(stock.symbol, rowCount, 1);
                    setLabel(stock.exchange, rowCount, 2);
                    setLabel(stock.yearLow.ToString(), rowCount, 3);
                    setLabel(stock.yearHigh.ToString(), rowCount, 4);
                    setLabel(stock.price.ToString(), rowCount, 5);
                    setLabel(stock.changesPercentage.ToString(), rowCount, 6, labelBackground);
                    setButton(rowCount, 7, isWatchList == true ? "Delete" : "Add");
                    rowCount++;
                }
            }
            LoadingLabel.Visibility = Visibility.Collapsed;
            WatchListButton.Visibility = isWatchList? Visibility.Collapsed: Visibility.Visible;
        }

        public async Task UpdateStockDataIntoSearchGrid(List<SearchResponse> stocks)
        {
            if ((stocks == null) || (stocks.Count == 0))
            {
                MessageBox.Show("No data found!!");
            }
            else
            {
                List<string> stockSymbolList = new List<string>();
                foreach (var stock in stocks)
                {
                    stockSymbolList.Add(stock.symbol);
                }
                await service.SwitchToSearchView(stockSymbolList);
            }
        }

        private void setLabel(string content, int row, int column, Brush background = null)
        {
            var labelControl = new Label
            {
                Content = content,
                Margin = new Thickness(0, 0, 0, 4)
            };
            if (background != null)
            {
                labelControl.Background = background;
            }
            if (row == 0)
            {
                labelControl.FontFamily = new FontFamily("Times New Roman Bold");
                labelControl.FontSize = 15.0;
            };
            Grid.SetRow(labelControl, row);
            Grid.SetColumn(labelControl, column);
            ResultGrid.Children.Add(labelControl);
        }

        private void setButton(int row, int column, string content = "Delete")
        {
            var buttonControl = new Button
            {
                Content = content,
                Background = new SolidColorBrush(Colors.CadetBlue),
                Width = 60.0,
                BorderBrush = new SolidColorBrush(Colors.Gray),
                Margin = new Thickness(0, 8, 0, 12),

            };
            if (content.Equals("Add"))
            {
                buttonControl.Click += AddButton_Click;
            }
            else
            {
                buttonControl.Click += DeleteButton_Click;
            }
            Grid.SetRow(buttonControl, row);
            Grid.SetColumn(buttonControl, column);
            ResultGrid.Children.Add(buttonControl);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.Content = "Deleting";
                int rowIndex = Grid.GetRow(button);
                var label = ResultGrid.Children.Cast<UIElement>()
                    .First(element => Grid.GetRow(element) == rowIndex && Grid.GetColumn(element) == 1) as Label;
                var symbol = label?.Content as string;
                service.DeleteFromConfiguredStocks(symbol);
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.Content = "Adding";
                int rowIndex = Grid.GetRow(button);
                var label = ResultGrid.Children.Cast<UIElement>()
                    .First(element => Grid.GetRow(element) == rowIndex && Grid.GetColumn(element) == 1) as Label;
                var symbol = label?.Content as string;
                await service.AddToConfiguredStocks(symbol);
                await service.SwitchToDashBoard();
                searchbox.Text = string.Empty;
            }
        }

        private async void WatchListButton_Click(object sender, RoutedEventArgs e)
        {
            await service.SwitchToDashBoard();
            searchbox.Text = string.Empty;
            WatchListButton.Visibility = Visibility.Collapsed;
            LoadingLabel.Visibility = Visibility.Visible;
        }
    }
}
