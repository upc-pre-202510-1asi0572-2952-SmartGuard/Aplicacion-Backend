#region ensamblado Pecano.General.Core, Version=1.22.83.1, Culture=neutral, PublicKeyToken=null
// C:\Users\Ferna\.nuget\packages\pecano.general.core\1.22.83.1\lib\netcoreapp3.1\Pecano.General.Core.dll
// Decompiled with ICSharpCode.Decompiler 8.1.1.7464
#endregion

using System.Runtime.CompilerServices;
using System.Text;


namespace UPC.SmartLock.BE.Util.Librarys
{
    public static class Constantes
    {
        private static Encoding _ENCODING;

        public static Encoding Encoding
        {
            get
            {
                if (_ENCODING == null)
                {
                    _ENCODING = Encoding.UTF8;
                }

                return _ENCODING;
            }
            set
            {
                _ENCODING = value;
            }
        }

        public static IEnumerable<T> ObtenerValoresFlag<T>(this T flags) where T : System.Enum
        {
            ulong flag = 1uL;
            foreach (T item in System.Enum.GetValues(flags.GetType()).Cast<T>())
            {
                ulong num = Convert.ToUInt64(item);
                while (flag < num)
                {
                    flag <<= 1;
                }

                if (flag == num && flags.HasFlag(item))
                {
                    yield return item;
                }
            }
        }

        private static string ObtenerValorEntorno(string variableName)
        {
            if (string.IsNullOrEmpty(variableName))
            {
                throw new ArgumentNullException("The parameter variableName cannot be null or empty");
            }

            return Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Process) ?? throw new Exception("The property " + variableName + " doesn't have a value");
        }

        public static string ObtenerCadenaEntorno(ref string variableValue, [CallerMemberName] string variableName = "")
        {
            if (string.IsNullOrEmpty(variableValue))
            {
                variableValue = ObtenerValorEntorno(variableName);
            }

            return variableValue;
        }

        public static int ObtenerEnteroEntorno(ref int? variableValue, [CallerMemberName] string variableName = "")
        {
            if (!variableValue.HasValue)
            {
                int result = 0;
                if (!int.TryParse(ObtenerValorEntorno(variableName), out result))
                {
                    throw new Exception("The variable " + variableName + " is not an int");
                }

                variableValue = result;
            }

            return variableValue.Value;
        }

        public static double ObtenerDoubleEntorno(ref double? variableValue, [CallerMemberName] string variableName = "")
        {
            if (!variableValue.HasValue)
            {
                double result = 0.0;
                if (!double.TryParse(ObtenerValorEntorno(variableName), out result))
                {
                    throw new Exception("The variable " + variableName + " is not a double");
                }

                variableValue = result;
            }

            return variableValue.Value;
        }

        public static bool ObtenerBooleanoEntorno(ref bool? variableValue, [CallerMemberName] string variableName = "")
        {
            if (!variableValue.HasValue)
            {
                bool result = false;
                if (!bool.TryParse(ObtenerValorEntorno(variableName), out result))
                {
                    throw new Exception("The variable " + variableName + " is not a boolean");
                }

                variableValue = result;
            }

            return variableValue.Value;
        }

        public static TimeSpan ObtenerTiempoEntorno(ref TimeSpan? variableValue, [CallerMemberName] string variableName = "")
        {
            if (!variableValue.HasValue)
            {
                TimeSpan result = default(TimeSpan);
                if (!TimeSpan.TryParse(ObtenerValorEntorno(variableName), out result))
                {
                    throw new Exception("The variable " + variableName + " is not a TimeSpan");
                }

                variableValue = result;
            }

            return variableValue.Value;
        }

        public static DateTime ObtenerFechaEntorno(ref DateTime? variableValue, [CallerMemberName] string variableName = "")
        {
            if (!variableValue.HasValue)
            {
                DateTime result = default(DateTime);
                if (!DateTime.TryParse(ObtenerValorEntorno(variableName), out result))
                {
                    throw new Exception("The variable " + variableName + " is not a DateTime");
                }

                variableValue = result;
            }

            return variableValue.Value;
        }

        public static Uri ObtenerUrlEntorno(ref Uri variableValue, [CallerMemberName] string variableName = "")
        {
            if (variableValue == null)
            {
                Uri result = null;
                if (!Uri.TryCreate(ObtenerValorEntorno(variableName), UriKind.Absolute, out result))
                {
                    throw new Exception("The variable " + variableName + " is not an Uri");
                }

                variableValue = result;
            }

            return variableValue;
        }
    }
}
