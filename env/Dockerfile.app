FROM mcr.microsoft.com/dotnet/sdk:7.0 

RUN apt-get update; \
    apt-get -y upgrade; \
    apt-get -y install vim wget