# MicroRabbit App
This project demonstrates how to build .NET Core Microservices using [RabbitMQ](https://www.rabbitmq.com) messaging.
The source code belongs to a Udemy course that i attended which is named [Getting Started .NET Core Microservices RabbitMQ](https://www.udemy.com/course/getting-started-net-core-microservices-rabbitmq).
Author has already published the code on GitHub before.

## Run RabbitMQ Server
Instead of installing the RabbitMQ server locally on your computer, we can host the server in a [Docker](https://www.docker.com) container.
Since you already have Docker installed on your machine, please run the following command:
```bash
docker run -d --hostname gman-rabbit-mq --name gman -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```
The image used also contains the RabbitMQ management UI that you can find at [http://localhost:15672](http://localhost:15672). **Username** and **password** have both a default value of `guest`.

## Useful Commands
```bash
rabbitmqctl stop_app  #Stops the RabbitMQ server
rabbitmqctl reset     #Resets the RabbitMQ server to initial state
rabbitmqctl start_app #Starts the RabbitMQ server
```