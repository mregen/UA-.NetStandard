# Microsoft Aio OPC UA .NET Library - NuGet Packages

## Overview

The Microsoft Aio OPC UA .NET Library provides a cross-platform, high-performance implementation of the OPC Unified Architecture (UA) specifications. It enables developers to integrate OPC UA capabilities into their .NET applications with support for secure and reliable communication between industrial automation systems. 

Microsoft Aio optimized the open source OPC Foundation OPC UA library in this release for high throughput and optimized memory allocations when sending the JSON encoded data to a cloud broker via MQTTv5. Only the latest .NET LTS and preview versions are supported.

## Getting Started

To install the OPC UA .NET Standard Library via NuGet for a server or client project, use the following commands:

Server:
```powershell
Install-Package Microsoft.Aio.Opc.Ua.Server
```

Client:
```powershell
Install-Package Microsoft.Aio.Opc.Ua.Client
```

For a list of available packages released by Microsoft Aio and their specific functionalities, visit the [NuGet Gallery](https://www.nuget.org/profiles/Microsoft).

## Features

- Cross-platform support (Windows, Linux, macOS)
- Secure and encrypted communication
- Compliance with OPC UA specifications up to 1.05
- Client and server development support
- Subscription and event handling
- High performance Json encoder based on System.Text.Json

## Documentation

Comprehensive documentation, tutorials, and examples can be found in the official OPC Foundation repository:

- [GitHub Repository](https://github.com/OPCFoundation/UA-.NETStandard)
- [OPC Foundation Website](https://opcfoundation.org/)
- [Microsoft Repository](https://dev.azure.com/msazure/One/_git/aio-opcua-sdk)

## Licensing

As an OPC Foundation Corporate Member, Microsoft distributes any code [dual licensed](https://opcfoundation.org/license/source/1.11/index.html) from the OPC Foundation under the [RCL](https://opcfoundation.org/license/rcl.html) or [GPLv2](https://opcfoundation.org/license/gpl.html) license, under the **RCL** license.

All Microsoft Aio OPC UA .NET Standard NuGet packages are released under the [Microsoft MIT license](https://dev.azure.com/msazure/One/_git/aio-opcua-sdk?path=/LICENSE).

Portions were licensed by Microsoft under the [OPC Foundation Reciprocal Community License (RCL)](https://opcfoundation.org/license/rcl.html). This license ensures that contributions and modifications remain open to the community while requiring reciprocal sharing of derivative works.

By using these NuGet packages, you agree to comply with the terms and conditions of the Microsoft MIT License. Please review the full license text here:

- [License Details](https://dev.azure.com/msazure/One/_git/aio-opcua-sdk?path=/LICENSE)

## Contributing

Contributions to the OPC UA .NET Standard Library are welcomed! If you’d like to contribute, please follow the guidelines in the GitHub repository.

## Support

For issues, feature requests, or discussions, please refer to the GitHub issues section or the OPC Foundation forums.

- [GitHub Issues](https://github.com/OPCFoundation/UA-.NETStandard/issues)
- [OPC Foundation Forum](https://opcfoundation.org/forum/)

---

© Microsoft Corporation. All rights reserved.
Portions copyright © by OPC Foundation, Inc. and licensed under the Reciprocal Community License (RCL). https://opcfoundation.org/license/rcl.html 
