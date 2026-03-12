# `icc3_chat`
======================

Instrucciones de ejecución del proyecto
-------------------------------

# para levantar el servidor:
reemplazar 1112 por el puerto deseado en los siguientes comandos:
se ejecuta así la primera vez:

```
docker build -f servidorSockets/Dockerfile -t servidorchat . && docker run -it  -p 1112:1112 --name servidorChatDocker servidorchat 1112
```

si ya se ha creado el contenedor con ese nombre previamente, el siguiente comando lo elimina antes de volverlo a crear:

```
docker build -f servidorSockets/Dockerfile -t servidorchat . && docker rm servidorChatDocker && docker run -it  -p 1112:1112 --name servidorChatDocker servidorchat
```

# para ejecutar el cliente de consola:

se ejecuta así la primera vez:
```
docker build -f clienteTexto/Dockerfile  -t chatclientetexto . && docker run -it  -p 1112:1112 --name clienteChatDocker chatclientetexto  
```
si ya se ha creado el contenedor con ese nombre previamente, el siguiente comando lo elimina antes de volverlo a crear:
```
docker build -f clienteTexto/Dockerfile  -t chatclientetexto . && docker rm clienteChatDocker && docker run -it  -p 1112:1112 --name clienteChatDocker chatclientetexto  
```
# para ejecutar el cliente web:
si ya se ha creado el contenedor con ese nombre previamente, el siguiente comando lo elimina antes de volverlo a crear:
```
 docker stop chatclienteweb && docker rm chatclienteweb && docker build -f clienteWeb/Dockerfile -t chatclienteweb . && docker run -d  -p 8080:80 -p 8081:443 --name chatclienteweb chatclienteweb
 ```