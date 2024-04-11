using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {

            if (!ValidateFullName(firstName, lastName))
            {
                return false;
            }

            if (!ValidateEmail(email))
            {
                return false;
            }

            if (!ValidateAge(dateOfBirth))
            {
                return false;
            }
            
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            int credits = CalculateCreditLimit(client.Type, lastName, dateOfBirth);
            if (credits < 500 && client.Type != "VeryImportantClient")
            {
                Console.WriteLine("Not enough credits");
                return false;
            }
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName,
                CreditLimit = credits
            };
            
            UserDataAccess.AddUser(user);
            return true;
        }
        
        private int CalculateCreditLimit(string clientType, string lastName, DateTime dateOfBirth)
        {
            int baseCreditLimit;
            using (var userCreditService = new UserCreditService())
            {
                baseCreditLimit = userCreditService.GetCreditLimit(lastName, dateOfBirth);
            }

            if (clientType != "VeryImportantClient")
            {
                baseCreditLimit *= 2;
            }

            return baseCreditLimit;
        }


        public Boolean ValidateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;

            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }
            {
                return true;
            }
        }
        
        private bool ValidateEmail(string email)
        {
            if (!email.Contains("@") || !email.Contains(".") ||  email.Length <= 2)
            {
                return false;
            }

            return true;
        }

        private bool ValidateFullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                return false;
            }

            return true;
        }

    }
}
