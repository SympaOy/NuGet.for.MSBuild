@ECHO OFF

SET NuGetToolsPath=.\tools\NuGet
SET KeyFile=F:\PrivateKeys\Winnster.snk

MSBuild.exe .\src\NuGet.for.MSBuild.sln /t:Rebuild /p:Configuration=Release /p:Platform="Any CPU" /p:AssemblyOriginatorKeyFile="%KeyFile%" /fl
%NuGetToolsPath%\NuGet.exe pack ".\src\NuGet.for.MSBuild.nuspec" -BasePath .\src\ -OutputDirectory .\