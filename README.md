# SalesProductApi asp.net core 3.1 

#conecta no container com o mysql.
#comando para rodar o container do mysql.
docker run -e MYSQL_ROOT_HOST=% -e MYSQL_ROOT_PASSWORD=123 --name mysqlserver -d -p=3306:3306 mysql/mysql-server:5.7.31

#comando para enviar uma requisição POST via CURL
curl -k -d '{"Amount": 200,"Description": "Product 001","Status": "Active","Price": "50"}' -H "Content-Type: application/json" -X POST https://localhost:5001/Product


#comando para acessar o mysql dentro do container
docker exec -it mysqlserver mysql -u root -p
