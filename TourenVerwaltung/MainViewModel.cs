using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourenVerwaltung
{
    public class MainViewModel : ViewModelBase
    {

        #region Properties

        private ObservableCollection<LUEntry> _LUCollection;

        public ObservableCollection<LUEntry> LUCollection
        {
            get { return _LUCollection; }
            set { SetProperty(ref _LUCollection, value, () => LUCollection); }
        }


        #endregion Properties

        #region Data


        #endregion Data

        #region Commands&Services


        #endregion Commands&Services

        #region Initialisations

        public MainViewModel()
        {
            InitCommandsAndServices();
            InitValuesAndCollections();
        }

        private void InitCommandsAndServices()
        {
           
        }

        private void InitValuesAndCollections()
        {
            // IMPOTANT: The order of these is essential as the last attributes are calling command on set()
            LUCollection = new ObservableCollection<LUEntry>(new List<LUEntry>());
        }

        #endregion Initialisations

        #region Public Methods


        #endregion Public Methods

        #region Private Methods    

        // Load-And-Store-Methods


        #endregion Private Methods

        #region Command Methods

        // CRUD-Commands

      
        // Export-Commands

  
        #endregion Command Methods

    }
}
