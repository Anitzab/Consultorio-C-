using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace WindowsFormsApp9.Controlador
{
    public class Validaciones
    {
        public static bool soloLetras(string _cad)
        {   //Regex regex = new Regex(@"^[^ ][a-zA-Z ]+[^ ]$");
            Regex regex = new Regex(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$");
            return regex.IsMatch(_cad);
        }
        public static bool LetrasNumeros(string _cad)
        {   //Regex regex = new Regex(@"^[^ ][a-zA-Z ]+[^ ]$");
            Regex regex = new Regex(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$");
            return regex.IsMatch(_cad);
        }

        public static bool soloDigitos(string _cad)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            return regex.IsMatch(_cad);
        }

        public static bool es_telefono(string _cad)
        {
            Regex regex = new Regex(@"^([0-9]{3})[-. ]?([0-9]{4})$");
            return regex.IsMatch(_cad);
        }

        public static bool es_decimal(string _cad)
        {
            Regex regex = new Regex(@"^[0-9]{1,9}([\.\,][0-9]{1,3})?$");
            return regex.IsMatch(_cad);
        }

        public static bool es_url(string _cad)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\-\.]+\.(com|org|net|mil|edu|COM|ORG|NET|MIL|EDU)$");
            return regex.IsMatch(_cad);
        }
        public static bool es_cp(string _cad)
        {
            Regex regex = new Regex(@"^([1-9]{2}|[0-9][1-9]|[1-9][0-9])[0-9]{3}$");
            return regex.IsMatch(_cad);
        }


        public static bool es_email(string _cad)
        {
            Regex regex = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");

            // Resultado: 
            //       Valid: david.jones@proseware.com 
            //       Valid: d.j@server1.proseware.com 
            //       Valid: jones@ms1.proseware.com 
            //       Invalid: j.@server1.proseware.com 
            //       Invalid: j@proseware.com9 
            //       Valid: js#internal@proseware.com 
            //       Valid: j_9@[129.126.118.1] 
            //       Invalid: j..s@proseware.com 
            //       Invalid: js*@proseware.com 
            //       Invalid: js@proseware..com 
            //       Invalid: js@proseware.com9 
            //       Valid: j.s@server1.proseware.com

            return regex.IsMatch(_cad);
        }
    }
}
