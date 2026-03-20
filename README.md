# `ICC3 Chat 💬 de Jordi Serra`

[![GitHub Repo](https://img.shields.io/badge/Repo-GitHub-181717?style=for-the-badge&logo=github)](https://github.com/unjordi/icc3_chat)
[![GitHub license](https://img.shields.io/github/license/unjordi/icc3_chat?style=for-the-badge)](https://github.com/unjordi/icc3_chat/blob/main/LICENSE)
[![Last Commit](https://img.shields.io/github/last-commit/unjordi/icc3_chat?style=for-the-badge&logo=github&color=32CD32)](https://github.com/unjordi/icc3_chat) 
[![README Update Status](https://img.shields.io/github/actions/workflow/status/unjordi/icc3_chat/main.yml?label=README%20Update&logo=github&style=for-the-badge)](https://github.com/unjordi/icc3_chat/actions/workflows/main.yml)

![.NET Version](https://img.shields.io/badge/.NET-10.0-512bd4?logo=dotnet&style=for-the-badge)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)


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
reemplazar 127.0.0.1 por la ip del servidor en los siguientes comandos;  
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


<!--START_SECTION:activity-->

<!--END_SECTION:activity-->
