# `icc3_chat de Jordi Serra`
======================
![GitHub](https://img.shields.io/badge/github-%23121011.svg?style=for-the-badge&logo=github&logoColor=white)

<!--START_SECTION:activity-->

<!--END_SECTION:activity-->

[![Version](https://img.shields.io/github/v/release/unjordi/icc3_chat?color=%230567ff&label=Latest%20Release&style=for-the-badge)](https://github.com/unjordi/icc3_chat)
![GitHub Downloads (specific asset, all releases)](https://img.shields.io/github/downloads/unjordi/icc3_chat/icc3_chat?label=Total%20Downloads&style=for-the-badge)


Instrucciones de ejecución del proyecto
-------------------------------
Todos los comandos se ejecutan desde la raíz del proyecto 

# 🛠 para levantar el servidor:
reemplazar 1112 por el puerto deseado en los siguientes comandos:
se ejecuta así la primera vez:

![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)


	docker build -f servidorSockets/Dockerfile -t servidorchat . 
	docker run -it  -p 1112:1112 --name servidorChatDocker servidorchat 1112


si ya se ha creado el contenedor con ese nombre previamente, el siguiente comando lo elimina antes de volverlo a crear:

![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)


	docker build -f servidorSockets/Dockerfile -t servidorchat .  
	docker rm servidorChatDocker  
	docker run -it  -p 1112:1112 --name servidorChatDocker servidorchat

# 🛠 para ejecutar el cliente de consola:
reemplazar 127.0.0.1 por la ip del servidor en los siguientes comandos:
reemplazar 1112 por el puerto deseado en los siguientes comandos:
se ejecuta así la primera vez:

![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)

	docker build -f clienteTexto/Dockerfile  -t chatclientetexto .  
	docker run -it --name clienteChatDocker chatclientetexto 127.0.0.1 1112


si ya se ha creado el contenedor con ese nombre previamente, el siguiente comando lo elimina antes de volverlo a crear:

![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)

	docker build -f clienteTexto/Dockerfile  -t chatclientetexto .  
	docker rm clienteChatDocker  
	docker run -it --name clienteChatDocker chatclientetexto 127.0.0.1 1112

# 🛠 para ejecutar el cliente web:
si ya se ha creado el contenedor con ese nombre previamente, el siguiente comando lo elimina antes de volverlo a crear:

![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)

	docker stop chatclienteweb && docker rm chatclienteweb  
	docker build -f clienteWeb/Dockerfile -t chatclienteweb .  
	docker run -d  -p 8080:80 -p 8081:443 --name chatclienteweb chatclienteweb
