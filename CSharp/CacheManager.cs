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
        private const int CacheVersion = 2;
        private Dictionary<DateTime, List<ApiData>> _cache;

        public CacheManager()
        {
            LoadCache();
        }


        private void LoadCache()
        {
            _cache = new Dictionary<DateTime, List<ApiData>>();

            if (!File.Exists(CacheFileName)) 
                return;
            
            try
            {
                using (var fileStream = new FileStream(CacheFileName, FileMode.Open))
                using (var decompressionStream = new GZipStream(fileStream, CompressionMode.Decompress))
                using (var reader = new BinaryReader(decompressionStream))
                {
                    int version = reader.ReadInt32();
                    if (version != CacheVersion)
                    {
                        Console.WriteLine($"Nieobsługiwana wersja cache: {version}. Oczekiwana wersja: {CacheVersion}.");
                        return;
                    }

                    int daysCount = reader.ReadInt32();
                    for (int i = 0; i < daysCount; i++)
                    {
                        DateTime date = DateTime.FromBinary(reader.ReadInt64());
                        int dataCount = reader.ReadInt32();
                        var dataList = new List<ApiData>();

                        for (int j = 0; j < dataCount; j++)
                        {
                            dataList.Add(new ApiData
                            {
                                UdtCzas = DateTime.FromBinary(reader.ReadInt64()),
                                Znacznik = reader.ReadInt32(),
                                BusinessDate = DateTime.FromBinary(reader.ReadInt64()),
                                SourceDatetime = DateTime.FromBinary(reader.ReadInt64())
                            });
                        }

                        _cache[date] = dataList;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas wczytywania cache: {ex.Message}");
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
                    using (var writer = new BinaryWriter(compressionStream))
                    {
                        writer.Write(CacheVersion);

                        writer.Write(_cache.Count);
                        foreach (var kvp in _cache)
                        {
                            writer.Write(kvp.Key.ToBinary());
                            writer.Write(kvp.Value.Count);

                            foreach (var data in kvp.Value)
                            {
                                writer.Write(data.UdtCzas.ToBinary());
                                writer.Write(data.Znacznik);
                                writer.Write(data.BusinessDate.ToBinary());
                                writer.Write(data.SourceDatetime.ToBinary());
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu cache: {ex.Message}");
            }
        }

        internal List<ApiData> GetFromCache(DateTime date)
        {
            return _cache.ContainsKey(date) ? _cache[date] : null;
        }

        internal void SaveToCache(DateTime date, List<ApiData> data)
        {
            _cache[date] = data;

            _ = SaveCacheAsync();
        }
    }
}
