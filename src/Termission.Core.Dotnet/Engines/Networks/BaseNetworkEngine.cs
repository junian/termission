using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Juniansoft.Termission.Core.Events;

namespace Juniansoft.Termission.Core.Engines.Networks
{
    public abstract class BaseNetworkEngine: INetworkEngine
    {
        protected AsyncOperation _operation;

        public event EventHandler<MessageResponseReceivedArgs> MessageResponseReceived;

        protected void OnMessageResponseReceived(byte[] response)
        {
            MessageResponseReceived?.Invoke(this, new MessageResponseReceivedArgs(response));
        }

        protected BaseNetworkEngine()
        {

        }

        public void Write(byte[] data)
        {
            Write(data, 0, data.Length);
        }

        public abstract void Write(byte[] data, int offset, int count);

        public abstract Stream BaseStream { get; protected set; }

        public abstract int ReadBufferSize { get; set; }

        public abstract bool IsOpen { get; protected set; }

        protected async Task<bool> StreamReadAsync()
        {
            try
            {
                var stream = BaseStream;
                var buffer = new byte[ReadBufferSize];
                var actualLength = await stream?.ReadAsync(buffer, 0, buffer.Length);
                if (actualLength <= 0)
                    return false;

                var received = new byte[actualLength];

                Buffer.BlockCopy(buffer, 0, received, 0, actualLength);

                HandleReceivedBytes(received);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        protected void HandleReceivedBytes(byte[] received)
        {
            _operation.Post(new SendOrPostCallback((_) =>
            {
                OnMessageResponseReceived(received);
            }), null);
        }

        protected abstract void EngineOpen();
        protected abstract Task EngineOpenAsync();

        public virtual void Open()
        {
            EngineOpen();
            if (IsOpen)
            {
                _operation = AsyncOperationManager.CreateOperation(null);
                Task.Run(async () => { while (await StreamReadAsync()) { } });
            }
        }

        public async Task OpenAsync()
        {
            await Task.Run(() => Open());
        }

        protected abstract void EngineClose();
        protected abstract Task EngineCloseAsync();

        public void Close()
        {
            _operation?.OperationCompleted();
            EngineClose();
        }

        public async Task CloseAsync()
        {
            await Task.Run(() => Close());
        }

        public abstract void Set(object model);
    }
}
