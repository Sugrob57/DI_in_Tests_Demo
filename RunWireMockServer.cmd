@setlocal
@echo off
@if NOT "%ECHO%"=="" @echo %ECHO%

set CMDHOME=%~dp0.

if exist "%CMDHOME%\Services\MyServiceSample\bin\Debug\WireMock.Server\net6.0\MyService.Sample.dll" (
cd /d "%CMDHOME%\Services\MyServiceSample\bin\Debug\WireMock.Server\net6.0\"
dotnet MyService.Sample.dll
cd "%CMDHOME%"
) else (
@echo Build DIDemoTestProject.sln and then run the program
)

TIMEOUT /T 60