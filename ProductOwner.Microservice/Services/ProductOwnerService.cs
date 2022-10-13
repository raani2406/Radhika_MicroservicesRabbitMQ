
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using ProductOwner.Microservice.Database;
using ProductOwner.Microservice.Models;
using ProductOwner.Microservice.Utility;
using RabbitMQ.Client;
using System.Linq.Expressions;
using System.Text;

namespace ProductOwner.Microservice.Services
{
    public class ProductOwnerService : IProductOwnerService
    {
        private readonly DbContextClass _dbContext;

        public ProductOwnerService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Products>> GetProductsListAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public bool SendProductOffer(ProductOfferDetail productOfferDetails)
        {
            var RabbitMQServerName = StaticConfigurationManager.AppSetting["RabbitMQ: RabbitURL"];
            var RabbitMQUserName = StaticConfigurationManager.AppSetting["RabbitMQ: UserName"];
            var RabbitMQPassword = StaticConfigurationManager.AppSetting["RabbitMQ: Password"];

            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = RabbitMQServerName,
                    UserName = RabbitMQUserName,
                    Password = RabbitMQPassword
                };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(
                        exchange: StaticConfigurationManager.AppSetting["RabbitMQ:ExchangeName"],
                       type: StaticConfigurationManager.AppSetting["RabbitMQ:ExchangeType"]);

                    channel.QueueDeclare(
                        queue: StaticConfigurationManager.AppSetting["RabbitMQ:QueueName"],
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    channel.QueueBind(
                        queue: StaticConfigurationManager.AppSetting["RabbitMQ:QueueName"],
                        exchange: StaticConfigurationManager.AppSetting["RabbitMQ:ExchangeName"],
                        routingKey: StaticConfigurationManager.AppSetting["RabbitMQ:RouteKey"]
                        );

                    var productdetail = JsonConvert.SerializeObject(productOfferDetails);
                    var body = Encoding.UTF8.GetBytes(productdetail);
                    var property = channel.CreateBasicProperties();
                    property.Persistent = true;

                    channel.BasicPublish(
                        exchange: StaticConfigurationManager.AppSetting["RabbitMQ:ExchangeName"],
                        routingKey: StaticConfigurationManager.AppSetting["RabbitMQ:RouteKey"],
                        basicProperties: property,
                        body: body
                        );
                    return true;
                        
                }
                
                
            }
            catch(Exception)
            {

            }
            return false;
        }
    }
}
