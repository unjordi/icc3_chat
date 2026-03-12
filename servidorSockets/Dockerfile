# Este Dockerfile construye la imagen para el servidor
# Primero, usamos la imagen base oficial  build 
# esta se desecha al final porque queda llena de basurita
# por todos los pasos de compilación.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["./servidorSockets/servidorSockets.csproj", "servidorSockets/"]
COPY ["./modelo/modelo.csproj", "modelo/"] 
RUN dotnet restore "servidorSockets/servidorSockets.csproj"
COPY . .
WORKDIR "/src/servidorSockets"
RUN dotnet build servidorSockets.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish servidorSockets.csproj -c Release -o /app/publish

# Ahora pasamos todo a la imagen base-------------------
# que vamos a dejar limpiecita y
# nomás vamos a usar como runtime
FROM mcr.microsoft.com/dotnet/runtime:10.0
ENV TERM=xterm-256color
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet", "servidorSockets.dll" ]

#se ejecuta así la primera vez:
	# docker build -t servidorchat . && docker run -it  -p 1112:1112 --name servidorChatDocker servidorchat 1112

# si ya se ha creado el contenedor con ese nombre previamente, el siguiente comando lo elimina antes de volverlo a crear:
	# docker build -t servidorchat . && docker rm servidorChatDocker && docker run -it  -p 1112:1112 --name servidorChatDocker servidorchat 