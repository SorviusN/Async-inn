# Async-inn

### Name: Jona Brown
### Date: 07/26/2021
## Summary
The Async Inn is an API that represents all Hotels in the Async name, including the rooms and the amenities for each for each of the rooms. These are all turned into JSON data, in which you are able to PUT, POST, GET and DELETE new hotels, rooms and amenities inside of the API.

![ERD](./ERD.png)

Tables -  
- Async Hotel - Contains the location info, amount of rooms and name of hotel.
- Location - 1 to 1 relationship with Async hotel, contains all of the locations that the Async hotel exists.
- Rooms - 1 to many relationship with Async hotel, contains the different types of rooms.
- Suite - 1 to 1 with relationship rooms, a room may be an empire suite, which has a location of Manhatttan (from location)
- Amenities - 1 to many relationship with suites, as there would be a lot of different amenities per room. 
