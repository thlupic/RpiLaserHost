using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace RpiLaserHostWpf
{
    /// <summary>
    /// The data access class.
    /// </summary>
    public class DataService
    {
        private const string FileName = "data.txt";

        private const string HitsFileName = "hits.txt";

        private readonly string path = $"{Environment.CurrentDirectory}\\{FileName}";

        private readonly string hitsPath = $"{Environment.CurrentDirectory}\\{HitsFileName}";

        public IEnumerable<Result> GetTop10Results()
        {
            return GetAllResults().Take(10);
        }

        public void StoreResult(Result result)
        {
            var allResults = GetAllResults().ToList();

            allResults.Add(result);

            StoreResults(allResults);
        }

        private IEnumerable<Result> GetAllResults()
        {
            try
            {
                var text = System.IO.File.ReadAllText(path);

                return JsonConvert.DeserializeObject<List<Result>>(text);
            }
            catch
            {
                return new List<Result>();
            }
        }

        private void StoreResults(IEnumerable<Result> results)
        {
            try
            {
                var resultsSorted = results.OrderBy(r => r.Time);

                var resultsJson = JsonConvert.SerializeObject(resultsSorted);

                System.IO.File.WriteAllText(path, resultsJson);
            }
            catch(Exception e)
            {
            }
        }

        public bool HandleHit(int hitId)
        {
            var currentHits = GetHits().ToList();

            if (currentHits.Contains(hitId)) return true;

            currentHits.Add(hitId);

            StoreHits(currentHits);

            return false;
        }

        public void ClearHits()
        {
            StoreHits(new List<int>());
        }

        public IEnumerable<int> GetHits()
        {
            try
            {
                var text = System.IO.File.ReadAllText(hitsPath);

                return JsonConvert.DeserializeObject<List<int>>(text);
            }
            catch (Exception)
            {
                return new List<int>();
            }
            
        }

        private void StoreHits(IEnumerable<int> hitIds)
        {
            try
            {
                System.IO.File.WriteAllText(hitsPath, JsonConvert.SerializeObject(hitIds));
            }
            catch (Exception e)
            {
            }
        }
    }
}
