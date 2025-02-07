REM Update seed corpus container with latest fuzz sample seeds
onefuzz.exe config --endpoint https://onefuzz.microsoft.com
REM onefuzz.exe containers create opcua-sdk-binarydecoder
onefuzz.exe containers files upload_dir opcua-sdk-binarydecoder ..\Fuzz\Testcases.Binary
onefuzz.exe containers files list opcua-sdk-binarydecoder
REM onefuzz.exe containers create opcua-sdk-jsondecoder
onefuzz.exe containers files upload_dir opcua-sdk-jsondecoder ..\Fuzz\Testcases.Json
onefuzz.exe containers files list opcua-sdk-jsondecoder
REM onefuzz.exe containers create opcua-sdk-xmldecoder
onefuzz.exe containers files upload_dir opcua-sdk-xmldecoder ..\Fuzz\Testcases.Xml
onefuzz.exe containers files list opcua-sdk-xmldecoder
REM onefuzz.exe containers create opcua-sdk-certdecoder
onefuzz.exe containers files upload_dir opcua-sdk-certdecoder ..\Fuzz\Testcases.Certificates
onefuzz.exe containers files list opcua-sdk-certdecoder
REM onefuzz.exe containers create opcua-sdk-crldecoder
onefuzz.exe containers files upload_dir opcua-sdk-crldecoder ..\Fuzz\Testcases.CRLs
onefuzz.exe containers files list opcua-sdk-crldecoder
