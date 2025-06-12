using FluentValidation;
using UPC.SmartLock.BE.Hogar.Request;
using UPC.SmartLock.BL.Util;

namespace UPC.SmartLock.BL.Homes
{
    public class HomeValidator : ValidatorBase<IHogarRequest>
    {
        public HomeValidator() : base()
        {
            RuleFor(hogar => hogar.Direccion)
                .NotEmpty().WithMessage("La dirección del hogar no puede estar vacía.")
                .MaximumLength(100).WithMessage("La dirección no puede exceder los 100 caracteres.");

            RuleFor(hogar => hogar.Nombre)
                .MaximumLength(50).WithMessage("El nombre del hogar no puede exceder los 50 caracteres.");

            //RuleFor(hogar => hogar.PropietarioId)
            //    .GreaterThan(0).WithMessage("El ID del propietario debe ser un número entero positivo.") 
            //    .NotNull().WithMessage("El ID del propietario no puede ser nulo."); 
        }
    }
}
