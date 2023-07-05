FROM mcr.microsoft.com/dotnet/sdk:7.0 

RUN apt-get update; \
    apt-get -y upgrade; \
    apt-get -y install vim wget

RUN dotnet tool install --global dotnet-ef; \
    echo "export PATH=$PATH:/root/.dotnet/tools" >> ~/.bashrc