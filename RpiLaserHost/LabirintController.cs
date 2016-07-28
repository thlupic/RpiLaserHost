using System;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace RpiLaserHost
{
    public class LabirintController : ApiController
    {
        private readonly DataService dataService = new DataService();

        // POST api/values 
        /// <summary>
        /// Post method
        /// </summary>
        /// <param name="value">The GpioValues instance.</param>
        /// <returns>
        /// Returns the HTTP response message containing the ID which triggered the event, error message otherwise.
        /// </returns>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]GpioValues value)
        {
            Console.WriteLine();

            var content = new StringContent("");

            var statusCode = HttpStatusCode.BadRequest;

            var startPin = 14;

            var stopPin = 15;

            try
            {
                if (value.Id == startPin)
                {
                    content = new StringContent($"Received message from GPIO pin: {value.Id}");

                    statusCode = HttpStatusCode.OK;

                    Console.WriteLine($"Received message from start pin: {value.Id}");

                    if (!dataService.CheckHit(startPin))
                    {
                        dataService.HandleHit(startPin);

                        var sp = new SoundPlayer {SoundLocation = Environment.CurrentDirectory + "\\start.wav"};

                        sp.Play();

                        StopwatchHub.Send("reset");

                        StopwatchHub.Send("start");
                    }
                }
                else if (value.Id == stopPin)
                {
                    content = new StringContent($"Received message from GPIO pin: {value.Id}");

                    statusCode = HttpStatusCode.OK;

                    Console.WriteLine($"Received message from stop pin: {value.Id}");

                    if (dataService.CheckHit(startPin))
                    {
                        dataService.HandleHit(stopPin);

                        StopwatchHub.Send("stop");

                        var sp = new SoundPlayer {SoundLocation = Environment.CurrentDirectory + "\\stop.wav"};

                        sp.Play();
                    }
                }
                else if (value.Id < 40)
                {
                    content = new StringContent($"Received message from GPIO pin: {value.Id}");

                    statusCode = HttpStatusCode.OK;

                    Console.WriteLine($"Received message from GPIO pin: {value.Id}");

                    var isAlreadyHit = dataService.HandleHit(value.Id);

                    if (!isAlreadyHit)
                    {
                        StopwatchHub.Send("addTime");

                        var sp = new SoundPlayer {SoundLocation = Environment.CurrentDirectory + "\\linecross.wav"};

                        sp.Play();
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(value));

                content = new StringContent($"An error occured {e.Message}");

                statusCode = HttpStatusCode.BadRequest;
            }

            return new HttpResponseMessage
            {
                Content = content,
                StatusCode = statusCode
            };
        }
    }
}
