FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY ./src ./src
COPY ./test ./test
COPY *.sln .
RUN dotnet restore --verbosity minimal
RUN dotnet publish -c Release -o output -v m

# Test
FROM build AS testrunner
RUN dotnet test --verbosity quiet

RUN dotnet build