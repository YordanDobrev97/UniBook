FROM microsoft/dotnet:3.1-aspnetcore-runtime AS base
WORKDIR /app

CMD ASPNETCORE_URLS=http://*:$PORT dotnet UniBook.Web.dll