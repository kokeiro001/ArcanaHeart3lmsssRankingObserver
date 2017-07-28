using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArcanaHeart3lmsssRankingObserver.Core;
using CsvHelper;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;

namespace ArcanaHeart3lmsssRankingObserver.Functions
{
    public static class CsvBlob2TableFunction
    {
        public static string TableName = @"ah3lmsssRanking";

        [FunctionName("CsvBlob2TableFunction")]
        public static async Task Run([BlobTrigger("ah3lmsss-ranking/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            var now = DateTime.UtcNow;

            var table = await GetCloudTableAsync();

            try
            {

                using (var streamReader = new StreamReader(myBlob))
                using (var csvReader = new CsvReader(streamReader))
                {
                    // 遅延評価したくないのでToArrayする
                    csvReader.Configuration.RegisterClassMap<RankingCsvViewModelMap>();
                    var csvData = csvReader.GetRecords<RankingCsvViewModel>().ToArray();


                    foreach (var entity in csvData.Select(x => x.ToEntity()))
                    {
                        entity.PartitionKey = entity.card_id.ToString();
                        entity.RowKey = entity.start_date.ToString("yyyyMMdd-hhmmss") + "-" + entity.pcol1_name;

                        var op = TableOperation.InsertOrReplace(entity);
                        try
                        {
                            await table.ExecuteAsync(op);
                        }
                        catch (Exception e)
                        {
                            log.Error(e.Message + " " + e.StackTrace);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message + " " + e.StackTrace);
                throw;
            }
        }

        private static async Task<CloudTable> GetCloudTableAsync()
        {
            var storageAccount = CloudStorageAccountUtility.GetDefaultStorageAccount();
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(TableName);
            await table.CreateIfNotExistsAsync();
            return table;
        }
    }
}