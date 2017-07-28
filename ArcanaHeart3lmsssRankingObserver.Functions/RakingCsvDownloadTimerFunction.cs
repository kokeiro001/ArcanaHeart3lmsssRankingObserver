using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ArcanaHeart3lmsssRankingObserver.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ArcanaHeart3lmsssRankingObserver.Functions
{
    public static class RakingCsvDownloadTimerFunction
    {
        static readonly string BlobContainerName = @"ah3lmsss-ranking";

        // CSVを公式ホームページからダウンロードする

        // 日本標準時の AM 3:00に起動するように設定。
        [FunctionName("RankingCsvDownloadTimerFunction")]
        public static async Task Run([TimerTrigger("0 0 18 * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            var client = new ArcanaHeart3lmsssRankingClient();

            var blobContainer = await GetBlobContainerAsync();

            var now = DateTime.UtcNow;

            foreach (CharacterId characterId in Enum.GetValues(typeof(CharacterId)))
            {
                var fileName = $"{characterId.ToString().ToLower()}_{now.ToString("yyyyMMdd-hhmm")}.csv";

                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);

                using (var stream = await client.DownloadCsvAsync(characterId))
                {
                    await blockBlob.UploadFromStreamAsync(stream);
                }
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        private static async Task<CloudBlobContainer> GetBlobContainerAsync()
        {
            var storageAccount = CloudStorageAccountUtility.GetDefaultStorageAccount();
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(BlobContainerName);

            try
            {
                await container.CreateIfNotExistsAsync();
            }
            catch (StorageException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }

            return container;
        }
    }
}