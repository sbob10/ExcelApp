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
    /// Interaktionslogik für AddFirmaWindow.xaml
    /// </summary>
    public partial class AddFirmaWindow : Window
    {
        FirmaWindowsViewModel vm;
        Action<Firma> callbackFromParentWindow;

        public AddFirmaWindow(Action<Firma> addingAction)
        {
            InitializeComponent();
            vm = (FirmaWindowsViewModel)this.DataContext;
            vm.CloseDialogAddFirmaFunc = new Func<int, string>(ShowAddFirmaDialogFunc);

            callbackFromParentWindow = addingAction;
        }

        private void PassFirmaToParentWindowCallback(object sender, EventArgs e)
        {
            callbackFromParentWindow(vm.AddFirmaValue);
        }

        private String ShowAddFirmaDialogFunc(int statusCode)
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
