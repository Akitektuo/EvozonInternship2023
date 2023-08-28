using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Users.Commands;
using MealPlan.Business.Users.Handlers;
using MealPlan.Data;
using MealPlan.Data.Models.Users;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    public class RegisterUserCommandHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private RegisterUserCommandHandler _handler;
        private RegisterUserCommand _successfulRequest;
        private RegisterUserCommand _failedRequest;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new RegisterUserCommandHandler(_context.Object);

            CreateCommands();
            SetupContext();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldRegisterUser()
        {
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var result = await _handler.Handle(_successfulRequest, new CancellationToken());

            result.Should().BeTrue();
        }

        [Test]
        public async Task WhenEmailAlreadyExists_ShouldThrowCustomApplicationException()
        {
            Func<Task> action = async () => await _handler.Handle(_failedRequest, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Registration failed",
                        Code = (int)ErrorCode.RegistrationEmailAlreadyUsed
                    })); 
        }

        [Test]
        public async Task WhenEmailDoesntExist_ShouldCallSaveChanges()
        {
            _context.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();

            var result = await _handler.Handle(_successfulRequest, new CancellationToken());

            _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenEmailAlreadyExists_ShouldNotCallSaveChanges()
        {
            _context.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();

            try
            {
                var result = await _handler.Handle(_failedRequest, new CancellationToken());
            }
            catch(CustomApplicationException)
            {
                _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            }            
        }

        private void SetupContext()
        {
            var users = new List<User>
            {
                new User 
                { 
                    Id = 1, 
                    FirstName = "Refuz", 
                    LastName = "NuVreau", 
                    Email = "refuznuvreau@gmail.com", 
                    Password = "password", 
                    RoleId = Role.User 
                }, 

                new User 
                { 
                    Id = 1, 
                    FirstName = "Dionisie", 
                    LastName = "Amuzant", 
                    Email = "dionisieamuzant@yahoo.com", 
                    Password = "password(amuzant)", 
                    RoleId = Role.User 
                },

                new User
                { 
                    Id = 1, 
                    FirstName = "Ragnar", 
                    LastName = "Catel", 
                    Email = "ragnargoodboy@dogmail.com", 
                    Password = "password", 
                    RoleId = Role.User 
                }
            };

            _context.Setup(c => c.Users).ReturnsDbSet(users);
        }

        private void CreateCommands()
        {
            _successfulRequest = new RegisterUserCommand 
            { 
                FirstName = "TestFirstName", 
                LastName = "TestLastName", 
                Email = "newemail@mail.com", 
                Password = "password" 
            };

            _failedRequest = new RegisterUserCommand 
            { 
                FirstName = "TestFirstName", 
                LastName = "TestLastName", 
                Email = "refuznuvreau@gmail.com", 
                Password = "password" 
            };
        }
    }
}