using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using Assistant.Application.Interfaces.Authentication;
using Assistant.Domain.Configuration;
using Assistant.Domain.Configuration.Options;
using Microsoft.Extensions.Options;

namespace Assistant.Application.Services.Authentication
{
    public sealed class AesEncryption : IEncryption, IDisposable
    {
        sealed class AesPool : IDisposable
        {
            private readonly ConcurrentQueue<Aes> queue = new ConcurrentQueue<Aes>();

            private readonly AesEncryptionOptions configuration;

            private int isDisposed;

            public AesPool(AesEncryptionOptions configuration)
            {
                this.configuration = configuration;
            }

            public void Dispose()
            {
                if (0 == Interlocked.CompareExchange(ref isDisposed, 1, 0))
                {
                    while (queue.TryDequeue(out var instance))
                    {
                        instance.Dispose();
                    }
                }
            }

            public Aes Take()
            {
                if (0 != isDisposed)
                {
                    throw new ObjectDisposedException(nameof(AesPool));
                }
                if (queue.TryDequeue(out var instance))
                {
                    return instance;
                }
                instance = Aes.Create();
                instance.Key = configuration.Key;
                instance.IV = configuration.IV;
                return instance;
            }

            public void Return(Aes instance)
            {
                if (0 != isDisposed)
                {
                    instance.Dispose();
                }
                else
                {
                    queue.Enqueue(instance);
                }
            }
        }

        private readonly AesEncryptionOptions configuration;

        private readonly AesPool pool;

        private int isDisposed;

        public AesEncryption(IOptions<AuthenticationConfiguration> configurationOptions)
        {
            var configuration = configurationOptions.Value;
            this.configuration = new AesEncryptionOptions
            {
                IV = Convert.FromBase64String(configuration.Encryption.IVBase64),
                Key = Convert.FromBase64String(configuration.Encryption.KeyBase64)
            };
            pool = new AesPool(this.configuration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ThrowIfDisposed()
        {
            if (0 != isDisposed)
            {
                throw new ObjectDisposedException(nameof(AesEncryption));
            }
        }

        public byte[] Decrypt(byte[] chipherData)
        {
            ThrowIfDisposed();
            var aes = pool.Take();
            try
            {
                using(var decryptor = aes.CreateDecryptor())
                {
                    return decryptor.TransformFinalBlock(chipherData, 0, chipherData.Length);
                }
            }
            finally
            {
                pool.Return(aes);
            }
        }

        public void Dispose()
        {
            if (0 == Interlocked.CompareExchange(ref isDisposed, 1, 0))
            {
                pool.Dispose();
            }
        }

        public byte[] Encrypt(byte[] plainData)
        {
            ThrowIfDisposed();
            var aes = pool.Take();
            try
            {
                using(var encryptor = aes.CreateEncryptor())
                {
                    return encryptor.TransformFinalBlock(plainData, 0, plainData.Length);
                }
            }
            finally
            {
                pool.Return(aes);
            }
        }
    }
}
