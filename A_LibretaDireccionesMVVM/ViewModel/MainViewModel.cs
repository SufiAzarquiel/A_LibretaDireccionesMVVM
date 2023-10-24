using A_LibretaDireccionesMVVM.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                    /* Empresa
                     Telefono 
                     Email 
                     Direccion1 
                     Direccion2 
                     CodigoPostal
                     Ciudad */
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
                /* Empresa
                     Telefono 
                     Email 
                     Direccion1 
                     Direccion2 
                     CodigoPostal
                     Ciudad */

            }
        }

        private void AccionNuevoContacto(object parametro)
        {

            if (ContactoSeleccionado != null)
            {
                ContactoSeleccionado.Apellidos = Apellidos;

                ContactoSeleccionado.Nombre = Nombre;

                ContactoSeleccionado.Funcion = Funcion;

                /* Empresa
                     Telefono 
                     Email 
                     Direccion1 
                     Direccion2 
                     CodigoPostal
                     Ciudad */

                OnPropertyChanged("ContactoSeleccionado");

                //Refrescamos el LisBox después de modificar el elemento modificado
                gestorDeDatos.Guardar(ListaContactos);
                Contacto[] contactos = gestorDeDatos.LeerTodosLosRegistros();
                ListaContactos = new ObservableCollection<Contacto>(contactos);

            }
            else
            {
                /* Empresa
                     Telefono 
                     Email 
                     Direccion1 
                     Direccion2 
                     CodigoPostal
                     Ciudad */
                Contacto contacto = new Contacto { Apellidos = Apellidos, Nombre = Nombre, Funcion = Funcion };
                ListaContactos.Add(contacto);
            }

            Nombre = "";
            Apellidos = "";
            Funcion = "";
            /* Empresa
                     Telefono 
                     Email 
                     Direccion1 
                     Direccion2 
                     CodigoPostal
                     Ciudad */

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        /* Empresa
                     Telefono 
                     Email 
                     Direccion1 
                     Direccion2 
                     CodigoPostal
                     Ciudad */
    }
}