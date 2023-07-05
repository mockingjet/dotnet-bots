include .env
export

ENV=dev
PROJECT=dotnet-bots

up: 
	docker compose -p $(PROJECT) -f docker-compose.$(ENV).yml up -d --build

down: 
	docker compose -p $(PROJECT) -f docker-compose.$(ENV).yml down