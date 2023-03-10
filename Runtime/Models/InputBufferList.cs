using System.Collections.Generic;

namespace Nazio_LT.Tools.Core
{
    /// <summary>Contains multiple input buffers.</summary>
    public class InputBufferList
    {
        public InputBufferList(InputBufferList _base, InputBufferList _additionnal)
        {
            buffers = new List<InputBuffer>(NUtils.Merge<InputBuffer>(_additionnal.buffers.ToArray(), _base.buffers.ToArray()));
        }

        public InputBufferList(InputBufferList _base, InputBuffer[] _buffers)
        {
            buffers = new List<InputBuffer>(NUtils.Merge<InputBuffer>(_buffers, _base.buffers.ToArray()));
        }

        public InputBufferList(InputBuffer[] _buffers)
        {
            buffers = new List<InputBuffer>(_buffers);
        }

        public readonly List<InputBuffer> buffers;

        public void ExecuteAll()
        {
            foreach (var _buffer in buffers) _buffer.TryExecute();
        }
    }
}