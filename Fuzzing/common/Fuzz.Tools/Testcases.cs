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
using Opc.Ua;

public partial class Testcases
{
    private static ServiceMessageContext messageContext = FuzzableCode.MessageContext;

    public delegate void MessageEncoder(IEncoder encoder);

    public static MessageEncoder[] MessageEncoders = new MessageEncoder[] {
        ReadRequest,
        ReadResponse,
        PublishResponse,
        WriteRequest,
    };

    public static void ReadRequest(IEncoder encoder)
    {
        var twoByteNodeIdNumeric = new NodeId(123);
        var nodeIdNumeric = new NodeId(4444, 2);
        var nodeIdString = new NodeId("ns=3;s=RevisionCounter");
        var expandedNodeIdString = new ExpandedNodeId($"nsu={messageContext.NamespaceUris.GetString(3)};s=RevisionCounter");
        var nodeIdGuid = new NodeId(Guid.NewGuid());
        var nodeIdOpaque = new NodeId(new byte[] { 66, 22, 55, 44, 11 });

        // Construct the traceparent header
        var traceData = new AdditionalParametersType();
        string traceparent = "00-480e22a2781fe54d992d878662248d94-b4b37b64bb3f6141-00";
        traceData.Parameters.Add(new KeyValuePair() { Key = "traceparent", Value = traceparent });

        var readRequest = new ReadRequest {
            RequestHeader = new RequestHeader {
                Timestamp = DateTime.UtcNow,
                TimeoutHint = 10000,
                RequestHandle = 422,
                AdditionalHeader = new ExtensionObject(traceData),
                ReturnDiagnostics = (uint)DiagnosticsMasks.All,
            },
            NodesToRead = new ReadValueIdCollection {
                new ReadValueId {
                    NodeId = twoByteNodeIdNumeric,
                    AttributeId = Attributes.UserRolePermissions,
                },
                new ReadValueId {
                    NodeId = nodeIdNumeric,
                    AttributeId = Attributes.Description,
                },
                new ReadValueId {
                    NodeId = nodeIdString,
                    AttributeId = Attributes.Value,
                    IndexRange = "1:2",
                },
                new ReadValueId {
                    NodeId = nodeIdGuid,
                    AttributeId = Attributes.DisplayName,
                },
                new ReadValueId {
                    NodeId = nodeIdNumeric,
                    AttributeId = Attributes.AccessLevel,
                },
                new ReadValueId {
                    NodeId = nodeIdOpaque,
                    AttributeId = Attributes.RolePermissions,
                },
            },
            MaxAge = 1000,
            TimestampsToReturn = TimestampsToReturn.Source,
        };
        encoder.EncodeMessage(readRequest);
    }

    public static void ReadResponse(IEncoder encoder)
    {
        var now = DateTime.UtcNow;
        var nodeId = new NodeId(1000);
        var matrix = new byte[2, 2, 2] { { { 1, 2 }, { 3, 4 } }, { { 11, 22 }, { 33, 44 } } };
        var readRequest = new ReadResponse {
            Results = new DataValueCollection {
                    new DataValue {
                        WrappedValue = new Variant("Hello World"),
                        ServerTimestamp = now,
                        SourceTimestamp = now.AddMinutes(1),
                        ServerPicoseconds = 100,
                        SourcePicoseconds = 10,
                        StatusCode = StatusCodes.Good,
                    },
                    new DataValue {
                        WrappedValue = new Variant((uint)12345678),
                        ServerTimestamp = DateTime.Today,
                        SourceTimestamp = now.AddMinutes(1),
                        StatusCode = StatusCodes.BadDataLost,
                    },
                    new DataValue {
                        WrappedValue = new Variant(new byte[] { 0,1,2,3,4,5,6 }),
                        ServerTimestamp = DateTime.MaxValue,
                        SourceTimestamp = now.AddMinutes(1),
                        StatusCode = StatusCodes.Good,
                    },
                    new DataValue {
                        WrappedValue = new Variant((byte)42),
                        SourceTimestamp = now,
                    },
                    new DataValue {
                        WrappedValue = new Variant((ulong)0xbadbeef),
                        SourceTimestamp = now,
                    },
                    new DataValue {
                        WrappedValue = new Variant(new Matrix(matrix, BuiltInType.Byte)),
                        ServerTimestamp = now,
                    },
                    new DataValue {
                        WrappedValue = new Variant((double)2025.111),
                        SourceTimestamp = now,
                        StatusCode = StatusCodes.BadTooManyOperations
                    },
                    new DataValue {
                        WrappedValue = new Variant(new LocalizedText("en-us", "The text")),
                        SourceTimestamp = now,
                        StatusCode = StatusCodes.BadTooManyOperations
                    },
                    new DataValue {
                        WrappedValue = new Variant(new QualifiedName("The text", 2)),
                        SourceTimestamp = now,
                        ServerPicoseconds = 100,
                        SourcePicoseconds = 10,
                        StatusCode = StatusCodes.BadTooManyOperations
                    },
                    new DataValue {
                        WrappedValue = new Variant(new ExtensionObject(new ThreeDVector(){ X=1.0, Y=-1.0, Z=0.0})),
                        SourceTimestamp = now,
                        ServerPicoseconds = 100,
                        SourcePicoseconds = 10,
                        StatusCode = StatusCodes.Good
                    },
                    new DataValue {
                        WrappedValue = new Variant(new ExtensionObject[] {
                            new ExtensionObject(new ThreeDVector(){ X=1.0, Y=-1.0, Z=0.0}),
                            new ExtensionObject(new ThreeDVector(){ X=1.0, Y=-1.0, Z=0.0})
                            }),
                        SourceTimestamp = now,
                        ServerPicoseconds = 100,
                        SourcePicoseconds = 10,
                        StatusCode = StatusCodes.Good
                    },
                },
            DiagnosticInfos = new DiagnosticInfoCollection {
                        new DiagnosticInfo {
                            AdditionalInfo = "Hello World",
                            InnerStatusCode = StatusCodes.BadCertificateHostNameInvalid,
                            InnerDiagnosticInfo = new DiagnosticInfo {
                                AdditionalInfo = "Hello World",
                                InnerStatusCode = StatusCodes.BadNodeIdUnknown,
                            },
                        },
                    },
            ResponseHeader = new ResponseHeader {
                Timestamp = DateTime.UtcNow,
                AdditionalHeader = new ExtensionObject(new AdditionalParametersType() {
                    Parameters = new KeyValuePairCollection {
                        new KeyValuePair() { Key = "traceparent", Value = "00-480e22a2781fe54d992d878662248d94-b4b37b64bb3f6141-00" },
                    }
                }),
                RequestHandle = 42,
                ServiceResult = StatusCodes.Good,
                ServiceDiagnostics = new DiagnosticInfo {
                    AdditionalInfo = "NodeId not found",
                    InnerStatusCode = StatusCodes.BadAggregateConfigurationRejected,
                    InnerDiagnosticInfo = new DiagnosticInfo {
                        AdditionalInfo = "Hello World",
                        InnerStatusCode = StatusCodes.BadIndexRangeInvalid,
                        InnerDiagnosticInfo = new DiagnosticInfo {
                            AdditionalInfo = "Hello World",
                            InnerStatusCode = StatusCodes.BadSecureChannelIdInvalid,
                            InnerDiagnosticInfo = new DiagnosticInfo {
                                AdditionalInfo = "Hello World",
                                InnerStatusCode = StatusCodes.BadAlreadyExists,
                            },
                        },
                    },
                },
                StringTable = new StringCollection {
                    "Hello",
                    "World",
                    "Goodbye",
                },
            }
        };
        encoder.EncodeMessage(readRequest);
    }

    public static void PublishResponse(IEncoder encoder)
    {
        var now = DateTime.UtcNow;
        var nodeId = new NodeId(1000);
        var expandedNodeId = new ExpandedNodeId(Guid.NewGuid(), messageContext.NamespaceUris.GetString(2));
        var opaqueNodeId = new ExpandedNodeId(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, messageContext.NamespaceUris.GetString(2));
        var matrix = new Matrix(new uint[2, 2, 2] { { { 1, 2 }, { 3, 4 } }, { { 11, 22 }, { 33, 44 } } }, BuiltInType.UInt32);
        var array = new uint[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        var publishResponse = new PublishResponse {
            AvailableSequenceNumbers = new UInt32Collection { 1, 2, 3, 4 },
            MoreNotifications = true,
            SubscriptionId = 1234,
            NotificationMessage = new NotificationMessage {
                SequenceNumber = 123456,
                PublishTime = now,
                NotificationData = new ExtensionObjectCollection {
                    new ExtensionObject(new DataChangeNotification() {
                        MonitoredItems = new MonitoredItemNotificationStruct[] {
                            new MonitoredItemNotificationStruct {
                                ClientHandle = 122,
                                Value = new DataValueStruct {
                                    WrappedValue = new Variant("Hello World"),
                                    ServerTimestamp = now,
                                    SourceTimestamp = now.AddMinutes(1),
                                    ServerPicoseconds = 100,
                                    SourcePicoseconds = 10,
                                    StatusCode = StatusCodes.Good,
                                },
                            },
                            new MonitoredItemNotificationStruct {
                                ClientHandle = 123,
                                Value = new DataValueStruct {
                                    WrappedValue = new Variant(matrix),
                                    ServerTimestamp = now,
                                    SourceTimestamp = now.AddSeconds(1),
                                    ServerPicoseconds = 100,
                                    SourcePicoseconds = 10,
                                    StatusCode = StatusCodes.Good,
                                },
                            },
                            new MonitoredItemNotificationStruct {
                                ClientHandle = 124,
                                Value = new DataValueStruct {
                                    WrappedValue = new Variant(nodeId),
                                    ServerTimestamp = now,
                                    SourceTimestamp = now.AddDays(1),
                                    StatusCode = StatusCodes.Good,
                                },
                            },
                            new MonitoredItemNotificationStruct {
                                ClientHandle = 125,
                                Value = new DataValueStruct {
                                    WrappedValue = new Variant(nodeId)
                                },
                            },
                            new MonitoredItemNotificationStruct {
                                ClientHandle = 125,
                                Value = new DataValueStruct {
                                    WrappedValue = new Variant(true)
                                },
                            },
                            new MonitoredItemNotificationStruct {
                                ClientHandle = 126,
                                Value = new DataValueStruct {
                                    WrappedValue = new Variant((byte)123)
                                },
                            },
                            new MonitoredItemNotificationStruct {
                                ClientHandle = 127,
                                Value = new DataValueStruct {
                                    WrappedValue = new Variant((float)123.123)
                                },
                            },
                            new MonitoredItemNotificationStruct {
                                ClientHandle = 128,
                                Value = new DataValueStruct {
                                    WrappedValue = new Variant((double)12301232.123),
                                    ServerTimestamp = now,
                                    ServerPicoseconds = 100,
                                    SourcePicoseconds = 10,
                                },
                            },
                            new MonitoredItemNotificationStruct {
                                ClientHandle = 129,
                                Value = new DataValueStruct {
                                    WrappedValue = new Variant((Int64)(-123012321234123)),
                                    ServerTimestamp = now,
                                    ServerPicoseconds = 100,
                                    SourcePicoseconds = 10,
                                },
                            },
                            new MonitoredItemNotificationStruct {
                                ClientHandle = 130,
                                Value = new DataValueStruct {
                                    WrappedValue = new Variant((UInt64)123012321234123),
                                    ServerTimestamp = now,
                                    ServerPicoseconds = 100,
                                    SourcePicoseconds = 10,
                                },
                            },
                            new MonitoredItemNotificationStruct {
                                ClientHandle = 131,
                                Value = new DataValueStruct {
                                    WrappedValue = new Variant(array),
                                    ServerTimestamp = now,
                                    ServerPicoseconds = 100,
                                    SourcePicoseconds = 10,
                                },
                            },
                        },
                    }),
                },
            },
            DiagnosticInfos = new DiagnosticInfoCollection {
                        new DiagnosticInfo {
                            AdditionalInfo = "Hello World",
                            InnerStatusCode = StatusCodes.BadCertificateHostNameInvalid,
                            InnerDiagnosticInfo = new DiagnosticInfo {
                                AdditionalInfo = "Hello World",
                                InnerStatusCode = StatusCodes.BadNodeIdUnknown,
                            },
                        },
                    },
            ResponseHeader = new ResponseHeader {
                Timestamp = DateTime.UtcNow,
                RequestHandle = 42,
                ServiceResult = StatusCodes.Good,
                StringTable = new StringCollection {
                    "No error occurred"
                },
            }
        };
        encoder.EncodeMessage(publishResponse);
    }

    public static void WriteRequest(IEncoder encoder)
    {
        var writeRequest = new WriteRequest {
            RequestHeader = new RequestHeader {
                Timestamp = DateTime.UtcNow,
                TimeoutHint = 10000,
                RequestHandle = 422,
                ReturnDiagnostics = (uint)DiagnosticsMasks.All,
            },
            NodesToWrite = new WriteValueCollection {
                new WriteValue {
                    NodeId = new NodeId(123),
                    AttributeId = Attributes.Value,
                    IndexRange = "1:2",
                    Value = new DataValue(new Variant("Hello World")) {
                        ServerTimestamp = DateTime.UtcNow,
                        SourceTimestamp = DateTime.UtcNow.AddMinutes(1),
                        ServerPicoseconds = 100,
                        SourcePicoseconds = 10,
                        StatusCode = StatusCodes.Good,
                    },
                },
                new WriteValue {
                    NodeId = new NodeId(124),
                    AttributeId = Attributes.ValueRank,
                    Value = new DataValue(new Variant(12345)) {
                        ServerTimestamp = DateTime.UtcNow,
                        SourceTimestamp = DateTime.UtcNow.AddMinutes(1),
                        StatusCode = StatusCodes.Good,
                    },
                },
                new WriteValue {
                    NodeId = new NodeId(125),
                    AttributeId = Attributes.Value,
                    Value = new DataValue(new Variant(123.45f)) {
                        ServerTimestamp = DateTime.UtcNow,
                        SourceTimestamp = DateTime.UtcNow.AddMinutes(1),
                        StatusCode = StatusCodes.Good,
                    },
                },
                new WriteValue {
                    NodeId = new NodeId(126),
                    AttributeId = Attributes.Value,
                    Value = new DataValue(new Variant(123.45)) {
                        ServerTimestamp = DateTime.UtcNow,
                        SourceTimestamp = DateTime.UtcNow.AddMinutes(1),
                        StatusCode = StatusCodes.Good,
                    },
                },
                new WriteValue {
                    NodeId = new NodeId(127),
                    AttributeId = Attributes.Value,
                    Value = new DataValue(new Variant(true)) {
                        ServerTimestamp = DateTime.UtcNow,
                        SourceTimestamp = DateTime.UtcNow.AddMinutes(1),
                        StatusCode = StatusCodes.Good,
                    },
                },
                new WriteValue {
                    NodeId = new NodeId(128),
                    AttributeId = Attributes.Value,
                    Value = new DataValue(new Variant(new byte[] { 1, 2, 3, 4, 5 })) {
                        ServerTimestamp = DateTime.UtcNow,
                        SourceTimestamp = DateTime.UtcNow.AddMinutes(1),
                        StatusCode = StatusCodes.Good,
                    },
                },
                new WriteValue {
                    NodeId = new NodeId("s=\"FastCounter\"", 2),
                    AttributeId = Attributes.Value,
                    Value = new DataValue(new Variant(new int[] { 1, 2, 3, 4, 5 })) {
                        ServerTimestamp = DateTime.UtcNow,
                        SourceTimestamp = DateTime.UtcNow.AddMinutes(1),
                        StatusCode = StatusCodes.Good,
                    },
                },
                new WriteValue {
                    NodeId = new NodeId(new byte [] { 0xaa, 0xbb, 0xcc, 0xdd, 0xee}, 3),
                    AttributeId = Attributes.Value,
                    Value = new DataValue(new Variant(new float[] { 1.1f, 2.2f, 3.3f })) {
                        ServerTimestamp = DateTime.UtcNow,
                        SourceTimestamp = DateTime.UtcNow.AddMinutes(1),
                        StatusCode = StatusCodes.Good,
                    },
                },
                new WriteValue {
                    NodeId = new NodeId(Guid.NewGuid()),
                    AttributeId = Attributes.Value,
                    Value = new DataValue(new Variant(new double[] { 1.1, 2.2, 3.3 })) {
                        ServerTimestamp = DateTime.UtcNow,
                        SourceTimestamp = DateTime.UtcNow.AddMinutes(1),
                        StatusCode = StatusCodes.Good,
                    },
                },
                new WriteValue {
                    NodeId = new NodeId(132, 3),
                    AttributeId = Attributes.Value,
                    Value = new DataValue(new Variant(new string[] { "one", "two", "three" })) {
                        ServerTimestamp = DateTime.UtcNow,
                        SourceTimestamp = DateTime.UtcNow.AddMinutes(1),
                        StatusCode = StatusCodes.Good,
                    },
                },
            },
        };
        encoder.EncodeMessage(writeRequest);
    }
}
