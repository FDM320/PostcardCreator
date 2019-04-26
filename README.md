# PostcardCreator
------------------------------------------
# Important!
------------------------------------------
The very first thing that you need to do in order to use this application is go to your Gmail account and let less secure apps access your account.
You can do this by following these steps:
1. Go to your Google Account.
2. On the left navigation panel, click Security.
3. On the bottom of the page, in the Less secure app access panel, click Turn on access.
------------------------------------------

Why this assignment? <br>
I had originally started developing a desktop Twitter app, but I struggled to get the standard search to use more than one search query at a time. <br>
Even the examples provided by the Twitter API documentation weren't working for me. I'm not sure if maybe I set my developer account up incorrectly. <br>
In any case, I decided that the Twitter API was too frustrating, and I don't use Twitter anyways, so I changed to the postcard creating app. <br>
I wanted to add some of the bonus features, but unfortunately I ran out of time and had to exclude them. <br><br>
Steps to run:
In order to run the application, you have to use a Gmail account. I wanted to add support for other mail services, but I ran out of time. <br>
You must set up your Gmail account to let less secure apps access your account. I wanted to get authorization through Gmail's API, but I didn't see an easy way to do this without knowing the user's account credentials beforehand. <br>
After you've made sure Gmail won't block the app, all you have to do is run it in Visual Studio. The app itself is pretty self-explanatory. <br>
When you initially run the app, a main window will open. The main window will be mostly blank, and you'll see a button that says "Load Photo". I have included the weddingPhoto.jpg file that I used to test the app during this step. <br>
When you click the "Load Photo" button, Windows will prompt you to open an image file. The application supports .jpg, .jpeg, and .png. <br>
Once you choose your image, it will be loaded into the main window and the "Create Postcard" button will appear. <br>
When you click the "Create Postcard" button, Windows will prompt you to open another image file. The application supports .jpg, .jpeg, and .png. I have included the postcard.png file that I used to test the app during this step. <br>
Once you choose your image, it will be loaded into the main window on top of the first image and the "Save Photo" button will appear. <br>
When you click the "Save Photo" button, Windows will prompt you to choose a location and a file name in order to save the file. It will save as a .bmp. <br>
Once you properly save the image, the "Credentials" button will appear. <br>
When you click the "Credentials" button, all of the previously invisible credential labels and textboxes will appear. <br>
At this point, use the forms to fill in your Gmail address, your Gmail password, and the email address of the person you want to receive the email. <br>
Clicking the "Enter" button will do some basic error checking. If any of the fields are left blank, a messagebox will pop up and tell you. <br>
Once you have successfully clicked the "Enter" button, all of the credential labels and textboxes will become invisible, and the information you typed in will be saved. The "Send Email" button will appear. <br>
When you click the "Send Email" button, Windows will prompt you to choose an image file. The application supports .bmp. Choose the image that you saved using the app a few steps ago. <br>
After you've chosen the image, the application will use the credentials you provided to send an email including the image you chose as an attachment. The subject and body will be filled in automatically. <br> 
If the message was sent successfully, a messagebox will pop up to confirm that the email was sent. If the message was not sent successfully, a messagebox will pop up and tell you that your username / password info was incorrect. <br> <br>

OS and libraries: <br>
I used Microsoft Windows 10 Pro, version 10.0.17763 build 17763. I didn't use any additional libraries. <br><br>

Design decisions: <br>
I tried to make the app function as simply as possible. The buttons reveal themselves in the order that you're supposed to use them. That was my version of providing on-screen instructions. <br>
If I had had more time, I would've liked to have made the app look more visually appealing. It also would have been nice to be able to use any email account, rather than just Gmail. <br><br>

External libraries: <br>
None. <br><br>

Tools / libraries: <br>
I used Visual Studio Community 2019 version 16.0.2 with the .NET Framework 4.7.2.