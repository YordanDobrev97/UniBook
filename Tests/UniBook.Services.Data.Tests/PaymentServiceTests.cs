namespace UniBook.Services.Data.Tests
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Stripe;
    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Web.ViewModels.Payments;
    using Xunit;

    public class PaymentServiceTests
    {
        [Fact]
        public void TestPaymentWithExistPayment()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDbPayment").Options;
            var dbContext = new ApplicationDbContext(options);

            var book = new Book
            {
                Name = "payment book",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var user = new ApplicationUser
            {
                UserName = "paymnet user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            dbContext.Books.Add(book);
            dbContext.Users.Add(user);
            dbContext.Payments.Add(new Payment
            {
                User = user,
                Book = book,
            });

            dbContext.SaveChanges();

            var paymentService = new PaymentService(dbContext);

            var result = paymentService.Pay(new PaymentInputModel
            {
                BookId = book.Id,
                UserId = user.Id,
            }).GetAwaiter().GetResult();

            Assert.False(result);
        }

        [Fact]
        public void TestPaymentCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UniBookDbPayment").Options;
            var dbContext = new ApplicationDbContext(options);

            var book = new Book
            {
                Name = "payment book",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var user = new ApplicationUser
            {
                UserName = "paymnet user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            dbContext.Books.Add(book);
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            StripeConfiguration.ApiKey = "sk_test_51HnNdgKSDI93DO7o7RDrTsvE5WVaAy9Ny15FRf68e0N9dR4QkBMoavuEqPzymieF1Wu4YRtyHUt0b9v3bRYx6g4k00hWkdKuXB";

            var paymnetService = new PaymentService(dbContext);
            var result = paymnetService.Pay(new PaymentInputModel
            {
                BookId = book.Id,
                UserId = user.Id,
                Price = 20,
                BookName = "payment book",
                CustomerName = "paymnet customer",
            }).GetAwaiter().GetResult();

            Assert.True(result);
        }
    }
}
