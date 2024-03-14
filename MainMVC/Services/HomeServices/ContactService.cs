using Data.Context;
using Data.Entity.EntityMVC;
using MainMVC.ViewModels.HomeViewModel;

namespace MainMVC.Services.HomeServices
{
    public class ContactService
    {
        private readonly AppDbContext _dbContext;

        public ContactService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveContactMessage(ContactViewModel model)
        {
            var contactMessage = new ContactMessage
            {
                UserId = model.UserId,
                UserName = model.UserName,
                UserEmail = model.UserEmail,
                ProductId = model.ProductId,
                ProductName = model.ProductName,
                Message = model.Message
            };

            _dbContext.contactMessages.Add(contactMessage);
            _dbContext.SaveChanges();
        }
    }
}
