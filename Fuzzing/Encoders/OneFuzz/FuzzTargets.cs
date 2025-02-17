// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

#nullable enable

namespace OneFuzz.OpcUa.Encoders;

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Opc.Ua;
using Opc.Ua.Bindings;
using Opc.Ua.Security.Certificates;

/// <summary>
/// The OneFuzz fuzzing targets for the OPC UA SDK.
/// </summary>
public static class FuzzTargets
{
    private static ServiceMessageContext _messageContext = ServiceMessageContext.GlobalContext;

    /// <summary>
    /// The fuzz target for the binary decoder.
    /// </summary>
    public static void FuzzBinaryDecoder(ReadOnlySpan<byte> input)
    {
        using ArraySegmentStream stream = PrepareArraySegmentStream(input);
        _ = FuzzableCode.FuzzBinaryDecoderCore(stream);
    }

    /// <summary>
    /// The fuzz target for the binary encoder.
    /// </summary>
    public static void FuzzBinaryEncoder(ReadOnlySpan<byte> input)
    {
        IEncodeable? encodeable;
        try
        {
            using ArraySegmentStream stream = PrepareArraySegmentStream(input);
            encodeable = FuzzableCode.FuzzBinaryDecoderCore(stream, true);
        }
        catch
        {
            return;
        }

        // encode the fuzzed object and see if it crashes
        if (encodeable != null)
        {
            _ = BinaryEncoder.EncodeMessage(encodeable, _messageContext);
        }
    }

    /// <summary>
    /// The fuzz target for the indempotent binary encoder/decoder.
    /// </summary>
    public static void FuzzBinaryEncoderIndempotent(ReadOnlySpan<byte> input)
    {
        IEncodeable? encodeable;
        byte[] serialized;
        try
        {
            using ArraySegmentStream stream = PrepareArraySegmentStream(input);
            encodeable = FuzzableCode.FuzzBinaryDecoderCore(stream, true);
            serialized = BinaryEncoder.EncodeMessage(encodeable, _messageContext);
        }
        catch
        {
            return;
        }

        FuzzableCode.FuzzBinaryEncoderIndempotentCore(serialized, encodeable);
    }

    /// <summary>
    /// The fuzz target for the Json decoder.
    /// </summary>
    public static void FuzzJsonDecoder(ReadOnlySpan<byte> input)
    {
        _ = FuzzJsonDecoderCore(input);
    }

    /// <summary>
    /// The fuzz target for the Json encoder. Reversible encoding.
    /// </summary>
    public static void FuzzJsonEncoder(ReadOnlySpan<byte> input)
    {
        FuzzableCode.FuzzJsonEncoderCore(input, JsonEncodingType.Reversible);
    }

    /// <summary>
    /// The fuzz target for the Json encoder. Non Reversible encoding.
    /// </summary>
    public static void FuzzJsonEncoderNonReversible(ReadOnlySpan<byte> input)
    {
        FuzzableCode.FuzzJsonEncoderCore(input, JsonEncodingType.NonReversible);
    }

    /// <summary>
    /// The fuzz target for the Json encoder. Compact encoding.
    /// </summary>
    public static void FuzzJsonEncoderCompact(ReadOnlySpan<byte> input)
    {
        FuzzableCode.FuzzJsonEncoderCore(input, JsonEncodingType.Compact);
    }

    /// <summary>
    /// The fuzz target for the Json encoder. Verbose encoding.
    /// </summary>
    public static void FuzzJsonEncoderVerbose(ReadOnlySpan<byte> input)
    {
        FuzzableCode.FuzzJsonEncoderCore(input, JsonEncodingType.Verbose);
    }

    /// <summary>
    /// The Xml decoder fuzz target for libfuzzer.
    /// </summary>
    public static void FuzzXmlDecoder(ReadOnlySpan<byte> input)
    {
        using var memoryStream = new MemoryStream(input.ToArray());
        _ = FuzzableCode.FuzzXmlDecoderCore(memoryStream);
    }

    /// <summary>
    /// The Xml encoder fuzz target for libfuzzer.
    /// </summary>
    public static void FuzzXmlEncoder(ReadOnlySpan<byte> input)
    {
        IEncodeable? encodeable;
        try
        {
            using var memoryStream = new MemoryStream(input.ToArray());
            encodeable = FuzzableCode.FuzzXmlDecoderCore(memoryStream);
        }
        catch
        {
            return;
        }

        // encode the fuzzed object and see if it crashes
        if (encodeable != null)
        {
            using var encoder = new XmlEncoder(_messageContext);
            encoder.EncodeMessage(encodeable);
            encoder.Close();
        }
    }

    /// <summary>
    /// The certificate decoder fuzz target for libfuzzer.
    /// </summary>
    public static void FuzzCertificateDecoder(ReadOnlySpan<byte> input)
    {
        _ = FuzzableCode.FuzzCertificateDecoderCore(input);
    }

    /// <summary>
    /// The certificate encoder fuzz target for libfuzzer.
    /// </summary>
    public static void FuzzCertificateChainDecoder(ReadOnlySpan<byte> input)
    {
        _ = FuzzableCode.FuzzCertificateChainDecoderCore(input, false);
    }

    /// <summary>
    /// The certificate encoder fuzz target for libfuzzer.
    /// </summary>
    public static void FuzzCertificateChainDecoderCustom(ReadOnlySpan<byte> input)
    {
        _ = FuzzableCode.FuzzCertificateChainDecoderCore(input, true);
    }

    /// <summary>
    /// The CRL decoder fuzz target for libfuzzer.
    /// </summary>
    public static void FuzzCRLDecoder(ReadOnlySpan<byte> input)
    {
        _ = FuzzableCode.FuzzCRLDecoderCore(input, false, false);
    }

    /// <summary>
    /// The CRL encoder fuzz target for libfuzzer.
    /// </summary>
    public static void FuzzCRLEncoder(ReadOnlySpan<byte> input)
    {
        X509CRL? crl = null;
        try
        {
            crl = FuzzableCode.FuzzCRLDecoderCore(input, false, true);
        }
        catch
        {
            return;
        }

        // encode the fuzzed object and see if it crashes
        if (crl != null)
        {
            _ = CrlBuilder.Create(crl).Encode();
        }
    }

    /// <summary>
    /// The fuzz target for the JsonDecoder.
    /// </summary>
    /// <param name="input">A Json Utf8 encoded string with fuzz content.</param>
    /// <param name="throwAll">Whether to throw on all exceptions.</param>
    private static IEncodeable? FuzzJsonDecoderCore(ReadOnlySpan<byte> input, bool throwAll = false)
    {
        string json = Encoding.UTF8.GetString(input);
        return FuzzableCode.FuzzJsonDecoderCore(json, throwAll);
    }

    /// <summary>
    /// Prepare a seekable array segment stream from the input span.
    /// </summary>
    private static ArraySegmentStream PrepareArraySegmentStream(ReadOnlySpan<byte> input)
    {
        const int segmentSize = 0x40;

        // use ArraySegmentStream in combination with fuzz target...
        var bufferCollection = new BufferCollection();
        byte[] buffer;
        int offset = 0;
        do
        {
            buffer = input.Slice(offset, Math.Min(segmentSize, input.Length - offset)).ToArray();
            bufferCollection.Add(new ArraySegment<byte>(buffer));
            offset += segmentSize;
        } while (buffer.Length == segmentSize);

        return new ArraySegmentStream(bufferCollection);
    }
}
