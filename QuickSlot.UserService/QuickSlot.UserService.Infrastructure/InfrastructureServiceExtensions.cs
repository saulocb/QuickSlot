using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime.CredentialManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickSlot.UserService.Domain.Entities;
using QuickSlot.UserService.Domain.Interfaces;
using QuickSlot.UserService.Infrastructure.Factories;
using QuickSlot.UserService.Infrastructure.Interfaces;
using QuickSlot.UserService.Infrastructure.Repositories;

namespace QuickSlot.UserService.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var awsProfileName = configuration["AWSProfile"];
            CredentialProfileStoreChain chain = new CredentialProfileStoreChain();

            if (chain.TryGetAWSCredentials(awsProfileName, out var awsCredentials))
            {
                AmazonDynamoDBConfig ddbConfig = new AmazonDynamoDBConfig
                {
                    RegionEndpoint = RegionEndpoint.EUWest2,
                  //  ServiceURL = configuration["ServiceURL"]
                };

                services.AddSingleton<IAmazonDynamoDB>(new AmazonDynamoDBClient(awsCredentials, ddbConfig));
            }
            else
            {
                throw new Exception("Handle error - couldn't retrieve AWS credentials");
            }

            services.AddTransient<DynamoDbOperationConfigFactory>();


            // Register DynamoDB Context
            services.AddTransient<IDynamoDBContext, DynamoDBContext>();

            // Register BaseRepository
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            // Register the factory for creating DynamoDBOperationConfig
            services.AddSingleton<IDynamoDbOperationConfigFactory, DynamoDbOperationConfigFactory>();

            // Register UserRepository
            services.AddScoped<IUserRepository>(serviceProvider =>
            {
                var dynamoDbContext = serviceProvider.GetRequiredService<IDynamoDBContext>();
                var configFactory = serviceProvider.GetRequiredService<DynamoDbOperationConfigFactory>();
                return new UserRepository(dynamoDbContext, configFactory, configuration["DynamoDbTables:User"]);
            });

            return services;
        }
    }
}
