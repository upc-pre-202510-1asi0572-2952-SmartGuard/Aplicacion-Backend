using FluentValidation;
using UPC.SmartLock.BE.Hogar.Request;
using UPC.SmartLock.BL.Util;

namespace UPC.SmartLock.BL.Homes
{
    public class HomeMembersValidator : ValidatorBase<IAsociarMiembroRequest>
    {
        public HomeMembersValidator() : base()
        {
            RuleFor(miembro => miembro.HogarId)
                .GreaterThan(0).WithMessage("El ID del HogarId  debe ser un número entero positivo.")
                .NotNull().WithMessage("El ID del HogarId no puede ser nulo.");

            RuleFor(miembro => miembro.UserId)
                .GreaterThan(0).WithMessage("El ID del UserId debe ser un número entero positivo.")
                .NotNull().WithMessage("El ID del UserId no puede ser nulo.");
        }
    }
}
