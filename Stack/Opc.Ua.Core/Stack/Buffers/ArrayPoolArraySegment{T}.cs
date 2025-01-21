// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;
using System.Buffers;

namespace Opc.Ua.Buffers
{

    /// <summary>
    /// Owner of <see cref="ArraySegment{T}"/> that is responsible for returning
    /// the underlying memory to <see cref="ArrayPool{T}"/> on dispose.
    /// </summary>
    /// <remarks>
    /// Implemented as a class to ensure that memory can not be disposed multiple times.
    /// </remarks>
    public sealed class ArrayPoolArraySegment<T> : IArraySegmentOwner<T>
    {
#if DEBUG
        // for testing clear buffers when returned
        private const bool ClearArray = true;
#else
        private const bool ClearArray = false;
#endif

        private ArrayPoolArraySegment(ArraySegment<T> segment)
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
        /// Retrieves a buffer in a <see cref="ArraySegment{T}"/>
        /// from <see cref="ArrayPool{T}"/> that has at least
        /// the requested length.
        /// </summary>
        /// <param name="minimumLength">The minimum length of the array needed.</param>
        /// <returns>
        /// An owner for an <see cref="ArraySegment{T}"/> that is at least <paramref name="minimumLength"/> in length.
        /// </returns>
        public static IArraySegmentOwner<T> Rent(int minimumLength)
        {
            if (minimumLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumLength), "Minimum length must be greater or equal than zero.");
            }
            if (minimumLength == 0)
            {
                return EmptyArraySegment<T>.Instance;
            }
            var segment = new ArraySegment<T>(ArrayPool<T>.Shared.Rent(minimumLength), 0, minimumLength);
            return new ArrayPoolArraySegment<T>(segment);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            var array = Segment.Array;
            if (array != null)
            {
                ArrayPool<T>.Shared.Return(array, ClearArray);
                Segment = default;
            }
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
