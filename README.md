# Async-inn

### Name: Jona Brown
### Date: 07/26/2021
## Summary
The Async Inn is an API that represents all Hotels in the Async name, including the rooms and the amenities for each for each of the rooms. These are all turned into JSON data, in which you are able to PUT, POST, GET and DELETE new hotels, rooms and amenities inside of the API.  
Additionally, we have individual controllers that handle CRUD requests by using their appropriate service worker. This wiring makes it possible to perform a variety of requests, along with different types of requests for each controller item (hotels, rooms, amenities and such)

![ERD](./ERD.png)

Tables -  
- Async Hotel - Contains the location info, amount of rooms and name of hotel.
- Location - 1 to 1 relationship with Async hotel, contains all of the locations that the Async hotel exists.
- Rooms - 1 to many relationship with Async hotel, contains the different types of rooms.
- Suite - 1 to 1 with relationship rooms, a room may be an empire suite, which has a location of Manhatttan (from location)
- Amenities - 1 to many relationship with suites, as there would be a lot of different amenities per room. 

## Data Transfer Objects
-DTOs give you the ability to hide data from the client that they would not want/need. Think IDs and passwords for a database of usernames.  
Additionally, New DTO classes give you the ability to write a simple object (A Hotel with a name, phone number and address) that would then parse out all of 
the info into a regular non DTO class and store it into the database. This info is then returned to the client in the form of a DTO.

## User Role Information
This section will explain the process of Login and Registration via the API. The main parts are:  
- Application User (Model) - Will hold user name and password information. Implements the IdentityUser interface which gives it all of the necessary tasks to handle a user inside of a database.  
- Users Controller - Handles the routes for the Application user. Uses Post for both login and register. Injects IUser interface methods to be used with the IdentityUserService.  
- Identity User Service - Acting as the one "Making the burger", the service receives the data given by the controller and performs all of the necessary operations with the database. In the case of the user service registration process it takes the data creates a new application user model from it. It then returns a new UserDTO if all went well. If there is an error with any of the fields, it is displayed as needed.  
- IUser (Interface) - holds the login and registration function. This is implemented by the Identity User Service.  
- DTOs - These are packaged versions of the Login, User and RegisterUser classes which give some simple fields to fill in on the API side. These DTOs are used for both input and output this time around, however this is not considered best practice in most applications.  
- NEWDTO - An unofficial term for the INCOMING dto vs the outgoing dto. In a perfect world they will be separate things - for instance when someone creates an account as a user they will need to enter a username, password but our program will generate a key for them along with their other info into the SQL database.
