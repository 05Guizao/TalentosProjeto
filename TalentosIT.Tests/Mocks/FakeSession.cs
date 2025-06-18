using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace TalentosIT.Tests.Mocks
{
    public class FakeSession : ISession
    {
        private readonly Dictionary<string, byte[]> _sessionStorage = new();

        public bool IsAvailable => true;
        public string Id => "FakeSessionId";
        public IEnumerable<string> Keys => _sessionStorage.Keys;

        public void Clear() => _sessionStorage.Clear();

        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public void Remove(string key) => _sessionStorage.Remove(key);

        public void Set(string key, byte[] value) => _sessionStorage[key] = value;

        public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);

        public void SetInt32(string key, int value)
        {
            _sessionStorage[key] = BitConverter.GetBytes(System.Net.IPAddress.HostToNetworkOrder(value));
        }

        public int? GetInt32(string key)
        {
            if (TryGetValue(key, out var data))
                return System.Net.IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 0));
            return null;
        }


        public void SetString(string key, string value) => Set(key, Encoding.UTF8.GetBytes(value));

        public string? GetString(string key)
        {
            return TryGetValue(key, out var data) ? Encoding.UTF8.GetString(data) : null;
        }
    }
}