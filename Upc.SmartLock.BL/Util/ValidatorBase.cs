using FluentValidation;

namespace UPC.SmartLock.BL.Util
{
    public class ValidatorBase<T> : AbstractValidator<T>
    {
        protected const string patronCorreo = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
        protected const string patronAlfanumericoConEspacioYSinWhitespace = @"^[\x20-\x7E\x80-\xFE]*$";  // desde " " hasta "~" y desde "Ç" hasta "■"
        protected const string patronAlfanumericoSinEspacioYSinWhitespace = @"^[\x21-\x7E\x80-\xFE]*$";  // desde "!" hasta "~" y desde "Ç" hasta "■"

        public ValidatorBase()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;
        }

        protected bool NoNuloVacio(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return false;

            return true;
        }

        protected bool EsMayor(float? valorActual, float valorMinimo = 0.000f)
        {
            if (!valorActual.HasValue) return true;

            return (valorActual.Value > valorMinimo);
        }

        protected bool EsMayor(double? valorActual, double valorMinimo = 0.000d)
        {
            if (!valorActual.HasValue) return true;

            return (valorActual.Value > valorMinimo);
        }

        protected bool ValidarFecha(DateTime fecha)
        {
            return !(fecha == null || fecha == DateTime.MinValue || fecha == DateTime.MaxValue);
        }

        protected bool EsFechaHoraValida(DateTime? fechahora)
        {
            if (fechahora == null) return false;
            return fechahora.Value.ToString("yyyy-MM-ddTHH:mm:ss").Equals(fechahora.Value.ToString("s"));
        }

        protected bool EsFechaHoraValida(DateTime fechahora)
        {
            return fechahora.ToString("yyyy-MM-ddTHH:mm:ss").Equals(fechahora.ToString("s"));
        }
    }
}
