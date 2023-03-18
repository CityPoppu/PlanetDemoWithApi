# Planet Demo

Planet Demo is a Blazor Web Assembly application, created by Dean Brown. It uses C# / Blazor & SQL to create a website where information about planets can be accessed & queried. 

# How to Run

The Planet Demo can be run locally by downloading the solution from from PlanetDemoWithApi github repository. The project can be inspected & run from inside visual studio with .net 6.0 & above. Ensure that PlanetDemoWithApi.Server is set as a startup project. 

# Design

Data Source: Planet information is stored in a SQL database, hosted remotely on a databse provider, planetscale.com. The database I created can be connected to from a read only connection using Server=aws-eu-west-2.connect.psdb.cloud;Database=planetdemodb;user=76gl80thw2v4qkftka1n;password=pscale_pw_35FocMjFUZhVCDwvYHWjVqBJZW4MR3s5SJxh0YYds5y;SslMode=VerifyFull; 

The scripts used to create the database table & insert the rows are found under the Database folder in the PlanetDemoWithApi.Server project. 

Website: The Web App is split into multiple components: a client side, server side & shared component projects. The Client side handles user interaction with the user inteface & display layer logic. The Server side hosts the API & manages connections to the database. The shared project contains models of objects that used by both other layers. 

Swagger API: The planet data is exposed via a REST API, which has a swagger UI. This allows for the endpoints to be visible & queried independantly of the frontend code. The available calls are: Get (Gets list of all planets), GetPlanetByName (Attempts to get a planet based on its name), GetPlanetsFromProperty (Provide either number of moons or classification to retrieve a list of possible matches)

Tests: TestProject contains Unit tests for the PlanetController, testing HTTP response codes + using Moq & the PlanetRepository, testing the actual data & retrieval process in the database. 

# Pages

Home - Welcome screen & description 
Planet List - (Requested solution) Displays a list of Planets with information that is rendered upon click
Swagger API - Displays the API endpoints with documentation & can be queried via a browser
Planet Search - (Additional) Allows for a user to search for a Planet by name or by Number of Moons + Classification

# Deployed version 

A version of the website has been hosted on www.citypoppu.com - I was unable to host the server side app for a low cost, so created a stripped down version that just utilises the client side user experience with mocked data. I already owned the domain citypoppu.com, so reused it here temporarily! 

# Credits: 

Images are from nineplanets.org & Wikipedia.com 