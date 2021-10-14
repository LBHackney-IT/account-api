.PHONY: setup
setup:
	docker-compose build

.PHONY: build
build:
	docker-compose build accounts-api

.PHONY: serve
serve:
	docker-compose build accounts-api && docker-compose up accounts-api

.PHONY: shell
shell:
	docker-compose run accounts-api bash

.PHONY: test
test:
	docker-compose up dynamodb-database & docker-compose build accounts-api-test && docker-compose up accounts-api-test

.PHONY: lint
lint:
	-dotnet tool install -g dotnet-format
	dotnet tool update -g dotnet-format
	dotnet format

.PHONY: restart-db
restart-db:
	docker stop $$(docker ps -q --filter ancestor=dynamodb-database -a)
	-docker rm $$(docker ps -q --filter ancestor=dynamodb-database -a)
	docker rmi dynamodb-database
	docker-compose up -d dynamodb-database
