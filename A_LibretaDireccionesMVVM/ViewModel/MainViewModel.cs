using A_LibretaDireccionesMVVM.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace A_LibretaDireccionesMVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private GestorDeDatos gestorDeDatos;

        private ObservableCollection<Contacto> listaContactos;

        public ObservableCollection<Contacto> ListaContactos
        {
            get { return listaContactos; }
            set
            {
                listaContactos = value;
                OnPropertyChanged("ListaContactos");
            }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        private string apellidos;
        public string Apellidos
        {
            get { return apellidos; }
            set
            {
                apellidos = value;
                OnPropertyChanged("Apellidos");
            }
        }

        private string funcion;
        public string Funcion
        {
            get { return funcion; }
            set
            {
                funcion = value;
                OnPropertyChanged("Funcion");
            }
        }

        private string empresa;
        public string Empresa
        {
            get { return empresa; }
            set
            {
                empresa = value;
                OnPropertyChanged("Empresa");
            }
        }

        private string telefono;
        public string Telefono
        {
            get { return telefono; }
            set
            {
                telefono = value;
                OnPropertyChanged("Telefono");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string direccion1;
        public string Direccion1
        {
            get { return direccion1; }
            set
            {
                direccion1 = value;
                OnPropertyChanged("Direccion1");
            }
        }

        private string direccion2;
        public string Direccion2
        {
            get { return direccion2; }
            set
            {
                direccion2 = value;
                OnPropertyChanged("Direccion2");
            }
        }

        private string codigoPostal;
        public string CodigoPostal
        {
            get { return codigoPostal; }
            set
            {
                codigoPostal = value;
                OnPropertyChanged("CodigoPostal");
            }
        }

        private string ciudad;
        public string Ciudad
        {
            get => ciudad;
            set
            {
                ciudad = value;
                OnPropertyChanged("Ciudad");
            }
        }

        private Contacto contactoSeleccionado;
        public Contacto ContactoSeleccionado
        {
            get { return contactoSeleccionado; }
            set
            {
                contactoSeleccionado = value;
                OnPropertyChanged("ContactoSeleccionado");

                if (contactoSeleccionado == null)
                    ActivarEliminacionYEdicion = false;
                else
                {
                    ActivarEliminacionYEdicion = true;

                    Nombre = contactoSeleccionado.Nombre;
                    Apellidos = contactoSeleccionado.Apellidos;

                    Funcion = contactoSeleccionado.Funcion;

                    Empresa = contactoSeleccionado.Empresa;

                    Telefono = contactoSeleccionado.Telefono;

                    Email = contactoSeleccionado.Email;

                    Direccion1 = contactoSeleccionado.Direccion1;

                    Direccion2 = contactoSeleccionado.Direccion2;

                    CodigoPostal = contactoSeleccionado.CodigoPostal;

                    Ciudad = contactoSeleccionado.Ciudad;
                }
            }
        }

        private bool activarEliminacionYEdicion;

        public bool ActivarEliminacionYEdicion
        {
            get { return activarEliminacionYEdicion; }
            set
            {
                activarEliminacionYEdicion = value;
                OnPropertyChanged("ActivarEliminacionYEdicion");
            }
        }

        public ICommand ComandoNuevoContacto { get; set; }
        public ICommand ComandoEliminarContacto { get; set; }
        public ICommand ComandoGuardarTodo { get; set; }

        public MainViewModel()
        {
            gestorDeDatos = new GestorDeDatos();

            Contacto[] contactos = gestorDeDatos.LeerTodosLosRegistros();
            ListaContactos = new ObservableCollection<Contacto>(contactos);

            ComandoNuevoContacto = new Command(AccionNuevoContacto);
            ComandoEliminarContacto = new Command(AccionEliminarContacto);
            ComandoGuardarTodo = new Command(AccionGuardarTodo);
        }

        private void AccionGuardarTodo(object parametro)
        {
            gestorDeDatos.Guardar(ListaContactos);
        }

        private void AccionEliminarContacto(object parametro)
        {
            if (ContactoSeleccionado != null)
            {
                ListaContactos.Remove(ContactoSeleccionado);

                Nombre = "";
                Apellidos = "";
                Funcion = "";
                Empresa = "";
                Telefono = "";
                Email = "";
                Direccion1 = "";
                Direccion2 = "";
                CodigoPostal = "";
                Ciudad = "";
            }
        }



        private void AccionNuevoContacto(object parametro)
        {
            if (Nombre == "" ||
                Apellidos == "" ||
                Empresa == "" ||
                Telefono == "")
            {
                // Show error window
                ShowWarning("Nombre, Apellidos, Empresa y Teléfono son obligatorios");
            }
            else
            {
                if (ContactoSeleccionado != null)
                {
                    ContactoSeleccionado.Apellidos = Apellidos;

                    ContactoSeleccionado.Nombre = Nombre;

                    ContactoSeleccionado.Funcion = Funcion;

                    ContactoSeleccionado.Empresa = Empresa;

                    ContactoSeleccionado.Telefono = Telefono;

                    ContactoSeleccionado.Email = Email;

                    ContactoSeleccionado.Direccion1 = Direccion1;

                    ContactoSeleccionado.Direccion2 = Direccion2;

                    ContactoSeleccionado.CodigoPostal = CodigoPostal;

                    ContactoSeleccionado.Ciudad = Ciudad;

                    OnPropertyChanged("ContactoSeleccionado");

                    //Refrescamos el LisBox después de modificar el elemento modificado
                    gestorDeDatos.Guardar(ListaContactos);
                    Contacto[] contactos = gestorDeDatos.LeerTodosLosRegistros();
                    ListaContactos = new ObservableCollection<Contacto>(contactos);

                }
                else
                {
                    Contacto contacto = new Contacto
                    {
                        Apellidos = Apellidos,
                        Nombre = Nombre,
                        Funcion = Funcion,
                        Empresa = Empresa,
                        Telefono = Telefono,
                        Email = Email,
                        Direccion1 = Direccion1,
                        Direccion2 = Direccion2,
                        CodigoPostal = CodigoPostal,
                        Ciudad = Ciudad
                    };
                    ListaContactos.Add(contacto);
                }
                EmptyFields();
            }
        }

        private void EmptyFields()
        {
            Nombre = "";
            Apellidos = "";
            Funcion = "";
            Empresa = "";
            Telefono = "";
            Email = "";
            Direccion1 = "";
            Direccion2 = "";
            CodigoPostal = "";
            Ciudad = "";
        }

        private static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Input Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}