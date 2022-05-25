#!/bin/bash
########################################################

## Shell Script to Build Docker Image and run.   

########################################################


result=$( docker images -q salesproductapi01 )
if [[ -n "$result" ]]; then
echo "image exists"
 docker rmi -f salesproductapi
else
echo "No such image"
fi

echo "build the docker image"
echo "built docker images and proceeding to delete existing container"

result=$( docker ps -q -f name=salesproductapi )
if [[ $? -eq 0 ]]; then
echo "Container exists"
 docker container rm -f salesproductapi
echo "Deleted the existing docker container"
else
echo "No such container"
fi

cp -a /home/francisco/estudos/azuredevops/myagent/_work/8/s/.  /home/francisco/estudos/azuredevops/myagent/_work/r7/a/

docker build -t salesproductapi .

echo "built docker images and proceeding to delete existing container"
echo "Deploying the updated container"

docker run --name salesproductapi -d -p 8087:4999 -p 9090:9090 --link sql1 salesproductapi

echo "Deploying the container"