using AzureTask.Dto;
using AzureTask.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AzureEmailTask.Domain.DTO
{
    public class CompleteModelDto
    {
        public NameDto Name { get; set; }
        public ContactDto Contact { get; set; }
        public string Date { get; set; }
        public int Size { get; set; }
        public string Service_Type { get; set; }
        public AddressDto From { get; set; }
        public AddressDto To { get; set; }

    }

    public class CompleteModelDtoValidator : AbstractValidator<CompleteModelDto>
    {
        public static List<StateModel> stateModel = new List<StateModel>()
        {
            new StateModel(){ StateAcronym = "AL", FromZip = 35004, ToZip = 36925 },
            new StateModel(){ StateAcronym = "AK", FromZip = 99501, ToZip = 99950 },
            new StateModel(){ StateAcronym = "AZ", FromZip = 85001, ToZip = 86556 },
            new StateModel(){ StateAcronym = "AR", FromZip = 71601, ToZip = 72959 },
            new StateModel(){ StateAcronym = "CA", FromZip = 90001, ToZip = 96162 },
            new StateModel(){ StateAcronym = "CO", FromZip = 80001, ToZip = 81658 },
            new StateModel(){ StateAcronym = "CT", FromZip = 06001, ToZip = 06928 },
            new StateModel(){ StateAcronym = "DE", FromZip = 19701, ToZip = 19980 },
            new StateModel(){ StateAcronym = "FL", FromZip = 32003, ToZip = 34997 },
            new StateModel(){ StateAcronym = "GA", FromZip = 30002, ToZip = 39901 },

            new StateModel(){ StateAcronym = "HI", FromZip = 96701, ToZip = 96898 },
            new StateModel(){ StateAcronym = "ID", FromZip = 83201, ToZip = 83877 },
            new StateModel(){ StateAcronym = "IL", FromZip = 60001, ToZip = 62999 },
            new StateModel(){ StateAcronym = "IN", FromZip = 46001, ToZip = 47997 },
            new StateModel(){ StateAcronym = "IA", FromZip = 50001, ToZip = 52809 },
            new StateModel(){ StateAcronym = "KS", FromZip = 66002, ToZip = 67954 },
            new StateModel(){ StateAcronym = "KY", FromZip = 40003, ToZip = 42788 },
            new StateModel(){ StateAcronym = "LA", FromZip = 70001, ToZip = 71497 },
            new StateModel(){ StateAcronym = "ME", FromZip = 03901, ToZip = 04992 },
            new StateModel(){ StateAcronym = "MD", FromZip = 20588, ToZip = 21930 },

            new StateModel(){ StateAcronym = "MA", FromZip = 01001, ToZip = 05544 },
            new StateModel(){ StateAcronym = "MI", FromZip = 48001, ToZip = 49971 },
            new StateModel(){ StateAcronym = "MN", FromZip = 55001, ToZip = 56763 },
            new StateModel(){ StateAcronym = "MS", FromZip = 38601, ToZip = 39776 },
            new StateModel(){ StateAcronym = "MO", FromZip = 63001, ToZip = 65899 },
            new StateModel(){ StateAcronym = "MT", FromZip = 59001, ToZip = 59937 },
            new StateModel(){ StateAcronym = "NE", FromZip = 68001, ToZip = 69367 },
            new StateModel(){ StateAcronym = "NV", FromZip = 88901, ToZip = 89883 },
            new StateModel(){ StateAcronym = "NH", FromZip = 03031, ToZip = 03897 },
            new StateModel(){ StateAcronym = "NJ", FromZip = 07001, ToZip = 08989 },

            new StateModel(){ StateAcronym = "NM", FromZip = 87001, ToZip = 88439 },
            new StateModel(){ StateAcronym = "NY", FromZip = 00501, ToZip = 14925 },
            new StateModel(){ StateAcronym = "NC", FromZip = 27006, ToZip = 28909 },
            new StateModel(){ StateAcronym = "ND", FromZip = 58001, ToZip = 58856 },
            new StateModel(){ StateAcronym = "OH", FromZip = 43001, ToZip = 45999 },
            new StateModel(){ StateAcronym = "OK", FromZip = 73001, ToZip = 74966 },
            new StateModel(){ StateAcronym = "OR", FromZip = 97001, ToZip = 97920 },
            new StateModel(){ StateAcronym = "PA", FromZip = 15001, ToZip = 19640 },
            new StateModel(){ StateAcronym = "RI", FromZip = 02801, ToZip = 02940 },
            new StateModel(){ StateAcronym = "SC", FromZip = 29001, ToZip = 29945 },

            new StateModel(){ StateAcronym = "SD", FromZip = 57001, ToZip = 57799 },
            new StateModel(){ StateAcronym = "TN", FromZip = 37010, ToZip = 38589 },
            new StateModel(){ StateAcronym = "TX", FromZip = 73301, ToZip = 88595 },
            new StateModel(){ StateAcronym = "UT", FromZip = 84001, ToZip = 84791 },
            new StateModel(){ StateAcronym = "VT", FromZip = 05001, ToZip = 05907 },
            new StateModel(){ StateAcronym = "VA", FromZip = 20101, ToZip = 24658 },
            new StateModel(){ StateAcronym = "WA", FromZip = 98001, ToZip = 99403 },
            new StateModel(){ StateAcronym = "WV", FromZip = 24701, ToZip = 26886 },
            new StateModel(){ StateAcronym = "WI", FromZip = 53001, ToZip = 54990 },
            new StateModel(){ StateAcronym = "WY", FromZip = 82001, ToZip = 83414 }
        };

        public static bool ValidateState(string acronym)
        {
            foreach (StateModel state in stateModel)
            {
                if (state.StateAcronym == acronym)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidateStateAndZip(int zip, string acronym)
        {
            string zipString = zip.ToString();
            foreach (StateModel state in stateModel)
            {
                if (acronym == state.StateAcronym && zip >= state.FromZip && zip <= state.ToZip && zipString.Length == 5)
                {
                    return true;
                }
            }
            return false;
        }
        public CompleteModelDtoValidator()
        {


            RuleFor(cm => cm.Name.First)
                .NotEmpty().WithMessage("First name cannot be empty");

            RuleFor(cm => cm.Name.Last)
                .NotEmpty().WithMessage("Last name cannot be empty");

            RuleFor(cm => cm.Contact.Email)
                .NotEmpty().WithMessage("Email cannot be epmty")
                .EmailAddress().WithMessage("Invalid email");

            RuleFor(cm => cm.Contact.Phone)
                .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
                .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("PhoneNumber not valid");

            RuleFor(cm => cm.Date)
                .NotEmpty().WithMessage("Date cannot be empty")
                .Must(date => date != default(DateTime).ToString())
                .WithMessage("Datetime unavaliable"); ;

            RuleFor(cm => cm.From.State)
                .Must(ValidateState).WithMessage("Invalid from state")
                .Length(2).WithMessage("Must be 2 characters.")
                .NotEmpty().WithMessage("From state cannot be empty");

            RuleFor(cm => cm.From.City)
                .NotEmpty().WithMessage("From city cannot be empty");

            RuleFor(cm => new { cm.From.State, cm.From.Zip })
                .Must(c => ValidateStateAndZip(acronym: c.State, zip: c.Zip)).WithMessage("Invalid from zip code");

            RuleFor(cm => cm.To.State)
                .Must(ValidateState).WithMessage("Invalid to state")
                .Length(2).WithMessage("Must be 2 characters")
                .NotEmpty().WithMessage("To state cannot be empty");

            RuleFor(cm => cm.To.City)
                .NotEmpty().WithMessage("To city cannot be empty");

            RuleFor(cm => new { cm.To.State, cm.To.Zip })
                .Must(c => ValidateStateAndZip(acronym: c.State, zip: c.Zip)).WithMessage("Invalid to zip code");
        }
    }
}
