# P.R.S.S - Publication.Rabbit.Subscription.Storage
Целью проекта является демонстрация знаний и навыков в области разработки крос платформенных приложений на микросервисной архитектуре.
Проект в данный момент состоит из 5 контейнеров web-input, rmq-publisher, rmq-subscriber, rabbitMq, console-input.

web-input-container: 
  blazor app on .NET6
  web page for input person data
  
console-input-container:
    console app on .NET6
    it was used for fast input Person data for testing other services

rmq-publisher-container:
    it can receive and validate data by http and send it to rabbitMQ
    it consist of several projects on .NET6. 

rmq-subscriber-container:
it can receive data from rabbit.

rabbitMq-container:
it contains a rabbitMQ instance


Applications rmq-subscriber and rmq-publisher have a similar architecture for ease of understanding,
this can be presented as several parts: FACADE, INFRA, DOMAIN, BL.

FACADE - it contains interfaces for interacting with the app, argument interfaces and their default implementations.

INFRA - it contains:
Proxy - it contains implementations of facade interfaces.
Dto - it contains dto models.
Daemon - program entry point, http api controllers, validations, app setting.

DOMAIN - it contains core interfaces of logic of the app

BL - it contains implementations of domain interfaces.