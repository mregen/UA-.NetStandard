// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;

namespace Opc.Ua.Buffers
{
    /// <summary>
    /// Owner of an <see cref="ArraySegment{T}"/> that is responsible
    /// for disposing the underlying memory appropriately.
    /// </summary>
    public interface IArraySegmentOwner<T> : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="ArraySegment{T}"/>.
        /// </summary>
        ArraySegment<T> Segment { get; }

        /// <summary>
        /// Get or sets an entry in the indexed payload.
        /// </summary>
        /// <param name="i">Index of the payload entry.</param>
        T this[int i] { get; set; }

        /// <summary>
        /// Sets the offset and the length of the <see cref="ArraySegment{T}"/>
        /// within the boundaries of the underlying memory.
        /// </summary>
        /// <param name="offset">The new offset in the ArraySegment.</param>
        /// <param name="length">The new length of the ArraySegment.</param>
        /// <returns>
        /// True if the ArraySegment update was successful given the underlying memory,
        /// False otherwise.
        /// </returns>
        bool TrySetSegment(int offset, int length);
    }
}
