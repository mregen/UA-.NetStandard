/* ========================================================================
 * Copyright (c) 2005-2024 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 * 
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.IO;
using System.Text;
using Opc.Ua;

/// <summary>
/// Fuzzing code for the JSON decoder and encoder.
/// </summary>
public static partial class FuzzableCode
{
    /// <summary>
    /// The Json decoder fuzz target for afl-fuzz.
    /// </summary>
    public static void AflfuzzJsonDecoder(string input)
    {
        _ = FuzzJsonDecoderCore(input);
    }

    /// <summary>
    /// The Json encoder fuzz target for afl-fuzz.
    /// </summary>
    public static void AflfuzzJsonEncoder(string input)
    {
        FuzzJsonEncoderCore(input);
    }

    /// <summary>
    /// The binary encoder Json decoder fuzz target for afl-fuzz.
    /// </summary>
    /// <param name="stream">The stdin stream from the afl-fuzz process.</param>
    public static void AflfuzzBinaryJsonEncoder(Stream stream)
    {
        FuzzBinaryJsonEncoderCore(stream);
    }

    /// <summary>
    /// The Json decoder fuzz target for libfuzzer.
    /// </summary>
    public static void LibfuzzJsonDecoder(ReadOnlySpan<byte> input)
    {
#if NETFRAMEWORK
        string json = Encoding.UTF8.GetString(input.ToArray());
#else
        string json = Encoding.UTF8.GetString(input);
#endif
        _ = FuzzJsonDecoderCore(json);
    }

    /// <summary>
    /// The Json encoder fuzz target for libfuzzer.
    /// </summary>
    public static void LibfuzzJsonEncoder(ReadOnlySpan<byte> input)
    {
        FuzzJsonEncoderCore(input);
    }

    /// <summary>
    /// The Json encoder fuzz target for libfuzzer.
    /// </summary>
    public static void LibfuzzBinaryJsonEncoder(ReadOnlySpan<byte> input)
    {
        FuzzBinaryJsonEncoderCore(input);
    }

    /// <summary>
    /// The fuzz target for the JsonDecoder.
    /// </summary>
    /// <param name="json">A string with fuzz content.</param>
    internal static IEncodeable FuzzJsonDecoderCore(string json, bool throwAll = false)
    {
        try
        {
            using (var decoder = new JsonDecoder(json, messageContext))
            {
                return decoder.DecodeMessage(null);
            }
        }
        catch (ServiceResultException sre)
        {
            switch (sre.StatusCode)
            {
                case StatusCodes.BadEncodingLimitsExceeded:
                case StatusCodes.BadDecodingError:
                    if (!throwAll)
                    {
                        return null;
                    }
                    break;
            }

            throw;
        }
    }

    /// <summary>
    /// The fuzz target for the Json encoder core.
    /// </summary>
    internal static void FuzzJsonEncoderCore(ReadOnlySpan<byte> input)
    {
#if NETFRAMEWORK
        string json = Encoding.UTF8.GetString(input.ToArray());
#else
        string json = Encoding.UTF8.GetString(input);
#endif

        FuzzJsonEncoderCore(json);
    }

    /// <summary>
    /// The fuzz target for the Json encoder core.
    /// </summary>
    internal static void FuzzJsonEncoderCore(string json)
    {
        IEncodeable deserialized;
        try
        {
            deserialized = FuzzJsonDecoderCore(json);
        }
        catch
        {
            return;
        }

        if (deserialized != null)
        {
            foreach (JsonEncodingType jsonEncodingType in Enum.GetValues<JsonEncodingType>())
            {
                // see if this throws
                using (var encoder = new JsonEncoder(messageContext, jsonEncodingType))
                {
                    switch (jsonEncodingType)
                    {
                        case JsonEncodingType.NonReversible:
                            encoder.EncodeNodeIdAsString = true;
                            encoder.ForceNamespaceUriForIndex1 = true;
                            break;
                    }

                    encoder.EncodeMessage(deserialized);
                    json = encoder.CloseAndReturnText();
                }

                _ = FuzzableCode.FuzzJsonDecoderCore(json);
            }
        }
    }

    /// <summary>
    /// The fuzz target for the Json encoder core with a binary encoded fuzz input.
    /// Tests all Json encoding types if it throws.
    /// </summary>
    internal static void FuzzBinaryJsonEncoderCore(ReadOnlySpan<byte> input)
    {
        using (var memoryStream = new MemoryStream(input.ToArray()))
        {
            FuzzBinaryJsonEncoderCore(memoryStream);
        }
    }

    /// <summary>
    /// The fuzz target for the Json encoder core with a binary encoded fuzz input.
    /// Tests all Json encoding types if it throws.
    /// </summary>
    internal static void FuzzBinaryJsonEncoderCore(Stream stream)
    {
        IEncodeable encodeable = null;
        try
        {
            encodeable = FuzzBinaryDecoderCore(stream, true);
        }
        catch
        {
            return;
        }

        // see if this throws
        if (encodeable != null)
        {
            foreach (JsonEncodingType jsonEncodingType in Enum.GetValues<JsonEncodingType>())
            {
                string json;
                using (var encoder = new JsonEncoder(messageContext, jsonEncodingType))
                {
                    switch (jsonEncodingType)
                    {
                        case JsonEncodingType.NonReversible:
                            encoder.EncodeNodeIdAsString = true;
                            encoder.ForceNamespaceUriForIndex1 = true;
                            break;
                    }

                    encoder.EncodeMessage(encodeable);
                    json = encoder.CloseAndReturnText();
                }

                _ = FuzzableCode.FuzzJsonDecoderCore(json);
            }
        }
    }
}

