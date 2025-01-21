// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;

namespace Opc.Ua.Buffers
{
    /// <summary>
    /// An instance of an empty <see cref="ArraySegment{T}"/>.
    /// All instances are shared.
    /// </summary>
    public sealed class EmptyArraySegment<T> : IArraySegmentOwner<T>
    {
        private EmptyArraySegment(ArraySegment<T> segment)
        {
            Segment = segment;
        }

        /// <summary>
        /// Gets an instance of the empty <see cref="ArraySegment{T}"/>.
        /// </summary>
        public static EmptyArraySegment<T> Instance { get; } = new EmptyArraySegment<T>(ArraySegment<T>.Empty);

        /// <inheritdoc/>
        public ArraySegment<T> Segment { get; private set; }

        /// <inheritdoc/>
        public T this[int i]
        {
            get => throw new ArgumentOutOfRangeException();
            set => throw new ArgumentOutOfRangeException();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
        }

        /// <inheritdoc/>
        public bool TrySetSegment(int offset, int length)
        {
            return false;
        }
    }
}
