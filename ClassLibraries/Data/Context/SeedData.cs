using Bogus;
using Data.Entity;


namespace Data.Context
{
    public class SeedData
    {
        private static List<User> _users;
        public static List<User> GetUser(int number)
        {
            if (_users == null)
            {
                _users = new Faker<User>()
                    .RuleFor(u => u.Id, f => f.IndexFaker)
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.LastName, f => f.Name.LastName())
                    .Generate(number);
            }
            return _users;

        }
    }
}
