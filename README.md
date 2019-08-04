# demo-docker

This is a demonstration docker built in asp.net that sends a POST request to https://developer.skidata-loyalty.com//user/82/v1/user to register a user and returns a success or failure based on the response.

To run the app - clone the repository - navigate to it in terminal with docker installed and run - 

docker build -f mvcdocker/Dockerfile .

once the image is built run the image on port 5010 with port 80 exposed using the command. You can get the name of the image using docker images:

docker run -p 5010:80 <image container name>


Navigate to http://0.0.0.0:5010 to make sure the docker is up and running.