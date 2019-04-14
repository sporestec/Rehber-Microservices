# Rehber-Microservices

docker network create somenetwork

<h3> RabbitMQ </h3>
 docker run -d -p 15672:15672 -p 5672:5672 rabbitmq:3-management

<h3> Elasticsearch </h3>
 docker run -d --name elasticsearch --net somenetwork -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" elasticsearch:6.6.2

<h3> Kibana </h3>
 docker run -d --name kibana --net somenetwork -p 5601:5601 kibana:6.4.0