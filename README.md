# Tours marketplace system designed as a monolith architecture - backend

## About
This repository contains the source code of the project which is the part of the **Software Design** course. The goal of this project is to design and implement a tour management system using a monolithic architecture. The focus is on creating modular components that handle different feature groups, with a clear separation between the core functionalities. Besides that, the project's focus was incorporating Agile methodology and organizing the collaboration of a large team. This repository contains the backend code of the application, and the frontend code can be found [here](https://github.com/RA2020PSW8/tourism-webapp). 
## Feature Overview: Structure & System Design
This is a feature overview from the backend perspective and provides an overview of the structure of the project and some of the main functionalities. More details about the design and look of the application are provided inside the frontend repository of this application.

Tutours provides a network for users to buy tours, track their progress on active tours, track their current location, complete certain challenges and socialize with other users. 
### User Types
The system supports three types of users: 
- Tourist - user that can buy tours, go on tours, track location, join the club, complete the encounters, participate in club fights, rate & report tours
- Guide/Author - user that can create tours, create key points of the tour and encounters related to their tour, create coupons and bundles for their tours
- Admin - user that manages the system
### Modules
The project consists of 5 modules with each having a clear group of features related to it: 
#### Tours
Base module that is responsible for tracking information about the tours and all of the information related to them including: key points of the tour, equipment needed for the tour and objects of interest near the tour route. Besides this, it tracks the locations and progress of the users who started a tour and progressed.
#### Stakeholders
A module that is responsible for saving information about the users and providing login and register functionalities. Besides that, it provides functions for following and chatting between the users, as well as creating clubs. The usage of clubs is expanded inside the Encounters feature, and together with it creates a system for clubs to challenge each other in fights and compete with each other on different criteria. This module is also responsible for handling notifications sent on the user's profiles and email notifications that come with certain features and functionalities.
#### Encounters
A module that includes information about the encounters which may be related to certain tours. Encounters are challenges that can be started by the users and completed for a certain XP amount. XP plays a role in the club fights previously mentioned as well as providing users with certain discounts on tours. Encounters have different types and can be completed by one user or could require multiple users to complete a challenge. To start an encounter, they require the user to be in a certain proximity of that encounter.
#### Payments
A module that handles all of the payments on the system with a virtual coin used on the site and provides each user with a digital wallet that allows them to purchase tours on the site. The module is responsible for tracking orders, order items and providing discounts and/or coupons that users won with certain challenges or by surpassing a certain amount spent on the site.
#### Blog
Community module that provides users with a place to create and share blogs, leave comments and provide information about the tours they were on. 

## Tests

The project includes automatic tests written with `xUnit`, including Unit and Integration tests. Tests are located within corresponding modules and can be run with the Test Explorer inside IDE. It's important to create a test database using the similar steps as mentioned in the [Running project](#running-project) section, parameters about the test database can be found inside the [DbConnectionStringBuilder.cs](\src\BuildingBlocks/Explorer.BuildingBlocks.Tests/BaseTestFactory.cs) file.
## Running project
### Requirements
You must have software from this list installed on your system to be able to run the backend of the project
* DotNetCore 7.0
* PostgreSQL database server

### Running project
After you setup all the required software follow these steps to run the application
1. create a database on your PostgreSQL server. You can use parameters found inside [DbConnectionStringBuilder.cs](\src\BuildingBlocks\Explorer.BuildingBlocks.Infrastructure\Database\DbConnectionStringBuilder.cs) file or if you create a database with different parameters update the linked file
2. run the `tutours-db-backup.sql` script found inside the [/scripts](/scripts) folder. You can do so by using the `/include <filename>` command inside the PSQL Tool
3. enter the Visual Studio or JetBrains Rider IDE, build the project and run. You will most likely be prompted to install the IIS Express SSL certificate, accept it 

## Authors
* [Anastasija Novaković](https://github.com/anastano)
* [Marko Šikanja](https://github.com/jomax01)
* [Branimir Koldan](https://github.com/Koldan001)
* [Anja Kovačević](https://github.com/kovacevicanja)
* [Ljubiša Perić](https://github.com/Ljubisa-Peric)
* [Mihailo Đajić](https://github.com/Mihailo44)
* [Milica Kljajić](https://github.com/miilicakljajic)
* [Milena Marković](https://github.com/MilenaM06)
* [Miljana Marjanović](https://github.com/MiljanaMa)
* [Ognjen Milojević](https://github.com/ognjenm01)
* [Miloš Pisarić](https://github.com/Pisaric)
* [Strahinja Praška](https://github.com/strahinjapraska)
* [Srđan Petronijević](https://github.com/srdjanpetronijevic)
* [Anastasija Radić](https://github.com/anastasijaradic)
* [Nikola Simić](https://github.com/dXellor)
* [Jelena Vujović](https://github.com/zanyaIO)