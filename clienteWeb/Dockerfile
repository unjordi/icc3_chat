# Primero, usamos la imagen base oficial  build 
# esta se desecha al final porque queda llena de basurita
# por todos los pasos de compilación.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY clienteWeb.csproj .
RUN dotnet restore clienteWeb.csproj
COPY . .
RUN dotnet build clienteWeb.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish clienteWeb.csproj -c Release -o /app/publish

# Ahora pasamos todo a la imagen base-------------------
# que vamos a dejar limpiecita y
# nomás vamos a usar como runtime
FROM nginx:alpine AS final
EXPOSE 80
EXPOSE 443
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
