// See https://aka.ms/new-console-template for more information
using System;
using Azure;
using Azure.AI.Inference;


var endpoint = new Uri("https://models.github.ai/inference");
var credential = new AzureKeyCredential(System.Environment.GetEnvironmentVariable("GITHUB_TOKEN"));
var model = "deepseek/DeepSeek-V3-0324";

var client = new ChatCompletionsClient(
    endpoint,
    credential,
    new AzureAIInferenceClientOptions());

var requestOptions = new ChatCompletionsOptions()
{
    Messages =
    {
        new ChatRequestSystemMessage("You are a helpful assistant."),
        new ChatRequestUserMessage("What is the capital of France?"),
    },
    Temperature = 1.0f,
    NucleusSamplingFactor = 1.0f,
    MaxTokens = 1000,
    Model = model
};

Response<ChatCompletions> response = client.Complete(requestOptions);
Console.WriteLine(response.Value.Content);

while (true)
{
    Console.Write("\nAsk a question (or type 'exit' to quit): ");
    var userInput = Console.ReadLine();
    if (string.Equals(userInput, "exit", StringComparison.OrdinalIgnoreCase))
        break;

    var chatOptions = new ChatCompletionsOptions()
    {
        Messages =
        {
            new ChatRequestSystemMessage("You are a helpful assistant."),
            new ChatRequestUserMessage(userInput),
        },
        Temperature = 1.0f,
        NucleusSamplingFactor = 1.0f,
        MaxTokens = 1000,
        Model = model
    };

    var chatResponse = client.Complete(chatOptions);
    Console.WriteLine(chatResponse.Value.Content);
}


Console.WriteLine("Hello, World!");
