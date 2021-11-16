using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Assistant.Application.Services.Authentication
{
    public class Sha512Helper : IDisposable
    {
        sealed class Sha512Pool : IDisposable
        {
            readonly ConcurrentQueue<SHA512> _queue = new ConcurrentQueue<SHA512>();

            int _isDisposed;

            public void Dispose()
            {
                if (0 == Interlocked.CompareExchange(ref _isDisposed, 1, 0))
                {
                    while (_queue.TryDequeue(out var instance))
                    {
                        instance.Dispose();
                    }
                }
            }

            public SHA512 Take() => _queue.TryDequeue(out var instance) ? instance : SHA512.Create();

            public void Return(SHA512 instance)
            {
                if (0 == _isDisposed)
                {
                    _queue.Enqueue(instance);
                }
                else
                {
                    instance.Dispose();
                }
            }
        }

        static readonly UTF8Encoding _utf8 = new UTF8Encoding(false);

        readonly Sha512Pool _pool = new Sha512Pool();

        readonly char[] _chars;

        int _isDisposed;

        public Sha512Helper()
        {
            _chars = new char[512]; // (char*)Marshal.AllocHGlobal(Marshal.SizeOf<char>() * 512);
            for (var i = 0; i < 256; ++i)
            {
                var ch = i.ToString("X2");
                _chars[i * 2] = ch[0];
                _chars[i * 2 + 1] = ch[1];
            }
        }

        ~Sha512Helper() => Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (0 == Interlocked.CompareExchange(ref _isDisposed, 1, 0))
            {
                if (disposing)
                {
                    _pool.Dispose();
                }
                // Marshal.FreeHGlobal((IntPtr)_chars);
            }
        }

        public string ComputeHash(string input)
        {
            var bytes = _utf8.GetBytes(input);
            byte[] hash;
            var sha512 = _pool.Take();
            try
            {
                hash = sha512.TransformFinalBlock(bytes, 0, bytes.Length);
            }
            finally
            {
                _pool.Return(sha512);
            }
            {
                // var buffer = stackalloc char[hash.Length * 2];
                var buffer = new char[hash.Length * 2];
                for (var i = 0; i < hash.Length; ++i)
                {
                    buffer[i * 2] = _chars[hash[i] * 2];
                    buffer[i * 2 + 1] = _chars[hash[i] * 2 + 1];
                }
                return new string(buffer, 0, hash.Length * 2);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }
    }
}
