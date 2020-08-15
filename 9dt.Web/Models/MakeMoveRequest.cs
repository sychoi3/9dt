using _9dt.Web.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Models
{
    public class MakeMoveRequest
    {
        public int column { get; set; }
    }
    public class MakeMoveRequestValidator : AbstractValidator<MakeMoveRequest>
    {
        public MakeMoveRequestValidator(IGameService gameService)
        {
            //var columnCount = gameService.GetColumnCount 
            //RuleFor(x => x.column)
            //    .Must(x => x.Count == 2)
            //    .WithMessage("Must have exactly 2 players.");
        }
    }
}
