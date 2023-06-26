# Newew Better Project
6.26.2023: I have a new project called Blazor Gallery:<br>
https://github.com/DataJuggler/BlazorGallery

Blazor Gallery is also a live site:<br>
https://blazorgallery.com 

Video for Blazor Gallery:<br>
https://youtu.be/HAMgeaCuvHY 

# BlazorImageGallery
This is a sample project I am building to demonstrate the BlazorFileUpload component, BlazorStyled, DataTier.Net and others.

To run this sample. 

1. Create a SQL Server database named BlazorImageGallery
2. Execute the SQL script located in the SQL folder of this repo
3. Build a connection string to the new database
Tip: DataTier.Net has a really cool project included with it called Connection String Builder:
https://github.com/DataJuggler/DataTier.Net

4. Create a System Environment Variable named BlazorImageGallery
5. Set the value of the System Environment Variable to your new Connection String
6. Run Blazor Image Gallery project, you should be able to Sign Up a new user if it is working. 

The Sign Up component expects profile pictures to be 256 x 256. I included about 14 Avatars in the wwwroot/Images/Avatars folder.

The UploadImages shown in the video above were created using Random Art:
https://github.com/DataJuggler/RandomArt

I am building a blog post for a full tutorial for this project, but it is a work in progress right now:
https://datajugglerblazor.blogspot.com/2020/02/building-blazor-image-gallery-complete.html
