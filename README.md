# MusicApi
Contains group API project for SD125

What the project does:
- Our project is a music-oriented database that is made up of three tables: record labels, artists, and songs. This application will allow music fans to view, add, update, or delete information on their favorite artists, songs, and the record labels that connect them.
- The inspiration from this project came from real world experiences of dealing with the music industry and the people that are a part of it.
- All of the tables are connected to each other via foreign keys. The Label table has a one to many relationship with the Artist and Song table, while the Artist and Song table have a many to many relationship.


- Methods:
  - There are CRUD methods for all three tables
  - Update Methods:
    -All of the update methods include logic that makes it so not all properties have to be updated at a time
    -This allows users to pick and choose what properties they wish to update at a time
  - GetByName methods for all three
  - AssignSongsToArtist method
    -Adds existing artist entities to the List<ArtistEntity> property in the SongEntity
    -In terms of the database, it adds data to the junction table between Song and Artist table
  - GetSongsByArtist method
    -returns only the list of Songs attached to a particular artist 
    -uses artistName to find the list - more user friendly than id
  - GetArtistsByLabel method
    -returns only the list of Artists attached to a particular label
    -uses labelName to find the list - more user frienly than id
    
 -External Planning Document Links:
  - Google Docs https://docs.google.com/document/d/10TVnMLZHOS5OPN4YLgsrfJB0FiwB-M-NSIZdwd18Lis/edit#
  - Trello Board: https://trello.com/b/azlNr0bm/music-api
  - Diagram: https://dbdiagram.io/d/6201452785022f4ee5507333
  
  -Resources Links:
    - Many To Many Relationship Article: https://henriquesd.medium.com/entity-framework-core-5-0-many-to-many-relationships-52c6c8b07b6e
    
    - URL Encoding: http://www.blooberry.com/indexdot/html/topics/urlencoding.htm
    
    
