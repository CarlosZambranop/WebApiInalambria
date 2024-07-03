
using WebApiPruebaInalambria.Model;
namespace WebApiPruebaInalambria.Backend
{
    public class Converter
    {
        private static readonly Dictionary<int, string> Unidad = new Dictionary<int, string>
        {        //Creo un dicionario para los valores menores a mil
                {0, "CERO"}, {1, "UNO"}, {2, "DOS"}, {3, "TRES"}, {4, "CUATRO"}, {5, "CINCO"},
                {6, "SEIS"}, {7, "SIETE"}, {8, "OCHO"}, {9, "NUEVE"}, {10, "DIEZ"},
                {11, "ONCE"}, {12, "DOCE"}, {13, "TRECE"}, {14, "CATORCE"}, {15, "QUINCE"},
                {20, "VEINTE"}, {30, "TREINTA"}, {40, "CUARENTA"}, {50, "CINCUENTA"},
                {60, "SESENTA"}, {70, "SETENTA"}, {80, "OCHENTA"}, {90, "NOVENTA"},
                {100, "CIEN"}, {200, "DOSCIENTOS"}, {300, "TRESCIENTOS"}, {400, "CUATROCIENTOS"},
                {500, "QUINIENTOS"}, {600, "SEISCIENTOS"}, {700, "SETECIENTOS"}, {800, "OCHOCIENTOS"},
                {900, "NOVECIENTOS"}
        };
        public Response ConverterIntToString(Request request)
        {
            ///Creo una instacia de la clase response
            Response response = new Response();
            //Valido que el numero se pueda convertir en double 
            if (double.TryParse(request.NumeroRequest, out double value))
            {
                if (value > 999999999999)
                {
                    response.StringResponse = "El numero excede los digitos posibles del sistema";
                }
                else
                {
                    //llamo la funcion para convertir el numero a su pronunciacion 
                    response.StringResponse = ToText(value);
                }
            }
            else
            {

                response.StringResponse = "No es posible convertir ese numero a su pronunciacion";
                //Si no se puede convertir sale mensaje avisando el problema 
            }
            return response;

        }
        private string ToText(double value)
        {
            //Convierte decimal en entero
            value = Math.Truncate(value);
            //Busca si en el dicionario existe alguna con el valor de 0 a 900
            if (Unidad.ContainsKey((int)value))
            {
                return Unidad[(int)value];
            }
            //Empieza a añadirle las centenas y decenas 
            if (value < 20)
            {
                return "DIECI" + ToText(value - 10);
            }
            if (value < 30)
            {
                return "VEINTI" + ToText(value - 20);
            }
            if (value < 100)
            {
                int decena = (int)Math.Truncate(value / 10) * 10;
                return Unidad[decena] + " Y " + ToText(value % 10);
            }
            if (value < 200)
                return "CIENTO " + ToText(value - 100);
            if (value < 1000)
            {
                int centena = (int)Math.Truncate(value / 100) * 100;
                return Unidad[centena] + " " + ToText(value % 100);
            }
            if (value < 2000)
                return "MIL " + ToText(value % 1000);
            if (value < 1000000)
            {
                string resultado = ToText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0)
                    resultado += " " + ToText(value % 1000);
                return resultado;
            }
            if (value < 2000000)
                return "UN MILLON " + ToText(value % 1000000);
            if (value < 1000000000)
            {
                string resultado = ToText(Math.Truncate(value / 1000000)) + " MILLONES";
                if ((value % 1000000) > 0)
                    resultado += " " + ToText(value % 1000000);
                return resultado;
            }
            if (value < 2000000000)
                return "MIL MILLONES " + ToText(value % 1000000000);
            string resultadoFinal = ToText(Math.Truncate(value / 1000000000)) + " MIL MILLONES";
            if ((value % 1000000000) > 0)
                resultadoFinal += " " + ToText(value % 1000000000);
            //Devuelve el valor convertido en su pronunaciacion
            return resultadoFinal;
        }
    }
}
