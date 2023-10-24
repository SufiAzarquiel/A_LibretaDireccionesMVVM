using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using System.IO;

namespace A_LibretaDireccionesMVVM.Model
{
    internal class GestorDeDatos
    {
        private const string archivoDatos = "contactos.txt";

        public Contacto[] LeerTodosLosRegistros()
        {
            if (!File.Exists(archivoDatos))
                return new Contacto[0];

            string[] registros = File.ReadAllLines(archivoDatos);

            Contacto[] resultado = new Contacto[registros.Length];

            for (int i = 0; i < registros.Length; i++)
            {
                string[] campos = registros[i].Split('\t');

                Contacto contacto = new Contacto
                {
                    Nombre = campos[0],
                    Apellidos = campos[1],
                    Funcion = campos[2],
                    Empresa = campos[3],
                    Telefono = campos[4],
                    Email = campos[5],
                    Direccion1 = campos[6],
                    Direccion2 = campos[7],
                    CodigoPostal = campos[8],
                    Ciudad = campos[9],
                };
                resultado[i] = contacto;
            }

            return resultado;
        }

        public void Guardar(IEnumerable<Contacto> contactos)
        {
            StringBuilder builder = new StringBuilder();

            foreach (Contacto c in contactos)
            {
                string registro = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}",
                                                     c.Nombre, c.Apellidos, c.Funcion,
                                                     c.Empresa, c.Telefono, c.Email,
                                                     c.Direccion1, c.Direccion2, c.CodigoPostal,
                                                     c.Ciudad);
                builder.AppendLine(registro);
            }


            File.WriteAllText(archivoDatos, builder.ToString());

        }

    }
}
