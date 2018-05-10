using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Models;

namespace WPFWebCrawler.Popup
{
    /// <summary>
    /// Interaction logic for addNewManga.xaml
    /// </summary>
    public partial class addNewManga : Window
    {
        MainWindow parentWindow; // this is pretty stupid
        public addNewManga(MainWindow Parent)
        {
            parentWindow = Parent;
            InitializeComponent();
        }

        private void tbLink_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblMangaKey.Content = MangaPark.StripForKey(tbLink.Text);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string newName = lblMangaKey.Content.ToString().Trim();
            if (lblMangaKey.Content.ToString() == string.Empty)
            {
                MessageBox.Show("Your link is too weird !!!");
            }
            else 
            {
                Boolean isDup = UserData.UserDataProvider.USER_DATA.Where(manga => manga.Key == newName).Count() > 0;
                if (isDup)
                {
                    MessageBox.Show("Your link is already in the list LOL !!!");
                }
                else
                {
                    UserData.UserDataProvider.USER_DATA.Add(new MangaPark(tbLink.Text));
                    parentWindow.RefreshGrid();
                }
            }
        }
    }
}
