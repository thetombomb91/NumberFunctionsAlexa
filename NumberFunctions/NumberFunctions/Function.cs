using Amazon.Lambda.Core;
using Slight.Alexa.Framework.Models.Requests;
using Slight.Alexa.Framework.Models.Responses;
using System;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace NumberFunctions
{
    public class Function
    {
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            IOutputSpeech innerResponse = null;
            var log = context.Logger;

            if (input.GetRequestType() == typeof(Slight.Alexa.Framework.Models.Requests.RequestTypes.ILaunchRequest))
            {
                // default launch request, let's just let them know what you can do
                log.LogLine($"Default LaunchRequest made");

                innerResponse = new PlainTextOutputSpeech();
                ((PlainTextOutputSpeech) innerResponse).Text = "Welcome to number functions.  You can ask us to add numbers!";
            }
            else if (input.GetRequestType() == typeof(Slight.Alexa.Framework.Models.Requests.RequestTypes.IIntentRequest))
            {
                var firstNumber = Convert.ToDouble(input.Request.Intent.Slots["firstnum"].Value);
                var secondNumber = Convert.ToDouble(input.Request.Intent.Slots["secondnum"].Value);
                if (input.Request.Intent.Name == "AddIntent")
                {
                    double result = firstNumber + secondNumber;

                    innerResponse = new PlainTextOutputSpeech();
                    ((PlainTextOutputSpeech) innerResponse).Text = $"The result is {result}.";
                }
                else if (input.Request.Intent.Name == "SubtractIntent")
                {
                    double result = firstNumber - secondNumber;

                    innerResponse = new PlainTextOutputSpeech();
                    ((PlainTextOutputSpeech) innerResponse).Text = $"The result is {result}.";
                }
                else if (input.Request.Intent.Name == "MultiplyIntent")
                {
                    double result = firstNumber * secondNumber;

                    innerResponse = new PlainTextOutputSpeech();
                    ((PlainTextOutputSpeech)innerResponse).Text = $"The result is {result}.";
                }
                else if (input.Request.Intent.Name == "DivideIntent")
                {
                    double result = firstNumber / secondNumber;

                    innerResponse = new PlainTextOutputSpeech();
                    ((PlainTextOutputSpeech)innerResponse).Text = $"The result is {result}.";
                }
            }

            var response = new Response
            {
                ShouldEndSession = true,
                OutputSpeech = innerResponse
            };
            var skillResponse = new SkillResponse
            {
                Response = response,
                Version = "1.0"
            };

            return skillResponse;
        }
    }
}