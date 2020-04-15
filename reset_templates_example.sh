dotnet new -u Ibistylus.Common.Templates
dotnet build ~/dev/_templates/dotnet_templates/working
dotnet pack ~/dev/_templates/dotnet_templates/working/
dotnet new -i ~/dev/_templates/dotnet_templates/working/bin/Debug/Ibistylus.Common.Templates.1.0.0.nupkg

rm -rf ~/dev/_templates/dotnet_templates/test
mkdir ~/dev/_templates/dotnet_templates/test

cd test
mkdir MyIbisCustomSolution
cd MyIbisCustomSolution
dotnet new IbisSolution
cd src/MyIbisCustomSolution/
dotnet build
dotnet run
cd ~/dev/_templates/dotnet_templates/
