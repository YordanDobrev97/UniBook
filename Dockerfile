FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
RUN mkdir \app
WORKDIR /app

ENTRYPOINT ["dotnet", "UniBook.Web.dll"]

COPY Web/UniBook.Web/UniBook.Web.csproj .app
COPY Web/UniBook.Web.Infrastucture/UniBook.Web.Infrastructure.csproj .app
COPY Web/UniBook.Web.ViewModels/UniBook.Web.ViewModels.csproj .app

COPY Data/Data/UniBook.Data.csproj .app
COPY Data/Data/UniBook.Data.Common.csproj .app
COPY Data/Data/UniBook.Data.Models.csproj .app

COPY Service/UniBook.Services/UniBook.Services.csproj .app
COPY Service/UniBook.Services.Data/UniBook.Services.Data.csproj .app
COPY Service/UniBook.Services.Mapping/UniBook.Services.Mapping.csproj .app
COPY Service/UniBook.Services.Messaging/UniBook.Services.Messaging.csproj .app

COPY Tests/Sandbox/Sandbox.csproj .app
COPY Tests/UniBook.Services.Data.Tests/UniBook.Services.Data.Tests.csproj .app
COPY Tests/UniBook.Web.Tests/UniBook.Web.Tests.csproj .app

COPY UniBook.Common/UniBook.Common.csproj .app

RUN dotnet restore

WORKDIR /app
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
COPY --from=build /app/out ./

CMD ASPNETCORE_URLS=http://*:$PORT dotnet UniBook.Web.dll