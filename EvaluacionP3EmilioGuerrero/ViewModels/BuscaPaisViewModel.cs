using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EvaluacionP3EmilioGuerrero.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace EvaluacionP3EmilioGuerrero.ViewModels
{
    internal class BuscaPaisViewModel : ObservableObject, IQueryAttributable
    {
        private ObservableCollection<Modelos.BuscaPais> _peopleList;
        private string _statusMessage;
        private readonly BuscarPaisRepository _buscaPaisRepository;
        public ICommand SaveCommand { get; }
        public ICommand GetAllPeopleCommand { get; }
        public ICommand DeletePersonCommand { get; }


        private Modelos.BuscaPais _buscaPais;

        public Modelos.BuscaPais BuscaPais
        {
            get => _buscaPais;
            set
            {
                if (SetProperty(ref _buscaPais, value))
                {
                    OnPropertyChanged(nameof(Nombre));
                    OnPropertyChanged(nameof(Region));
                }
            }
        }

        public ObservableCollection<Modelos.BuscaPais> PeopleList
        {
            get => _peopleList;
            set => SetProperty(ref _peopleList, value);
        }

        public string Nombre
        {
            get => _buscaPais.Nombre;
            set
            {
                if (_buscaPais.Nombre != value)
                {
                    _buscaPais.Nombre = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Region
        {
            get => _buscaPais.Region;
            set
            {
                if (_buscaPais.Region != value)
                {
                    _buscaPais.Region = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Id => _buscaPais.ID;


        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }



        public BuscaPaisViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pais.db3");
            _buscaPaisRepository = new BuscarPaisRepository(dbPath);

            _buscaPais = new Modelos.BuscaPais();
            PeopleList = new ObservableCollection<Modelos.BuscaPais>();
            SaveCommand = new AsyncRelayCommand(Save);
            GetAllPeopleCommand = new AsyncRelayCommand(LoadPeople);
            DeletePersonCommand = new AsyncRelayCommand<Modelos.BuscaPais>((person) => Eliminar(person));
        }

        private async Task Save()
        {
            try
            {
                if (string.IsNullOrEmpty(_buscaPais.Nombre))
                {
                    throw new Exception("El nombre no puede estar vacío.");
                }
                if (string.IsNullOrEmpty(_buscaPais.Region))
                {
                    throw new Exception("El nombre no puede estar vacío.");
                }
                _buscaPaisRepository.agregarBuscaPais(_buscaPais.Nombre, _buscaPais.Region);

                StatusMessage = $"Persona {_buscaPais.Nombre} guardada exitosamente.";
                await Shell.Current.GoToAsync($"..?saved={_buscaPais.Nombre}");
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al guardar la persona: {ex.Message}";
            }
        }

        private async Task Eliminar(Modelos.BuscaPais personaAEliminar)
        {
            try
            {
                if (personaAEliminar == null)
                {
                    throw new Exception("Persona no válida.");
                }

                _buscaPaisRepository.EliminarPersona(personaAEliminar.Nombre);
                PeopleList.Remove(personaAEliminar);
                StatusMessage = $"Se eliminó a {personaAEliminar.Nombre}.";

                await Shell.Current.DisplayAlert("Aviso!", $"Gabriel Calderón acaba de eliminar a {personaAEliminar.Nombre}", "Aceptar");
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar a la persona: {ex.Message}";
            }
        }


        private async Task LoadPeople()
        {
            try
            {
                var people = _buscaPaisRepository.GetAllPeople();
                PeopleList.Clear();
                foreach (var person in people)
                {
                    PeopleList.Add(person);
                }

                StatusMessage = $"Se cargaron {PeopleList.Count} personas.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al obtener personas: {ex.Message}";
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("person") && query["person"] is Modelos.BuscaPais person)
            {
                BuscaPais = person;
            }
            else if (query.ContainsKey("deleted"))
            {
                string nombre = query["deleted"].ToString();
                Modelos.BuscaPais matchedPerson = PeopleList.FirstOrDefault(p => p.Nombre == nombre);

                if (matchedPerson != null)
                    PeopleList.Remove(matchedPerson);
            }
        }
    }
}
