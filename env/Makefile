include .env
export

ENV=dev
PROJECT=dotnet-telegram-bot

up: 
	docker compose -p $(PROJECT) -f docker-compose.$(ENV).yml up -d

down: 
	docker compose -p $(PROJECT) -f docker-compose.$(ENV).yml down