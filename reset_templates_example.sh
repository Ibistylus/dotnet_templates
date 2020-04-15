dotnet new -u Ibistylus.Common.Templates
dotnet build [path to working dir]
dotnet pack [path to working dir] 
dotnet new -i [path to compiled nupkg]

rm -rf [path to test folder] 
mkdir [path to test folder] 

cd [path to test folder] 
mkdir [test project folder] 
cd [test project folder] 
dotnet new [creat dotnet project name] 
cd [dot net project source] 
dotnet build
dotnet run
cd [back to original folde] 
