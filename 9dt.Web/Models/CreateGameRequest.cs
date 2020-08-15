using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace _9dt.Web.Models
{
    public class CreateGameRequest
    {
        public IList<string> players { get; set; }
        public int columns { get; set; }
        public int rows { get; set; }
    }

    public class CreateGameRequestValidator : AbstractValidator<CreateGameRequest>
    {
        public CreateGameRequestValidator()
        {
            //RuleFor(x => x.players)
            //    .NotEmpty()
            //    .Must(x => x.Count == 2)
            //    .WithMessage("Must have exactly 2 players.");
        }
    }
}
