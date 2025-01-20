using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.Json;
using System.Reflection;
using EvaluacionP3EmilioGuerrero.Service;
using EvaluacionP3EmilioGuerrero.Modelos;
using EvaluacionP3EmilioGuerrero.Repositories;

namespace EvaluacionP3EmilioGuerrero.ViewModels
{
    internal class PaisViewModel : ObservableObject, IQueryAttributable
    {

        private readonly PaisService _paisService;
        private string _statusMessage;
        public ObservableCollection<Pais> Paises { get; set; }

        private Modelos.Pais _pais;
        private readonly PaisesRepository _paisesRepository;

        public ICommand SaveCommand { get; }
        public ICommand GetAllPaisCommand { get; }
        public ICommand DeletePaisCommand { get; }
        public ICommand FetchFromApiCommand { get; }
        public Modelos.Pais Pais
        {
            get => _pais;
            set
            {
                if (SetProperty(ref _pais, value)) 
                {
                    OnPropertyChanged(nameof(_pais.Nombre));
                    OnPropertyChanged(nameof(_pais.Region));

                }
            }
        }
        public string Nombre
        {
            get => _pais.Nombre;
            set
            {
                if (_pais.Nombre != value)
                {
                    _pais.Nombre = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Region
        {
            get => _pais.Region;
            set
            {
                if (_pais.Region != value)
                {
                    _pais.Region = value;
                    OnPropertyChanged();
                }
            }
        }


        public int Id => _pais.ID;
        public PaisViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pais.db3");
            _paisesRepository = new PaisesRepository(dbPath);

            _pais = new Modelos.Pais();
            _paisService = new PaisService();
            Paises = new ObservableCollection<Modelos.Pais>();
            SaveCommand = new AsyncRelayCommand(Save);
            GetAllPaisCommand = new AsyncRelayCommand(LoadPeople);
            DeletePaisCommand = new AsyncRelayCommand<Modelos.Pais>((person) => Eliminar(person));
            FetchFromApiCommand = new AsyncRelayCommand(FetchFromApi);
        }
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadUsersAsync()
        {

            var paises = await _paisService.GetPaisAsync();
            Paises.Clear();
            foreach (var pais in paises)
            {
                Paises.Add(pais);
                Console.WriteLine(pais.ToString);
            }
        }
        private async Task Save()
        {
            try
            {
                if (string.IsNullOrEmpty(_pais.Nombre))
                {
                    throw new Exception("El nombre no puede estar vacío.");
                }
                if (string.IsNullOrEmpty(_pais.Region))
                {
                    throw new Exception("El nombre no puede estar vacío.");
                }
                if (string.IsNullOrEmpty(_pais.Link))
                {
                    throw new Exception("El nombre no puede estar vacío.");
                }
                _paisesRepository.agregarPais(_pais.Nombre, _pais.Region, _pais.Link);

                StatusMessage = $"Persona {_pais.Nombre} guardada exitosamente.";
                await Shell.Current.GoToAsync($"..?saved={_pais.Nombre}");
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al guardar al pais: {ex.Message}";
            }
        }
        private async Task Eliminar(Modelos.Pais paisAEliminar)
        {
            try
            {
                if (paisAEliminar == null)
                {
                    throw new Exception("Persona no válida.");
                }

                _paisesRepository.EliminarPais(paisAEliminar.Nombre);
                Paises.Remove(paisAEliminar);
                StatusMessage = $"Se eliminó a {paisAEliminar.Nombre}.";

                await Shell.Current.DisplayAlert("Aviso!", $"Emilio Guerrero acaba de eliminar a {paisAEliminar.Nombre}", "Aceptar");
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar al pais: {ex.Message}";
            }
        }
        private async Task LoadPeople()
        {
            try
            {
                var people = _paisesRepository.GetAllPais();
                Paises.Clear();
                foreach (var person in people)
                {
                    Paises.Add(person);
                }

                StatusMessage = $"Se cargaron {Paises.Count} personas.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al obtener personas: {ex.Message}";
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("person") && query["person"] is Modelos.Pais person)
            {
                Pais = person;
            }
            else if (query.ContainsKey("deleted"))
            {
                string nombre = query["deleted"].ToString();
                Modelos.Pais matchedPerson = Paises.FirstOrDefault(p => p.Nombre == nombre);

                if (matchedPerson != null)
                    Paises.Remove(matchedPerson);
            }
        }



        private async Task FetchFromApi()
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync("https://restcountries.com/v3.1/name");

                var paises = JsonSerializer.Deserialize<List<Modelos.Pais>>(response);

                foreach (var pais in paises)
                {
                    _paisesRepository.agregarPais(pais.Nombre, pais.Region, pais.Link);
                }

                StatusMessage = "Datos importados desde la API correctamente.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al importar datos: {ex.Message}";
            }
        }
    }
}
