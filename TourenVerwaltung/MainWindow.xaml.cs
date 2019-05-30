using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TourenVerwaltung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        MainViewModel vm;

        public MainWindow()
        {
            InitializeComponent();

            Application.Current.MainWindow.WindowState = WindowState.Normal;

            vm = (MainViewModel)this.DataContext;

            vm.ShowAddFahrerDialogFunc = new Func<int, int, string>(ShowAddFahrerDialogFunc);
            vm.ShowAddFirmaDialogFunc = new Func<int, int, string>(ShowAddFirmaDialogFunc);

            vm.ShowEditFahrerDialogFunc = new Func<Fahrer, string>(ShowEditFahrerDialogFunc);
            vm.ShowEditFirmaDialogFunc = new Func<Firma, string>(ShowEditFirmaDialogFunc);

            vm.SyncFahrerStundenGridColumns = new Func<List<Fahrer>, string>(SyncFahrerStundenGridColumns);
            vm.SyncFahrerStundenGridEntries = new Func<string, double, string>(SyncFahrerStundenGridEntries);

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var result = MessageBox.Show("Wollen Sie das Programm verlassen? Alle ungespeicherten Daten gehen verloren!", "Schließen", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                base.OnClosing(e);
                return;
            }
            e.Cancel = true;
        }


        // Fahrer Dialogs
        private string ShowAddFahrerDialogFunc(int pseudoValue1, int pseudoValue2)
        {
            var dialog = new AddFahrerWindow(AddFahrerFromDialog);
            dialog.ShowDialog();

            return ""; //just pseudo
        }

        private string ShowEditFahrerDialogFunc(Fahrer entryToEdit)
        {
            var dialog = new EditFahrerWindow(EditFahrerFromDialog, entryToEdit);
            dialog.ShowDialog();

            return ""; //just pseudo
        }

        private void AddFahrerFromDialog(Fahrer entry)
        {
            vm.AddFahrerFromCodeBehind(entry);
        }

        private void EditFahrerFromDialog(Fahrer entry)
        {
            vm.EditFahrerFromCodeBehind(entry);
        }

        // Firma Dialogs
        private string ShowAddFirmaDialogFunc(int pseudoValue1, int pseudoValue2)
        {
            var dialog = new AddFirmaWindow(AddFirmaFromDialog);
            dialog.ShowDialog();

            return ""; //just pseudo
        }

        private string ShowEditFirmaDialogFunc(Firma entryToEdit)
        {
            var dialog = new EditFirmaWindow(EditFirmaFromDialog, entryToEdit);
            dialog.ShowDialog();

            return ""; //just pseudo
        }

        private void AddFirmaFromDialog(Firma entry)
        {
            vm.AddFirmaFromCodeBehind(entry);
        }

        private void EditFirmaFromDialog(Firma entry)
        {
            vm.EditFirmaFromCodeBehind(entry);
        }

        private string SyncFahrerStundenGridColumns(List<Fahrer> fahrer)
        {
            foreach (var column in FahrerStundenGrid.Columns)
            {
                FahrerStundenGrid.Columns.Remove(column);
            }

            foreach (var Fahrer in fahrer)
            {
                

            }


            return "";
        }

        private string SyncFahrerStundenGridEntries(string fahrerName, double stunden)
        {

            return "";
        }

        public void SyncFahrerStundenGesamtGridColumns(List<Fahrer> fahrer)
        {

        }

        public void SyncFahrerStundenGesamtGridEntries()
        {

        }

    }

   
}
