[![CI/CD](https://github.com/cawadead/DistantWorkCalendarService/actions/workflows/pipeline.yml/badge.svg?branch=master)](https://github.com/cawadead/DistantWorkCalendarService/actions/workflows/pipeline.yml)

# Сервис для хранения данных календаря удаленки отдела УППСОТП

- .NET6
- PostgreSQL

## Сборка сервиса

`docker build -t registry.extserv.ru/distant-work-calendar-service:latest -f docker/Dockerfile . --platform linux/amd64`

`docker push registry.extserv.ru/distant-work-calendar-service:latest`

## Репозиторий с фронтом

https://github.com/Kjanys/distant-work
