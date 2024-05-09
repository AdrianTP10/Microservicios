using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentMicroservice.ApplicationServices;
using PaymentMicroservice.Core.Payments;
using PaymentMicroservice.DataAccess;
using PaymentMicroservice.Payments.Dto;
using UserMicroservice.UnitTest;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        protected TestServer _server;
        [OneTimeSetUp]
        public void Setup()
        {
            this._server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        }

        [Test]
        public async Task InsertPaymentTest()
        {
            var _paymentsAppService = _server.Host.Services.GetService<IPaymentsAppService>();
            var _context = _server.Host.Services.GetService<PaymentMicroserviceContext>();



            var newPaymentDto = new PaymentDto
            {
                Id = 3,
                OrderId = 1,
                Amount = 1,
            };

            var createdPayment = await _paymentsAppService.AddPaymentAsync(newPaymentDto);

            // Verifying if the payment is saved in memory.
            var retrievedPayment = _context.Payments.FirstOrDefault(u => u.Id == newPaymentDto.Id);
            Assert.IsNotNull(retrievedPayment);
        }

        [Test]
        public async Task GetAllPayments_Test()
        {
            var _paymentsAppService = _server.Host.Services.GetService<IPaymentsAppService>();
            var _context = _server.Host.Services.GetService<PaymentMicroserviceContext>();

            // Adding some payments to the db
            var payments = new List<Payment>
            {
                new Payment {
                     OrderId = 2,
                     Amount = 3,
                   
                },
                new Payment {
                     OrderId = 2,
                     Amount = 5,

                },
                new Payment {
                     OrderId = 3,
                     Amount = 2,

                },
                new Payment {
                     OrderId = 1,
                     Amount = 6,

                },
            };
            _context.Payments.AddRange(payments);
            _context.SaveChanges();


            var paymentsList = await _paymentsAppService.GetPaymentsAsync();
            Assert.That(payments.Count, Is.EqualTo(paymentsList.Count));
        }

        [Test]
        public async Task GetPaymentById_Test()
        {
            var _paymentsAppService = _server.Host.Services.GetService<IPaymentsAppService>();
            var _context = _server.Host.Services.GetService<PaymentMicroserviceContext>();

            // Adding some payments to the db
            var payments = new List<Payment>
            {
                new Payment {
                    Id = 1,
                     OrderId = 2,
                     Amount = 3,

                },
                new Payment {
                    Id=2,
                     OrderId = 2,
                     Amount = 5,

                },
                new Payment {
                    Id=3,
                     OrderId = 3,
                     Amount = 2,

                },
                new Payment {
                    Id=4,
                     OrderId = 1,
                     Amount = 6,

                },
            };
            _context.Payments.AddRange(payments);
            _context.SaveChanges();



            var payment = await _paymentsAppService.GetPaymentByIdAsync(3);

            Assert.IsNotNull(payment);
            Assert.That(payment.Id, Is.EqualTo(3));
        }

        [Test]
        public async Task EditPayment_Test()
        {
            var _paymentsAppService = _server.Host.Services.GetService<IPaymentsAppService>();
            var _context = _server.Host.Services.GetService<PaymentMicroserviceContext>();
            var _mapper = _server.Host.Services.GetService<IMapper>();

            // Adding some payments to the db
            var payments = new List<Payment>
            {
                new Payment {
                    Id = 1,
                     OrderId = 2,
                     Amount = 3,

                },
                new Payment {
                    Id=2,
                     OrderId = 2,
                     Amount = 5,

                },
                new Payment {
                    Id=3,
                     OrderId = 3,
                     Amount = 2,

                }
            };

            //Adding payment to be modified
            var payment4 = new Payment
            {
                Id = 4,
                OrderId = 1,
                Amount = 6,

            };

            await _context.Payments.AddRangeAsync(payments);
            await _context.Payments.AddAsync(payment4);
            await _context.SaveChangesAsync();

            // Detaching the entity context tracking
            _context.Entry<Payment>(payment4).State = EntityState.Detached;

            //Editing the payment
            payment4.OrderId = 4;
            payment4.Amount = 2;
            await _paymentsAppService.EditPaymentAsync(_mapper.Map<PaymentDto>(payment4));


            // Searching for the Payment
            var updatedPayment = await _context.Payments.FindAsync(4);
            Assert.That(updatedPayment.OrderId, Is.EqualTo(4));
            Assert.That(updatedPayment.Amount, Is.EqualTo(2));

        }

        [Test]
        public void DeletePayment_Test()
        {
            var _paymentsAppService = _server.Host.Services.GetService<IPaymentsAppService>();
            var _context = _server.Host.Services.GetService<PaymentMicroserviceContext>();

            // Adding some payments to the db
            var payments = new List<Payment>
            {
                new Payment {
                    Id = 1,
                     OrderId = 2,
                     Amount = 3,

                },
                new Payment {
                    Id=2,
                     OrderId = 2,
                     Amount = 5,

                },
                new Payment {
                    Id=3,
                     OrderId = 3,
                     Amount = 2,

                },
                new Payment {
                    Id=4,
                     OrderId = 1,
                     Amount = 6,

                },
            };
            _context.Payments.AddRange(payments);
            _context.SaveChanges();



            _paymentsAppService.DeletePaymentAsync(3);
            var nullPayment = _context.Payments.FirstOrDefault(u => u.Id == 3);
            Assert.IsNull(nullPayment);
        }
    }
}