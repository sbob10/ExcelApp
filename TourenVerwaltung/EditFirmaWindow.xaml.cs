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
    /// Interaktionslogik für EditFirmaWindow.xaml
    /// </summary>
    public partial class EditFirmaWindow : Window
    {
        FirmaWindowsViewModel vm;
        Action<Firma> callbackFromParentWindow;

        public EditFirmaWindow(Action<Firma> editingAction, Firma entryToEdit)
        {
            InitializeComponent();

            vm = (FirmaWindowsViewModel)this.DataContext;
            vm.EditFirmaValue = entryToEdit;
            vm.CloseDialogEditFirmaFunc = new Func<int, string>(ShowEditFirmaDialogFunc);

            callbackFromParentWindow = editingAction;
        }

        private void PassFirmaToParentWindowCallback(object sender, EventArgs e)
        {
            callbackFromParentWindow(vm.EditFirmaValue);
        }

        private String ShowEditFirmaDialogFunc(int statusCode)
        {
            if (statusCode == 1)
            {
                Closed += PassFirmaToParentWindowCallback;
            }
            this.Close();

            return ""; //just pseudo
        }
    }
}
