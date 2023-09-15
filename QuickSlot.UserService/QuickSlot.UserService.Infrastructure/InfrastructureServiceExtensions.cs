using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime.CredentialManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickSlot.UserService.Domain.Interfaces;
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
                };

                services.AddSingleton<IAmazonDynamoDB>(new AmazonDynamoDBClient(awsCredentials, ddbConfig));
            }
            else
            {
                throw new Exception("Handle error - couldn't retrieve AWS credentials");
            }

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
