# Video_Monitoring_Back-end
A Backend solution for registering servers and their respective videos. This solution is used for video monitoring.

## Setting up the environment
To run the API or perform automated tests, it is necessary to prepare the environment.
In the main folder of the repository there is a Dockerfile that contains instructions to generate an image with MySQL, and to load an SQL script.

To perform the build from the Dockerfile, execute the command below in your terminal in the directory where the Dockerfile is located:

```bash
docker build --pull --rm -f "Dockerfile" -t mysql_setup:latest
```

This will create an image with the name "mysql_setup". With the image ready, execute the command below to generate a docker container:

```bash
docker run -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=RootPassword -e MYSQL_DATABASE=Video_Monitoring -e MYSQL_USER=MainUser -e MYSQL_PASSWORD=MainPassword -e MYSQL_ROOT_HOST=% mysql_setup
```
Note that the container access port and consequently MySQL is set to 3306, if you want to change the port you must pass it in the command above and also change the application's connection string in the appsettings.json file and appsettings.Development.json file, in the ConnectionStrings section, informing the new port.

## Running the API
In the main directory of the solution, execute the command below:
```bash
dotnet build
```
Right after, access the Backend.WebApi directory:
```bash
cd Backend.WebApi
```
and execute:
```bash
dotnet run
```
The API will be by default listening on port 5000 or 5001 from localhost. Open your browser and access one of the urls below:

https://localhost:5001/swagger/index.html
http://localhost:5000/swagger/index.html

You will have an interface for testing the API, as well as information about schemas and available methods. You can find more information about the API by accessing the README file in the Backend.WebApi folder.

## Running Tests

Automated tests have been developed to ensure software quality. To run the tests, type the command below in the main directory of the solution:
```bash
dotnet test
```
The test results will be displayed on your terminal.

##  Resources used in the development process
* .NET 5.0
* ASP.NET
* C#*
* Asynchronous Programming
* OOP
* DDD
* Dependency Injection  
* Restfull
* AutoMapper
* FluentValidator  
* Dapper
* SQL
* MySQL
* Docker
* Git
* XUnit (automated testing)
* NUnit (automated testing)

## IDEs and Editors
This project was developed using:
* JetBrains Rider IDE 2020.3
* Visual Studio Code 


