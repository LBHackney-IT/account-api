FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ENV DynamoDb_LocalMode='true'
ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./AccountsApi.sln ./
COPY ./AccountsApi/AccountsApi.csproj ./AccountsApi/
COPY ./AccountsApi.Tests/AccountsApi.Tests.csproj ./AccountsApi.Tests/
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config

RUN dotnet restore ./AccountsApi/AccountsApi.csproj
RUN dotnet restore ./AccountsApi.Tests/AccountsApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out AccountsApi.Tests/AccountsApi.Tests.csproj

CMD dotnet test
