using FluentValidation;
using UPC.SmartLock.BE.Usuario.Request;
using UPC.SmartLock.BL.Util;

namespace UPC.SmartLock.BL.Users
{
    public class UserValidator : ValidatorBase<IUsuarioRequest>
    {
        public UserValidator() : base()
        {
            //RuleFor(x => x.Correo)
            //    .NotNull()
            //    .NotEmpty()
            //    .Matches(patronCorreo).WithMessage("El correo no tiene un formato válido.");

            //RuleFor(x => x.Contraseña)
            //    .NotNull()
            //    .NotEmpty()
            //    .MinimumLength(5).WithMessage("Cantidad minima de caracteres es 5")
            //    .MaximumLength(200).WithMessage("Cantidad maxima de caracteres es 200");

            //RuleFor(x => x.Nombre)
            //    .NotNull()
            //    .NotEmpty()
            //    .MinimumLength(5).WithMessage("Cantidad minima de caracteres es 5")
            //    .MaximumLength(200).WithMessage("Cantidad maxima de caracteres es 200");

        }
    }
}
