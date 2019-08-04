# demo-docker

This is a demonstration docker built in ASP.net. It registers a user on the skidata portal and relays it back to the origin request using the endpoint:
	https://developer.skidata-loyalty.com//user/82/v1/user 
	

To run the app: clone the repository, navigate to root with docker installed and run - 

	docker build -f mvcdocker/Dockerfile .

Once the image is built, run it on port 5010 with port 80 exposed using:

	docker run -p 5010:80 <image container name>

If you can't find the image name you can get it using:

	docker images

Navigate to http://0.0.0.0:5010 to make sure the docker is up and running.


# Code Notes

Controllers/Submit.cs-  handles the networking for the app and most of the logic.

Startup.cs - handles Cors logic

Dockerfile - dockerizes app
