using Amazon.Lambda.Core;
using Slight.Alexa.Framework.Models.Requests;
using Slight.Alexa.Framework.Models.Responses;
using System;
using System.Diagnostics;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace NumberFunctions
{
    public class Function
    {
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            IOutputSpeech innerResponse = null;
            var result = 0;

            if (input.GetRequestType() == typeof(Slight.Alexa.Framework.Models.Requests.RequestTypes.ILaunchRequest))
            {
                innerResponse = new PlainTextOutputSpeech();
                ((PlainTextOutputSpeech)innerResponse).Text = "Welcome to number functions.  You can ask us to add numbers!";
            }
            else if (input.GetRequestType() == typeof(Slight.Alexa.Framework.Models.Requests.RequestTypes.IIntentRequest))
            {
                Debug.WriteLine(input.Request.Intent.Name);

                var firstNumber = Convert.ToDouble(input.Request.Intent.Slots["firstnum"].Value);
                var secondNumber = Convert.ToDouble(input.Request.Intent.Slots["secondnum"].Value);
                switch (input.Request.Intent.Name)
                {
                    case "AddIntent":
                        result = (int) (firstNumber + secondNumber);
                        break;
                    case "SubtractIntent":
                        result = (int) (firstNumber - secondNumber);
                        break;
                    case "MultiplyIntent":
                        result = (int) (firstNumber * secondNumber);
                        break;
                    case "DivideIntent":
                        result = (int) (firstNumber / secondNumber);
                        break;
                    default:
                        result = 42;
                        break;
                }
            }

            innerResponse = new PlainTextOutputSpeech();
            ((PlainTextOutputSpeech)innerResponse).Text = $"The result is {result}.";

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