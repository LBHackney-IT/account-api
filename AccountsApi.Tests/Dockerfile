FROM mcr.microsoft.com/dotnet/sdk:6.0

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ENV DynamoDb_LocalMode='true'
ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN

ARG SONAR_TOKEN
ENV SONAR_TOKEN=$SONAR_TOKEN

WORKDIR /app

RUN apt-get update && apt-get install -y openjdk-17-jdk
RUN dotnet tool install --global dotnet-sonarscanner
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet sonarscanner begin /k:"LBHackney-IT_accounts-api" /o:"lbhackney-it" /d:sonar.host.url=https://sonarcloud.io /d:sonar.login="${SONAR_TOKEN}"

# Copy csproj and restore as distinct layers
COPY ./AccountsApi.sln ./
COPY ./AccountsApi/AccountsApi.csproj ./AccountsApi/
COPY ./AccountsApi.Tests/AccountsApi.Tests.csproj ./AccountsApi.Tests/
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config

RUN dotnet restore ./AccountsApi/AccountsApi.csproj
RUN dotnet restore ./AccountsApi.Tests/AccountsApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c Release -o out AccountsApi/AccountsApi.csproj
RUN dotnet build -c debug -o out AccountsApi.Tests/AccountsApi.Tests.csproj

CMD dotnet test
RUN dotnet sonarscanner end /d:sonar.login="${SONAR_TOKEN}"