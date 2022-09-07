dotnet publish -c Release -o ./src/RmqPublisher/Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon/bin/published ./src/RmqPublisher/Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon
dotnet publish -c Release -o ./src/RmqSubscriber/Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon/bin/published ./src/RmqSubscriber/Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon
dotnet publish -c Release -o ./src/WebInput/Publication.Rabbit.Subscription.Storage.WebInput/bin/published ./src/WebInput/Publication.Rabbit.Subscription.Storage.WebInput
docker-compose -f ./docker-compose.yml -p PublicationRabbitSubscriptionStorage --ansi never up -d --build
docker image prune -f