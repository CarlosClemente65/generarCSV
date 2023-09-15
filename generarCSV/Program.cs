/*  Generador de codigos seguros de verificacion (CSV)
    Creado por Carlos Clemente - sep/2023
    En cada ejecucion se genera una cadena de 8 caracteres de forma aleatoria y se le
    añade al principio los dias transcurridos desde el 01/01/2000 y al final los segundos
    del dia de hoy hasta el momento de la generacion.
    La combinacion de los 8 caracteres (1,5 billones de combinaciones posibles) junto a la 
    referencia de los dias y segundos, hace practicamente imposible que se repita el mismo numero,
    ya que aunque se generasen varios en el mismo segundo (cosa que no es posible), se garantiza
    la unicidad por la cadena de 8 caracteres aleatorios
*/

using System;
using System.IO;
using System.Text;

namespace generarCSV
{
    internal class Program
    {
        //Definicion de variables
        private static readonly string Caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random Random = new Random();

        static void Main(string[] args)
        {
            //Se genera el CSV con 8 caracteres
            string codigoCSV = CSVGenerator(8);
            //Console.WriteLine(codigoCSV);
            //Console.ReadLine();

            //Guarda el resultado en un archivo
            string nombreArchivo = "resultado.txt";
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

        public static string CSVGenerator(int longitud)
        {
            // Generar cadena de 8 caracteres de forma aleatoria (son 1,5 billones de combinaciones posibles)
            StringBuilder csv = new StringBuilder();
            for (int i = 0; i < longitud; i++)
            {
                // Elegir un carácter aleatorio de la cadena de caracteres
                char randomChar = Caracteres[Random.Next(Caracteres.Length)];
                csv.Append(randomChar);
            }
            string csvCalculado = csv.ToString();

            // Calculo de los dias que han pasado desde el 01/01/2000
            TimeSpan difDias = DateTime.Now - new DateTime(2000, 1, 1);
            int totalDias = (int)difDias.TotalDays;
            string hexDias = totalDias.ToString("X5"); //Se pasan a hexadecimal

            //Calculo de los segundos transcurridos hoy en formato hexadecimal
            string hexSegundos = (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second * 60).ToString("X5");
            csvCalculado = hexDias + csvCalculado.ToString() + hexSegundos;

            return csvCalculado;
        }
    }
}
