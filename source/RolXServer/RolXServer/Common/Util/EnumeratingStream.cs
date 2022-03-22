// -----------------------------------------------------------------------
// <copyright file="EnumeratingStream.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Common.Util;

/// <summary>
/// A lazy-evaluated stream.
/// </summary>
/// <typeparam name="T">The data-type of the source.</typeparam>
internal sealed class EnumeratingStream<T> : Stream
{
    private readonly Queue<byte> buffer = new Queue<byte>();

    private readonly IEnumerator<T> source;
    private readonly Func<T, IEnumerable<byte>> serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumeratingStream{T}" /> class.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="serializer">The serializer.</param>
    public EnumeratingStream(IEnumerable<T> source, Func<T, IEnumerable<byte>> serializer)
    {
        this.source = source.GetEnumerator();
        this.serializer = serializer;
    }

    /// <inheritdoc/>
    public override bool CanRead => true;

    /// <inheritdoc/>
    public override bool CanSeek => false;

    /// <inheritdoc/>
    public override bool CanWrite => false;

    /// <inheritdoc/>
    public override long Length => -1;

    /// <inheritdoc/>
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    /// <inheritdoc/>
    public override int Read(byte[] buffer, int offset, int count)
    {
        var read = 0;
        while (read < count)
        {
            var mayb = this.NextByte();
            if (mayb == null)
            {
                break;
            }

            buffer[offset + read] = (byte)mayb;
            read++;
        }

        return read;
    }

    /// <inheritdoc/>
    public override void Write(byte[] buffer, int offset, int count)
        => throw new NotSupportedException();

    /// <inheritdoc/>
    public override void Flush() => throw new NotSupportedException();

    /// <inheritdoc/>
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    /// <inheritdoc/>
    public override void SetLength(long value) => throw new NotSupportedException();

    private bool SerializeNext()
    {
        if (!this.source.MoveNext())
        {
            return false;
        }

        foreach (var b in this.serializer(this.source.Current))
        {
            this.buffer.Enqueue(b);
        }

        return true;
    }

    private byte? NextByte()
    {
        if (this.buffer.Any() || this.SerializeNext())
        {
            return this.buffer.Dequeue();
        }

        return null;
    }
}
