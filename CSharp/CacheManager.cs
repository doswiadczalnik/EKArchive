using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace EKArchive
{
    public class CacheManager
    {
        private const string CacheFileName = "cache.dat";
        private Dictionary<DateTime, List<ApiData>> _cache;

        public CacheManager()
        {
            LoadCache();
        }

        private void LoadCache()
        {
            if (File.Exists(CacheFileName))
            {
                try
                {
                    using (var fileStream = new FileStream(CacheFileName, FileMode.Open))
                    using (var decompressionStream = new GZipStream(fileStream, CompressionMode.Decompress))
                    using (var reader = new StreamReader(decompressionStream))
                    {
                        var json = reader.ReadToEnd();
                        _cache = JsonConvert.DeserializeObject<Dictionary<DateTime, List<ApiData>>>(json);
                    }
                }
                catch
                {
                    _cache = new Dictionary<DateTime, List<ApiData>>();
                }
            }
            else
            {
                _cache = new Dictionary<DateTime, List<ApiData>>();
            }
        }

        private async Task SaveCacheAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var fileStream = new FileStream(CacheFileName, FileMode.Create))
                    using (var compressionStream = new GZipStream(fileStream, CompressionMode.Compress))
                    using (var writer = new StreamWriter(compressionStream))
                    {
                        var json = JsonConvert.SerializeObject(_cache, Formatting.Indented);
                        writer.Write(json);
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu cache: {ex.Message}");
            }
        }

        public List<ApiData> GetFromCache(DateTime date)
        {
            if (_cache.ContainsKey(date))
            {
                return _cache[date];
            }

            return null;
        }

        public void SaveToCache(DateTime date, List<ApiData> data)
        {
            _cache[date] = data;

            _ = SaveCacheAsync();
        }
    }
}
