using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Threading.Tasks;

namespace SQS.Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("My AWS SQS Consumer!");

            try
            {
                var client = new AmazonSQSClient(RegionEndpoint.USEast2);
                var url = "https://sqs.us-east-2.amazonaws.com/713434528261/teste_sqs";

                var request = new ReceiveMessageRequest
                {
                    QueueUrl = url
                };

                while (true) 
                {
                    var response = await client.ReceiveMessageAsync(request);

                    foreach (var message in response.Messages)
                    {
                        await client.DeleteMessageAsync(url, message.ReceiptHandle);
                        Console.WriteLine($"Message received and removed from SQS: {message.Body}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);

                throw ex;
            }
        }
    }
}
