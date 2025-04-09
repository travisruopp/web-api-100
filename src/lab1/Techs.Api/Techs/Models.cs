
using FluentValidation;
using Newtonsoft.Json;

namespace Techs.Api.Techs;


public record TechCreateModel(string FirstName, string LastName, string Sub, string Email, string Phone);

public record TechResponseModel(Guid Id, string FirstName, string LastName, string Sub, string Email, string Phone);

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        RuleFor(x=>x.FirstName).NotEmpty().WithMessage("FirstName is required.");
        RuleFor(x =>x.LastName).NotEmpty().WithMessage("LastName is required.");
        RuleFor(x =>x.Email).EmailAddress().WithMessage("Email looks wrong.");
        var SubStartsWith = new char[] { 'a', 'x' };
        RuleFor(x => x.Sub).Must(str => SubStartsWith.Any(c => str.StartsWith(c.ToString()))).WithMessage("Sub must start with an x or a").When(t => string.IsNullOrEmpty(t.Sub) == false);
    }
}