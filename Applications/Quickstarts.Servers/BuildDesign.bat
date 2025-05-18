@echo off
setlocal

REM if docker is not available, ensure the Opc.Ua.ModelCompiler.exe is in the PATH
set MODELCOMPILER=Opc.Ua.ModelCompiler.exe
REM The version of the ModelCompiler from the OPCF to use as docker container
set MODELCOMPILERIMAGE=ghcr.io/opcfoundation/ua-modelcompiler:2.3.0
REM private build until the official image is updated
set MODELCOMPILERIMAGE=ghcr.io/mregen/ua-modelcompiler:latest-docker-nodeset2
set MODELROOT=.

echo pull latest modelcompiler from github container registry
echo %MODELCOMPILERIMAGE%
docker pull %MODELCOMPILERIMAGE%
IF ERRORLEVEL 1 (
:nodocker
    Echo The docker command to download ModelCompiler failed. Using local PATH instead to execute ModelCompiler.
) ELSE (
    Echo Successfully pulled the latest docker container for ModelCompiler.
    set MODELROOT=/model
    set MODELCOMPILER=docker run -v "%CD%:/model" -it --rm --name ua-modelcompiler %MODELCOMPILERIMAGE% 
)

echo Building TestData
%MODELCOMPILER% compile -version v104 -id 1000 -d2 "%MODELROOT%/TestData/TestDataDesign.xml" -cg "%MODELROOT%/TestData/TestDataDesign.csv" -o2 "%MODELROOT%/TestData"
IF %ERRORLEVEL% EQU 0 echo Success!

echo Building MemoryBuffer
%MODELCOMPILER% compile -version v104 -id 1000 -d2 "%MODELROOT%/MemoryBuffer/MemoryBufferDesign.xml" -cg "%MODELROOT%/MemoryBuffer/MemoryBufferDesign.csv" -o2 "%MODELROOT%/MemoryBuffer" 
IF %ERRORLEVEL% EQU 0 echo Success!

echo Building BoilerDesign
%MODELCOMPILER% compile -version v104 -id 1000 -d2 "%MODELROOT%/Boiler/BoilerDesign.xml" -cg "%MODELROOT%/Boiler/BoilerDesign.csv" -o2 "%MODELROOT%/Boiler"
IF %ERRORLEVEL% EQU 0 echo Success!

echo Building DI from Nodeset2
%MODELCOMPILER% compile -version v104 -id 1000 -d2 "%MODELROOT%/Opc.Ua.Di.NodeSet2.xml,Opc.Ua.DI,OpcUaDI" -o2 "%MODELROOT%/DI"
IF %ERRORLEVEL% EQU 0 echo Success!

echo Building IA from Nodeset2
%MODELCOMPILER% compile -version v104 -id 1000 -d2 "%MODELROOT%/Opc.Ua.IA.NodeSet2.xml,Opc.Ua.IA,OpcUaIA" -d2 "%MODELROOT%/Opc.Ua.Di.NodeSet2.xml,Opc.Ua.DI,OpcUaDI" -o2 "%MODELROOT%/IA"
IF %ERRORLEVEL% EQU 0 echo Success!

echo Building Machinery from Nodeset2
%MODELCOMPILER% compile -version v104 -id 1000 -d2 "%MODELROOT%/Opc.Ua.Machinery.NodeSet2.xml,Opc.Ua.Machinery,OpcUaMachinery" -d2 "%MODELROOT%/Opc.Ua.Di.NodeSet2.xml,Opc.Ua.DI,OpcUaDI" -o2 "%MODELROOT%/Machinery"
IF %ERRORLEVEL% EQU 0 echo Success!

echo Building Machinery.Examples from Nodeset2
%MODELCOMPILER% compile -version v104 -id 1000 -d2 "%MODELROOT%/Opc.Ua.Machinery.Examples.NodeSet2.xml,Opc.Ua.Machinery.Examples,OpcUaMachineryExamples" -d2 "%MODELROOT%/Opc.Ua.Di.NodeSet2.xml,Opc.Ua.DI,OpcUaDI" -d2 "%MODELROOT%/Opc.Ua.Machinery.NodeSet2.xml,Opc.Ua.Machinery,OpcUaMachinery" -o2 "%MODELROOT%/Machinery.Examples"
IF %ERRORLEVEL% EQU 0 echo Success!

echo Building Robotics from Nodeset2
%MODELCOMPILER% compile -version v104 -id 1000 -d2 "%MODELROOT%/Opc.Ua.Robotics.NodeSet2.xml,Opc.Ua.Robotics,OpcUaRobotics" -d2 "%MODELROOT%/Opc.Ua.Di.NodeSet2.xml,Opc.Ua.DI,OpcUaDI" -o2 "%MODELROOT%/Robotics"
IF %ERRORLEVEL% EQU 0 echo Success!

echo Building MachineTool from Nodeset2
%MODELCOMPILER% compile -version v104 -id 1000 -d2 "%MODELROOT%/Opc.Ua.MachineTool.NodeSet2.xml,Opc.Ua.MachineTool,OpcUaMachineTool" -d2 "%MODELROOT%/Opc.Ua.Di.NodeSet2.xml,Opc.Ua.DI,OpcUaDI" -d2 "%MODELROOT%/Opc.Ua.IA.NodeSet2.xml,Opc.Ua.IA,OpcUaIA" -d2 "%MODELROOT%/Opc.Ua.Machinery.NodeSet2.xml,Opc.Ua.Machinery,OpcUaMachinery" -o2 "%MODELROOT%/MachineTool"
IF %ERRORLEVEL% EQU 0 echo Success!

echo Building Woodworking from Nodeset2
%MODELCOMPILER% compile -version v104 -id 1000 -d2 "%MODELROOT%/Opc.Ua.Woodworking.NodeSet2.xml,Opc.Ua.Woodworking,OpcUaWoodworking" -d2 "%MODELROOT%/Opc.Ua.Di.NodeSet2.xml,Opc.Ua.DI,OpcUaDI" -d2 "%MODELROOT%/Opc.Ua.Machinery.NodeSet2.xml,Opc.Ua.Machinery,OpcUaMachinery" -o2 "%MODELROOT%/Woodworking"
IF %ERRORLEVEL% EQU 0 echo Success!

echo Building MachineVision from Nodeset2
%MODELCOMPILER% compile -version v104 -id 1000 -d2 "%MODELROOT%/Opc.Ua.MachineVision.NodeSet2.xml,Opc.Ua.MachineVision,OpcUaMachineVision"  -o2 "%MODELROOT%/MachineVision"
IF %ERRORLEVEL% EQU 0 echo Success!

echo Building StructuresWithArrays from Nodeset2
%MODELCOMPILER% compile -version v104 -id 1000 -d2 "%MODELROOT%/StructuresWithArrays.Nodeset2.xml,StructuresWithArrays,StructuresWithArrays" -o2 "%MODELROOT%/StructuresWithArrays"
IF %ERRORLEVEL% EQU 0 echo Success!

