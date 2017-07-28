using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;

namespace ArcanaHeart3lmsssRankingObserver.Functions
{
    static class CloudStorageAccountUtility
    {
        public static CloudStorageAccount GetDefaultStorageAccount()
        {
            var connectionString = CloudConfigurationManager.GetSetting("AzureWebJobsStorage");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = @"UseDevelopmentStorage=true";
            }
            return CloudStorageAccount.Parse(connectionString);
        }
    }
}