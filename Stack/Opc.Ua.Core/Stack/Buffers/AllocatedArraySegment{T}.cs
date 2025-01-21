// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;

namespace Opc.Ua.Buffers
{
    /// <summary>
    /// Owner of <see cref="ArraySegment{T}"/> that is responsible
    /// for disposing the underlying memory appropriately.
    /// </summary>
    public sealed class AllocatedArraySegment<T> : IArraySegmentOwner<T>
    {
        private AllocatedArraySegment(ArraySegment<T> segment)
        {
            Segment = segment;
        }

        /// <inheritdoc/>
        public ArraySegment<T> Segment { get; private set; }

        /// <inheritdoc/>
        public T this[int i]
        {
            get => Segment[i];
            set => Segment.Array[i + Segment.Offset] = value;
        }

        /// <summary>
        /// Creates a payload that contains the buffer as
        /// in an <see cref="ArraySegment{T}"/>.
        /// </summary>
        /// <returns>
        /// An owner for an <see cref="ArraySegment{T}"/> that contains the buffer.
        /// Since the buffer is garbage collected, it is only invalidated on dispose.
        /// </returns>
        public static IArraySegmentOwner<T> Create(T[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            return new AllocatedArraySegment<T>(new ArraySegment<T>(buffer));
        }

        /// <summary>
        /// Creates a payload that contains the buffer as
        /// in an <see cref="ArraySegment{T}"/>.
        /// </summary>
        /// <returns>
        /// An owner for an <see cref="ArraySegment{T}"/> that contains the buffer.
        /// Since the buffer is garbage collected, it is only invalidated on dispose.
        /// </returns>
        public static IArraySegmentOwner<T> Create(ArraySegment<T> buffer)
        {
            return new AllocatedArraySegment<T>(buffer);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Segment = default;
        }

        /// <inheritdoc/>
        public bool TrySetSegment(int offset, int length)
        {
            var array = Segment.Array;
            if (array?.Length >= offset + length)
            {
                Segment = new ArraySegment<T>(array, offset, length);
                return true;
            }

            return false;
        }
    }
}

