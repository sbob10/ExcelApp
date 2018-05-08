using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TourenVerwaltung
{
    public class MainViewModel : ViewModelBase
    {

        #region Properties

        private Fahrer _SelectedFahrer;

        public Fahrer SelectedFahrer
        {
            get { return _SelectedFahrer; }
            set { SetProperty(ref _SelectedFahrer, value, () => SelectedFahrer); LoadFahrerLUEntries(); }
        }

        private Firma _SelectedFirma;

        public Firma SelectedFirma
        {
            get { return _SelectedFirma; }
            set { SetProperty(ref _SelectedFirma, value, () => SelectedFirma); LoadFirmaLUEntries(); }
        }

        private Firma _SelectedFirmaFirmenPreis;

        public Firma SelectedFirmeFirmenPreis
        {
            get { return _SelectedFirmaFirmenPreis; }
            set { SetProperty(ref _SelectedFirmaFirmenPreis, value, () => SelectedFirmeFirmenPreis); LoadTourPreisCollection(); }
        }

        private ObservableCollection<LUEntry> _LUCollection;

        public ObservableCollection<LUEntry> LUCollection
        {
            get { return _LUCollection; }
            set { SetProperty(ref _LUCollection, value, () => LUCollection); LoadLUGesamtEntries(); LoadLUGesamtValues(); }
        }

        private ObservableCollection<Fahrer> _FahrerCollection;

        public ObservableCollection<Fahrer> FahrerCollection
        {
            get { return _FahrerCollection; }
            set { SetProperty(ref _FahrerCollection, value, () => FahrerCollection); }
        }

        private ObservableCollection<LUEntry> _FahrerTabLUEntries;

        public ObservableCollection<LUEntry> FahrerTabLUEntries
        {
            get { return _FahrerTabLUEntries; }
            set { SetProperty(ref _FahrerTabLUEntries, value, () => FahrerTabLUEntries); }
        }

        private ObservableCollection<Firma> _FirmenCollection;

        public ObservableCollection<Firma> FirmenCollection
        {
            get { return _FirmenCollection; }
            set { SetProperty(ref _FirmenCollection, value, () => FirmenCollection); }
        }

        private ObservableCollection<LUEntry> _FirmenTabLUEntries;

        public ObservableCollection<LUEntry> FirmenTabLUEntries
        {
            get { return _FirmenTabLUEntries; }
            set { SetProperty(ref _FirmenTabLUEntries, value, () => FirmenTabLUEntries); }
        }

        private ObservableCollection<Double> _LUGesamtEntryCollection;

        public ObservableCollection<Double> LUGesamtEntryCollection
        {
            get { return _LUGesamtEntryCollection; }
            set { SetProperty(ref _LUGesamtEntryCollection, value, () => LUGesamtEntryCollection); }
        }

        private ObservableCollection<TourPreis> _CurrentTourPreisCollection;

        public ObservableCollection<TourPreis> CurrentTourPreisCollection
        {
            get { return _CurrentTourPreisCollection; }
            set { SetProperty(ref _CurrentTourPreisCollection, value, () => CurrentTourPreisCollection); }
        }

        private Double _GesamtNettoValue;

        public Double GesamtNettoValue
        {
            get { return _GesamtNettoValue; }
            set { SetProperty(ref _GesamtNettoValue, value, () => GesamtNettoValue); }
        }

        private Double _GesamtBruttoValue;

        public Double GesamtBruttoValue
        {
            get { return _GesamtBruttoValue; }
            set { SetProperty(ref _GesamtBruttoValue, value, () => GesamtBruttoValue); }
        }

        private String _SelectedMonth;

        public String SelectedMonth
        {
            get { return _SelectedMonth; }
            set { StoreAllCollections(); SetProperty(ref _SelectedMonth, value, () => SelectedMonth); LoadAllCollections(); }
        }


        private ObservableCollection<String> _MonthComboSource;

        public ObservableCollection<String> MonthComboSource
        {
            get { return _MonthComboSource; }
            set { SetProperty(ref _MonthComboSource, value, () => MonthComboSource); }
        }

        #endregion Properties

        #region Data

        private ExcelManager _manager;

        private Dictionary<String, List<TourPreis>> _FirmenPreiseHashMap;

        #endregion Data

        #region Commands&Services

        public Func<int, int, string> ShowAddFahrerDialogFunc { get; set; }
        public Func<Fahrer, string> ShowEditFahrerDialogFunc { get; set; }

        public Func<int, int, string> ShowAddFirmaDialogFunc { get; set; }
        public Func<Firma, string> ShowEditFirmaDialogFunc { get; set; }

        public ICommand SaveCollectionsCommand { get; set; }

        public ICommand ExportLUExcelCommand { get; set; }
        public ICommand OnGridEditedCommand { get; set; }
        public ICommand AddEmptyLUValueCommand {get; set;}
        public ICommand SyncFirmenPreiseIntoLUEntriesCommand { get; set; }

        public ICommand AddFahrerCommand { get; set; }
        public ICommand EditFahrerCommand { get; set; }
        public ICommand DeleteFahrerCommand { get; set; }
        public ICommand ExportFahrerExcelCommand { get; set; }

        public ICommand AddFirmaCommand { get; set; }
        public ICommand EditFirmaCommand { get; set; }
        public ICommand DeleteFirmaCommand { get; set; }
        public ICommand ExportFirmaExcelCommand { get; set; }

        public ICommand ExportFirmenPreiseExcelCommand { get; set; }
        public ICommand SyncLUEntriesIntoFirmenPreiseCommand { get; set; }

        public IMessageBoxService MessageBoxService { get { return ServiceContainer.GetService<IMessageBoxService>(); } set { } }
        public IServiceContainer IServiceContainer { get; set; }

        #endregion Commands&Services

        #region Initialisations

        public MainViewModel()
        {
            _manager = new ExcelManager();
            _FirmenPreiseHashMap = new Dictionary<string, List<TourPreis>>();

            InitCommandsAndServices();
            InitValuesAndCollections();
        }

        private void InitCommandsAndServices()
        {
            SaveCollectionsCommand = new DelegateCommand(SaveCollections);

            OnGridEditedCommand = new DelegateCommand(OnGridLUEditedForCommand);
            ExportLUExcelCommand = new DelegateCommand(ExportLUExcel);
            AddEmptyLUValueCommand = new DelegateCommand(AddEmptyLUValue);
            SyncFirmenPreiseIntoLUEntriesCommand = new DelegateCommand(LoadLUEntriesForAllFirmenPreise);

            AddFahrerCommand = new DelegateCommand(AddFahrer);
            EditFahrerCommand = new DelegateCommand(EditFahrer);
            DeleteFahrerCommand = new DelegateCommand(DeleteFahrer);
            ExportFahrerExcelCommand = new DelegateCommand(ExportFahrerExcel);

            AddFirmaCommand = new DelegateCommand(AddFirma);
            EditFirmaCommand = new DelegateCommand(EditFirma);
            DeleteFirmaCommand = new DelegateCommand(DeleteFirma);
            ExportFirmaExcelCommand = new DelegateCommand(ExportFirmaExcel);

            ExportFirmenPreiseExcelCommand = new DelegateCommand(ExportFirmenPreiseExcel);
            SyncLUEntriesIntoFirmenPreiseCommand = new DelegateCommand(LoadFirmenPreisForAllLUEntries);
        }

        private void InitValuesAndCollections()
        {
            // IMPOTANT: The order of these is essential as the last attributes are calling command on set()

            MonthComboSource = new ObservableCollection<String>(Constants.Months);
            SelectedMonth = Constants.getCurrentDateMonth();

            LUCollection = new ObservableCollection<LUEntry>(new List<LUEntry>());
            LUCollection.CollectionChanged += OnGridLUEdited;
            FirmenCollection = new ObservableCollection<Firma>(new List<Firma>());
            FahrerCollection = new ObservableCollection<Fahrer>(new List<Fahrer>());

            CurrentTourPreisCollection = new ObservableCollection<TourPreis>(new List<TourPreis>());

            LoadAllCollections();
        }

        #endregion Initialisations

        #region Public Methods

        public void LoadAllCollections()
        {
            if(_manager != null)
            {
                try
                {
                    LUCollection = new ObservableCollection<LUEntry>(_manager.LoadCollectionLUEntry(SelectedMonth));
                    LUCollection.CollectionChanged += OnGridLUEdited;
                    FirmenCollection = new ObservableCollection<Firma>(_manager.LoadCollectionFirma());
                    _FirmenPreiseHashMap = _manager.LoadHashMapFirmenPreise();                   
                    FahrerCollection = new ObservableCollection<Fahrer>(_manager.LoadCollectionFahrer());

                    SyncHashMapWithFirmenCollection();
                }
                catch(Exception e)
                {
                    var x = MessageBoxService.ShowMessage("Schließen Sie alle geöffneten Excel-Dokumente und starten Sie das Programm neu!", "Fehler", MessageButton.OK, MessageIcon.Information);
                    if (x.Equals(MessageResult.OK))
                        Environment.Exit(0);
                    return;
                }
            }
        }

        public void StoreAllCollections()
        {
            if(_manager != null && LUCollection != null && FahrerCollection != null && FirmenCollection != null)
            {
                _manager.StoreCollectionLU(LUCollection.ToList(), SelectedMonth);
                _manager.StoreCollectionFahrer(FahrerCollection.ToList());
                _manager.StoreCollectionFirma(FirmenCollection.ToList());
                _manager.StoreHashMapFirmenPreise(_FirmenPreiseHashMap);
            }
        }

        public void AddFahrerFromCodeBehind(Fahrer fahrer)
        {
            FahrerCollection.Add(fahrer);
        }

        public void EditFahrerFromCodeBehind(Fahrer fahrer)
        {
            if (fahrer != null)
            {
                List<Fahrer> tempList = FahrerCollection.ToList();
                foreach (Fahrer item in FahrerCollection)
                {
                    if (fahrer.NameVorname.Equals(item.NameVorname))
                    {
                        item.DatumBis = fahrer.DatumBis;
                        item.DatumSeit = fahrer.DatumSeit;
                        item.GebDatum = fahrer.GebDatum;
                        item.StundenAbgerechnet = fahrer.StundenAbgerechnet;
                        item.StundenGesamt = fahrer.StundenGesamt;
                        item.ÜberstundenDifferenz = fahrer.ÜberstundenDifferenz;
                        item.ÜberstundenVormonate = fahrer.ÜberstundenVormonate;
                        FahrerCollection = new ObservableCollection<Fahrer>(tempList);
                        break;
                    }
                }
            }
        }

        public void AddFirmaFromCodeBehind(Firma firma)
        {
            FirmenCollection.Add(firma);
            SyncHashMapWithFirmenCollection();
        }

        public void EditFirmaFromCodeBehind(Firma firma)
        {
            if (firma != null)
            {
                List<Firma> tempList = FirmenCollection.ToList();
                foreach (Firma item in FirmenCollection)
                {
                    if (firma.Name.Equals(item.Name))
                    {
                        item.Ansprechpartner = firma.Ansprechpartner;
                        item.Land = firma.Land;
                        item.PLZ = firma.PLZ;
                        item.Ort = firma.Ort;
                        item.StraßeUndNr = firma.StraßeUndNr;
                        FirmenCollection = new ObservableCollection<Firma>(tempList);
                        break;
                    }
                }
            }
            SyncHashMapWithFirmenCollection();
        }

        /*public LUEntry ConfigureLUEntryNewItem()
        {
            LUEntry returnValue = new LUEntry();

            returnValue.OnAuftragsgeberChanged = new Func<string, LUEntry, string>(LoadRechnungsnummerForLUEntry);
            returnValue.OnLoadValueIntoFirmenPreise = new Func<LUEntry, string>(LoadFirmenPreisForLUEntry);

            return returnValue;
        }*/

        #endregion Public Methods

        #region Private Methods    

        // Load-And-Store-Methods

        private void LoadLUGesamtEntries()
        {
            List<Double> tempList = new List<Double>();
            foreach(LUEntry item in _LUCollection)
            {
                Double tempValue = item.Preis_Netto + item.Maut + item.WarteZeit;
                item.GesamtNetto = tempValue;
                tempList.Add(tempValue);
            }
            LUGesamtEntryCollection = new ObservableCollection<double>(tempList);
        }

        private void LoadLUGesamtValues()
        {
            double tempValue = 0;
            foreach(double item in LUGesamtEntryCollection.ToList())
            {
                tempValue += item;
            }
            GesamtNettoValue = tempValue;
            GesamtBruttoValue = tempValue * 1.19;
        }

        private void LoadFahrerLUEntries()
        {
            if (SelectedFahrer == null)
                return;
        
            List<LUEntry> tempList = new List<LUEntry>();

            foreach (LUEntry entry in LUCollection.ToList())
            {
                if (entry.Fahrer.Equals(SelectedFahrer.NameVorname))
                    tempList.Add(entry);
            }

            FahrerTabLUEntries = new ObservableCollection<LUEntry>(tempList);

        }

        private void LoadFirmaLUEntries()
        {
            if (SelectedFirma == null)
                return;

            List<LUEntry> tempList = new List<LUEntry>();

            foreach (LUEntry entry in LUCollection.ToList())
            {
                if (entry.Auftragsgeber != null && entry.Auftragsgeber.Equals(SelectedFirma.Name))
                    tempList.Add(entry);
            }

            FirmenTabLUEntries = new ObservableCollection<LUEntry>(tempList);
        }

        private String LoadRechnungsnummerForLUEntry(String firma, LUEntry changedItem)
        {
            foreach (Firma item in FirmenCollection)
            {
                if (item.Name.Equals(firma))
                {
                    Firma holderFirma = item;

                    if (changedItem.RechnNr == 0)
                    {

                        if (holderFirma.CountForCurrentRechnungsNr == 0)
                            holderFirma.CurrentRechnungsNr++;

                        holderFirma.CountForCurrentRechnungsNr++;

                        if(holderFirma.CountForCurrentRechnungsNr > 30)
                        {
                            holderFirma.CountForCurrentRechnungsNr = 0;
                            holderFirma.CurrentRechnungsNr++;
                        }

                        LUEntry holder = changedItem;

                        holder.RechnNr = item.CurrentRechnungsNr;

                        LUCollection.Remove(changedItem);
                        LUCollection.Add(holder);

                        FirmenCollection.Remove(item);
                        FirmenCollection.Add(holderFirma);

                        return ""; //just pseudo
                    }

                }
            }
            return ""; //just pseudo
        }

        private void AddEmptyLUValue()
        {
            LUEntry newValue = new LUEntry();
            newValue.OnAuftragsgeberChanged = new Func<string, LUEntry, string>(LoadRechnungsnummerForLUEntry);
            LUCollection.Add(newValue);
        }

        private void LoadTourPreisCollection()
        {
            if(SelectedFirmeFirmenPreis != null && _FirmenPreiseHashMap.Keys.Contains(SelectedFirmeFirmenPreis.Name))
                CurrentTourPreisCollection = new ObservableCollection<TourPreis>(_FirmenPreiseHashMap[SelectedFirmeFirmenPreis.Name]);
        }

        private void SyncHashMapWithFirmenCollection()
        {
            foreach(Firma item in FirmenCollection.ToList())
            {
                if (!_FirmenPreiseHashMap.Keys.Contains(item.Name))
                    _FirmenPreiseHashMap.Add(item.Name, new List<TourPreis>());
            }
        }

        #endregion Private Methods

        #region Command Methods

        // CRUD-Commands

        private void SaveCollections()
        {
            StoreAllCollections();
        }

        private void AddFahrer()
        {
            ShowAddFahrerDialogFunc.Invoke(0, 0);
        }

        private void EditFahrer()
        {
            if(SelectedFahrer != null && !string.IsNullOrEmpty(SelectedFahrer.NameVorname))
                ShowEditFahrerDialogFunc(SelectedFahrer);
        }

        private void DeleteFahrer()
        {
            if (SelectedFahrer != null && !string.IsNullOrEmpty(SelectedFahrer.NameVorname))
                FahrerCollection.Remove(SelectedFahrer);
        }


        private void AddFirma()
        {
            ShowAddFirmaDialogFunc.Invoke(0, 0);
        }

        private void EditFirma()
        {
            if (SelectedFirma != null && !string.IsNullOrEmpty(SelectedFirma.Name))
                ShowEditFirmaDialogFunc.Invoke(SelectedFirma);
        }

        private void DeleteFirma()
        {
            if (SelectedFirma != null && !string.IsNullOrEmpty(SelectedFirma.Name))
                FirmenCollection.Remove(SelectedFirma);
        }

        // Sync-Commands


        private void LoadFirmenPreisForAllLUEntries()
        {
            if (LUCollection == null)
                return;

            Dictionary<String, List<TourPreis>> tempHashMap = new Dictionary<String, List<TourPreis>>();

            foreach ( LUEntry item in LUCollection)
            {
                String tourname = item.Beladeort + " - " + item.Entladeort;
                bool contains = false;

                foreach (var firma in FirmenCollection.ToList())
                {
                    if (item.Auftragsgeber != null && firma.Name.Equals(item.Auftragsgeber))
                        contains = true;
                }

                if (!contains)
                    continue;

                if (!tempHashMap.Keys.Contains(item.Auftragsgeber))
                    tempHashMap.Add(item.Auftragsgeber, new List<TourPreis>());

                contains = false;
         
                foreach (var item1 in tempHashMap[item.Auftragsgeber])
                {
                    if (item1.Tour.Equals(tourname))
                        contains = true;
                }

                if (!contains)
                    tempHashMap[item.Auftragsgeber].Add(new TourPreis(tourname));
            }

            _FirmenPreiseHashMap = tempHashMap;
            LoadTourPreisCollection();
        }

        private void LoadLUEntriesForAllFirmenPreise()
        {
            List<LUEntry> tempList = new List<LUEntry>();
            foreach (String key in _FirmenPreiseHashMap.Keys)
            {
                foreach (LUEntry entry in LUCollection.ToList())
                {
                    bool added = false;
                    foreach (TourPreis tp in _FirmenPreiseHashMap[key])
                    {
                        if (entry.Auftragsgeber.Equals(key) && tp.Tour.Equals(entry.Beladeort + " - " + entry.Entladeort))
                        {
                            switch (entry.Autotyp)
                            {
                                case "AutoTyp1":
                                    if (tp.Kilometer == 0)
                                        entry.Preis_Netto = tp.AutoTyp1;
                                    else
                                        entry.Preis_Netto = tp.AutoTyp1 * tp.Kilometer;
                                    break;
                                case "AutoTyp2":
                                    if (tp.Kilometer == 0)
                                        entry.Preis_Netto = tp.AutoTyp2;
                                    else
                                        entry.Preis_Netto = tp.AutoTyp2 * tp.Kilometer;
                                    break;
                                case "AutoTyp3":
                                    if (tp.Kilometer == 0)
                                        entry.Preis_Netto = tp.AutoTyp3;
                                    else
                                        entry.Preis_Netto = tp.AutoTyp3 * tp.Kilometer;
                                    break;
                                default:
                                    break;
                            }
                            tempList.Add(entry);
                            added = true;
                            break;
                        }                      
                    }

                    if (!added)
                        tempList.Add(entry);
                }
            }

            LUCollection = new ObservableCollection<LUEntry>(tempList);
            LoadLUGesamtEntries();
        }


        // Export-Commands

        private void ExportLUExcel()
        {
            _manager.StoreCollectionLU(LUCollection.ToList(), SelectedMonth);
            _manager.OpenLeistungsuebersicht(SelectedMonth);
        }

        private void ExportFirmaExcel()
        {
            StoreAllCollections();
            if(SelectedFirma != null)
            {
                _manager.CreateAndOpenRechnungFirma(FirmenTabLUEntries.ToList());
            }
        }

        private void ExportFahrerExcel()
        {
            StoreAllCollections();
            if (SelectedFahrer != null)
            {
                _manager.CreateAndOpenRechnungFahrer(FahrerTabLUEntries.ToList());
            }
        }

       private void ExportFirmenPreiseExcel()
        {
            //TBD!!!!
            StoreAllCollections();
            if ( SelectedFirmeFirmenPreis != null)
            {

            }
        }

        // Events

        public void OnGridLUEdited(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            LoadLUGesamtEntries();
            LoadLUGesamtValues();
            LoadFahrerLUEntries();
            LoadFirmaLUEntries();
        }

        public void OnGridLUEditedForCommand()
        {
            LoadLUGesamtEntries();
            LoadLUGesamtValues();
            LoadFahrerLUEntries();
            LoadFirmaLUEntries();
        }

        #endregion Command Methods

    }
}
