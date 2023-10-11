using System.Collections.Generic;

namespace Nazio_LT.Tools.Core
{
    /// <summary>Contains multiple input buffers.</summary>
    public class InputBufferList
    {
        public InputBufferList(InputBufferList other, InputBufferList additionnal)
        {
            buffers = new List<InputBuffer>(NUtils.Merge<InputBuffer>(additionnal.buffers.ToArray(), other.buffers.ToArray()));
        }

        public InputBufferList(InputBufferList other, InputBuffer[] buffers)
        {
            this.buffers = new List<InputBuffer>(NUtils.Merge<InputBuffer>(buffers, other.buffers.ToArray()));
        }

        public InputBufferList(InputBuffer[] buffers)
        {
            this.buffers = new List<InputBuffer>(buffers);
        }

        public readonly List<InputBuffer> buffers;

        public void ExecuteAll()
        {
            foreach (var buffer in buffers) buffer.TryExecute();
        }
    }
}