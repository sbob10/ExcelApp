using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace TourenVerwaltung
{
    /// <summary>
    /// Interaktionslogik für EditFahrerWindow.xaml
    /// </summary>
    public partial class EditFahrerWindow : Window
    {
        FahrerWindowsViewModel vm;
        Action<Fahrer> callbackFromParentWindow;

        public EditFahrerWindow(Action<Fahrer> editingAction, Fahrer entryToEdit)
        {
            InitializeComponent();

            vm = (FahrerWindowsViewModel)this.DataContext;
            vm.EditFahrerValue = entryToEdit;
            vm.CloseDialogEditFahrerFunc = new Func<int, string>(ShowEditFahrerDialogFunc);

            callbackFromParentWindow = editingAction;
        }

        private void PassFahrerToParentWindowCallback(object sender, EventArgs e)
        {
            callbackFromParentWindow(vm.EditFahrerValue);
        }

        private String ShowEditFahrerDialogFunc(int statusCode)
        {
            if (statusCode == 1)
            {
                Closed += PassFahrerToParentWindowCallback;
            }
            this.Close();

            return ""; //just pseudo
        }
    }


}
