# SalesProductApi asp.net core 3.1 

#conecta no container com o mysql.
#comando para rodar o container do mysql.
docker run -e MYSQL_ROOT_HOST=% -e MYSQL_ROOT_PASSWORD=123 --name mysqlserver -d -p=3306:3306 mysql/mysql-server:5.7.31

#comando para enviar uma requisição POST via CURL
curl -k -d '{"Amount": 200,"Description": "Product 001","Status": "Active","Price": "50"}' -H "Content-Type: application/json" -X POST https://localhost:5001/Product


#comando para acessar o mysql dentro do container
docker exec -it mysqlserver mysql -u root -p

#comando para do docker para criar a imagem
docker build --tag salesproductapi .

#comando para criar e rodar o container
docker run --name salesproductapi -d -p 8087:80 --link sql1 salesproductapi

## command to create sqlserver container
sudo docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Mau123&&&" \ -p 1433:1433 --name sql1 \ -d mcr.microsoft.com/mssql/server:2019-latest

## LINK with sqlcmd commands
https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-ubuntu?view=sql-server-linux-2017

## get ip of container
docker inspect -f "{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}" sql1

## iterate with sqlserver in Container Docker
docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa

## command docker run container 
docker run --name salesproductapi -d -p 8087:80 --link sql1 salesproductapi

## command docker run container with more then 2 ports (http, grpc)
docker run --name salesproductapi -d -p 8087:4999 -p 9090:9090 --link sql1 salesproductapi

## command to create docker image 
docker build --tag salesproductapi .


## connectionString conteiner docker to compose in appsettings.json

  "ConnectionStrings": {
    "myConnection": "Server=dbsqlserver;Database=productApi;User Id=sa;Password=Mau123&&&"
    },


 ## connectionString conteiner docker out of compose in appsettings.json (ip of local machine)

    "ConnectionStrings": {
    "myConnection": "Server=127.0.0.1,1433;Database=productApi;User Id=sa;Password=Mau123&&&"
    },

## connectionString conteiner docker out of compose in appsettings.json 
    "ConnectionStrings": {
    "myConnection": "Server=sql1,1433;Database=productApi;User Id=sa;Password=Mau123&&&"
    },


## connectionString conteiner docker Minikube in appsettings.json 
    "ConnectionStrings": {
    "myConnection": "Server=productdb;Database=productApi;User Id=sa;Password=Mau123&&&"
    },


## link to learn to itereate with sqlserver into container docker    
https://docs.microsoft.com/pt-br/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&pivots=cs1-bash

## Use o comando docker exec -it para iniciar um shell bash interativo dentro do contêiner em execução. No exemplo a seguir, sql1 é o nome especificado pelo parâmetro --name na criação do contêiner.
sudo docker exec -it sql1 "bash"

## Quando estiver dentro do contêiner, conecte-se localmente com a sqlcmd. A sqlcmd não está no caminho por padrão, portanto, você precisará especificar o caminho completo.
 /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<YourNewStrong@Passw0rd>"
  -- OR --
 docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa

 

## create request POST via CURL
 curl -X POST -H "Content-Type: application/json" -d '{"Description": "Product 001", "Amount": 200, "Status": "Active", "Price": "200"}' http://localhost:4999/Product


  curl -X POST -H "Content-Type: application/json" -d '{"Description": "Product 008", "Amount": 600, "Status": "Active", "Price": "600"}' http://salesorder.com/CreateProduct


 

