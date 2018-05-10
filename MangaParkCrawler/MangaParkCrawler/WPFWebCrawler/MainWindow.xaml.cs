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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Models;
using System.IO;
using System.Xml.Serialization;
using WPFWebCrawler.Popup;
using System.ComponentModel;
using System.Threading;

namespace WPFWebCrawler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //hardcoded value
        private static string USER_DATA_PATH = System.IO.Directory.GetCurrentDirectory() + "\\" + ("userData.xml");
        public static string WEB_LINK_MANGA_PARK = @"http://mangapark.me/manga/";

        public List<MangaPark> MangaList = new List<MangaPark>();

        public MainWindow()
        {
            InitializeComponent();
            additionalSetup();
        }
        private List<MangaPark> loadMangaData()
        {
            // ... Create a List of objects.
            //mangaList.Add(new MangaPark("http://mangapark.me/manga/shishunki-na-adam", 3));
            //mangaList.Add(new MangaPark("http://mangapark.me/manga/deathtopia-yamada-yoshinobu", 3));

            string text = File.ReadAllText(USER_DATA_PATH, Encoding.UTF8);
            XmlSerializer x = new XmlSerializer(typeof(List<MangaPark>));
            List<MangaPark> answer = (List<MangaPark>)x.Deserialize(new StringReader(text));

            return answer;
        }
        public void RefreshGrid()
        {
            dataGridMangaList.Items.Refresh();
        }

        private void additionalSetup()
        {
            MangaList = UserData.UserDataProvider.USER_DATA;// loadMangaData();

            //this is pretty stupid thing to do but ..meh
            foreach (MangaPark manga in MangaList)
            {
                if (manga.Name != string.Empty)
                    manga.UpdateDisplayName();
            }

            CollectionViewSource itemCollectionViewSource;
            itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
            itemCollectionViewSource.Source = MangaList;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //to get dat from row
            //DataGridRow row = sender as DataGridRow;

            //get the selected row
            MangaPark manga = (MangaPark)dataGridMangaList.SelectedItem;

            
            string selectedColumn = (string)dataGridMangaList.SelectedCells[0].Column.Header;
            if ( "Manga".Equals ( dataGridMangaList.CurrentColumn.Header ) )
            {
                System.Diagnostics.Process.Start( manga.Link );
            }

            //test saving
            //MessageBox.Show( System.IO.Directory.GetCurrentDirectory() );
            //manga.SaveData();

            //test get latest chapter
            //MessageBox.Show( "Latest chapter is " + manga.GetLatestChapter().ToString());
        }


        #region saving and stuffs ... should bu put in a new class 

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            btnSave_Click(null, null);

            base.OnClosing(e);
        }

        //private void 

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(userData );
            string userData = Serialize(MangaList).ToString();
            File.WriteAllText( USER_DATA_PATH , userData);
        }

        public static string Serialize(object o)
        {
            var xs = new XmlSerializer(o.GetType());
            //var xml = new StringWriter();
            //xs.Serialize(xml, o);

            //return xml;
            StringWriter xml;
            using ( xml = new StringWriter() )
            {
                xs.Serialize(xml, o);
            }
            return xml.ToString();
        }
        #endregion saving and stuffs ... should be put in a new class

        #region update latest
        private void btnUpdateLatestCh_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateLatestCh.IsEnabled = false;

            BackgroundWorker updateChpaterWorker = new BackgroundWorker();
            updateChpaterWorker.WorkerReportsProgress = true;
            updateChpaterWorker.DoWork += worker_DoWork;
            updateChpaterWorker.ProgressChanged += worker_ProgressChanged;
            updateChpaterWorker.RunWorkerCompleted += worker_RunWorkerCompleted;

            updateChpaterWorker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    (sender as BackgroundWorker).ReportProgress(i);
            //    Thread.Sleep(100);
            //}
            double allTask = UserData.UserDataProvider.USER_DATA.Count;
            double jobDone = 0;
            foreach( MangaPark manga in UserData.UserDataProvider.USER_DATA )
            {
                manga.GetLatestChapter();
                if (allTask == 0) allTask = 1;

                (sender as BackgroundWorker).ReportProgress((int)( ++jobDone * 100 / allTask), "Getting latest chapter for " + manga.Name );
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblUpdateProgress.Content =  e.UserState.ToString();
            pgbUpdateData.Value = e.ProgressPercentage;
        }

       void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnUpdateLatestCh.IsEnabled = true;

            lblUpdateProgress.Content = "done! LOLOLOL";
            RefreshGrid();
        }

        #endregion update latest
        private void btnAddManga_Click(object sender, RoutedEventArgs e)
        {
            addNewManga ss = new addNewManga( this );
            ss.Topmost = true;
            ss.Activate();
            ss.Show();
        }


    }
}
