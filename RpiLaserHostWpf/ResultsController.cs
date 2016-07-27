using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;

namespace RpiLaserHostWpf
{
    public class ResultsController : ApiController
    {
        private readonly DataService dataService = new DataService();

        /// <summary>
        /// The GET method.
        /// </summary>
        /// <returns>
        /// Returns the top 10 results in serialized resultVm.
        /// </returns>
        public string Get()
        {
            var top10 = dataService.GetTop10Results();

            var top10ViewModel = new List<ResultVm>();

            foreach (var item in top10)
            {
                var itemMinutes = item.Time/60000;

                var itemSeconds = (item.Time/1000)%60;

                var itemMiliSeconds = item.Time%1000;

                top10ViewModel.Add(new ResultVm
                {
                    Name = item.Name,
                    Time = $"{itemMinutes.ToString("D2")}:{itemSeconds.ToString("D2")}:{itemMiliSeconds.ToString("D3")}"
                });
            }

            return JsonConvert.SerializeObject(top10ViewModel);
        }

        /// <summary>
        /// The POST method.
        /// </summary>
        /// <param name="result">
        /// The result instance.
        /// </param>
        public void Post([FromBody] Result result)
        {
            Console.WriteLine($"Received data: {JsonConvert.SerializeObject(result)}");
            if (!string.IsNullOrWhiteSpace(result.Name))
            {
                result.Crosses = dataService.GetHits();
                dataService.StoreResult(result);
                dataService.ClearHits();
            }
        }
    }
}
