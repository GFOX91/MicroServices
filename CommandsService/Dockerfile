# pull down SDK image create working directory
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

#copy the csproj file
COPY *.csproj ./
#run dot net restore to pull down packages we need for our image
RUN dotnet restore

#copy the files over and run publish
COPY . ./
RUN dotnet publish -c Release -o out

#pull down runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "CommandsService.dll" ]