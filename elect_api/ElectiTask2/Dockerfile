FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR "/src/ElectiTask2"
COPY . .
RUN dotnet build 
EXPOSE 5283

ENTRYPOINT ["dotnet", "run"]