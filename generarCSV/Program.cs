/*  Generador de codigos seguros de verificacion (CSV)
    Creado por Carlos Clemente - sep/2023
    En cada ejecucion se genera una cadena de 13 caracteres con 10 caracteres aleatorios de un patron de caracteres, seguido de 2 caracteres obtenidos a partir de la fecha de generacion y el ultimo es un digito de control
    Una cadena aleatoria de 12 caracteres desde un patron de 36 caracteres, genera unos 4,7 cuatrillones de combinaciones posibles, por lo que es practicamente imposible que se repita el mismo numero, ya que aunque se generasen varios en el mismo segundo (cosa que no es posible), se garantiza la unicidad por la cadena de caracteres aleatorios.
*/

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace generarCSV
{
    internal class Program
    {
        //Definicion de variables
        private static readonly string Caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; //Caracteres que se utilizan en la generacion
        private static readonly Random Random = new Random();
        private static int largoCadena = 10; //Se corresponde con los caracteres aleatorios

        static void Main(string[] parametros)
        {
            // Comprueba si se pasa como parametro el numero de caracteres a generar
            if (parametros.Length > 0)
            {
                switch (parametros[0])
                {
                    case "-c":
                        if (parametros.Length > 1)
                        {
                            //compruebaCSV(parametros[1]);
                            chequeaCSV(parametros[1]);
                        }
                        else
                        {
                            Console.WriteLine("Debe introducir un csv");
                            Console.ReadLine();
                        }
                        break;

                    case "-h":
                        Console.WriteLine("Uso de la aplicacion");
                        Console.WriteLine("generarCSV [opciones]\n");
                        Console.WriteLine("Opciones:");
                        Console.WriteLine("   -c CSV a comprobar");
                        Console.WriteLine("   -h esta ayuda\n");
                        Console.WriteLine("Pulse una tecla para salir");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                //Se genera el CSV con los caracteres pasados
                string codigoCSV = generarCSV();

                //Guarda el resultado en un archivo
                string nombreArchivo = "codigoCSV.txt";
                try
                {
                    using (StreamWriter grabar = new StreamWriter(nombreArchivo))
                    {
                        string texto = $"El codigo CSV generado es: {codigoCSV}";
                        grabar.WriteLine(texto);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al generar el archivo: " + ex.Message);
                }
            }
        }

        public static string generarCSV()
        {
            string cadenaAleatoria = caracteresAleatorios(largoCadena);

            string cadenaFecha = caracteresFecha();

            string csvPrevio = cadenaAleatoria + cadenaFecha;

            char digitoControl = calculoDigitoControl(csvPrevio);

            return csvPrevio + digitoControl;
        }
        private static string caracteresAleatorios(int longitud)
        {
            // Generar cadena de n caracteres de forma aleatoria
            StringBuilder csv = new StringBuilder();
            byte[] randomBytes = new byte[longitud];

            // Usar RandomNumberGenerator para llenar el arreglo de bytes aleatorios
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            // Convertir cada byte a un índice dentro de la cadena de caracteres permitidos
            foreach (byte b in randomBytes)
            {
                int index = b % Caracteres.Length;
                csv.Append(Caracteres[index]);
            }

            return csv.ToString();
        }

        static char calculoDigitoControl(string cadena)
        {
            int suma = 0;
            int peso = 1;

            // Iterar a través de los caracteres desde el final hacia el principio
            foreach (char caracter in cadena.Reverse())
            {
                // Si el carácter es un número, calcula su valor numérico
                if (char.IsDigit(caracter))
                {
                    int digito = caracter - '0';
                    suma += digito * peso;
                }
                // Si el carácter es una letra, calcula su valor numérico basado en su posición en el alfabeto
                else if (char.IsLetter(caracter))
                {
                    int valorLetra = char.ToUpper(caracter) - 'A' + 10; // A=10, B=11, ..., Z=35
                    suma += valorLetra * peso;
                }

                // Alterna el peso (1 o 2)
                peso = (peso == 1) ? 2 : 1;

            }

            // Calcula el dígito de control como el residuo de la suma dividida por 36 (números + letras) y devuelve el caracter que corresponde al indice del patron de carateres.
            return Caracteres[suma % 36];
        }


        private static string caracteresFecha()
        {
            DateTime fechaActual = DateTime.Now;
            string fecha = fechaActual.ToString("ddMMyy");

            int indice1 = int.Parse(fecha) % 36; //Primer caracter con la fecha sin modificar
            int indice2 = int.Parse(new string(fecha.Reverse().ToArray())) % 36; //Segundo caracter con la fecha al reves

            return $"{Caracteres[indice1]}{Caracteres[indice2]}";
        }

        private static void chequeaCSV(string _csv)
        {
            string texto = string.Empty;
            string nombreArchivo = "codigoCSV.txt";
            if (_csv.Length != largoCadena + 3 || _csv == null)
            {
                texto = $"El CSV {_csv} debe tener una longitud de {largoCadena + 3} y solo tiene {_csv.Length}";
            }
            else
            {
                texto = (calculoDigitoControl(_csv.Substring(0, _csv.Length - 1)) == _csv[_csv.Length - 1]) ? $"El CSV {_csv} es correcto" : $"El CSV {_csv} no es correcto";
            }

            try
            {
                using (StreamWriter grabar = new StreamWriter(nombreArchivo))
                {
                    grabar.WriteLine(texto);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al generar el archivo: " + ex.Message);
            }
        }
    }
}