using LiteDB;
using System;
using System.Windows;

namespace SampleClient.Writer
{
    public partial class MainWindow : Window
    {
        private const string CONN_STR = @"Filename=..\..\..\Sample.LiteDB3";

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = ConnectToRepo())
            {
                db.Insert(new SampleRecord
                {
                    Text1 = DateTime.Now.ToLongTimeString()
                });
            }
        }

        private LiteRepository ConnectToRepo()
        {
            var mapr = new BsonMapper();

            mapr.RegisterAutoId<ulong>(v => v == 0,
                (db, col) => (ulong)db.Count(col) + 1);

            return new LiteRepository(CONN_STR, mapr);
        }
    }
}
