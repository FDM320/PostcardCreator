using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using PostcardCreator.Properties;

namespace PostcardCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string fileName;
        public string postcardFN;
        public string newPostcardFN;
        public string emailSender;
        public string emailReceiver;
        public string emailPassword;
        

        // On button click, lets you the user choose an image to load. After choosing an image, the createPostcardButton becomes visible.
        private void loadButtonClick(object sender, RoutedEventArgs e)
        {
            // Open a file dialog that lets the user select a picture from the allowed types.
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Title = "Select a picture";
            openFD.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "PNG (*.png)|*.png";
            if (openFD.ShowDialog() == true)
            {
                // Set the image to display in the app.
                userPhoto.Source = new BitmapImage(new Uri(openFD.FileName));
                fileName = openFD.FileName;
            }

            // Make the createPostcardButton visible.
            createPostcardButton.Visibility = 0;
        }

        // On button click, lets you choose an image to overlap over the first image. After choosing an image, the saveButton becomes visible.
        private void createPostcardButtonClick(object sender, RoutedEventArgs e)
        {
            // Open a file dialog that lets the user select a picture from the allowed types.
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Title = "Select a picture";
            openFD.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "PNG (*.png)|*.png";
            if (openFD.ShowDialog() == true)
            {
                // Set the image to display in the app.
                postcardEffect.Source = new BitmapImage(new Uri(openFD.FileName));
                postcardFN = openFD.FileName;
            }

            // Set the image to visible.
            postcardEffect.Visibility = 0;
            // Make the saveButton visible.
            saveButton.Visibility = 0;
        }

        // On button click has you choose a location and a name so that you can save your new image as a .bmp. After saving, the credentialsButton becomes visible.
        // The image is made by placing the second image over the first image and merging them together.
        private void saveButtonClick(object sender, RoutedEventArgs e)
        {
            // Opens a save file dialog that lets the user choose a location and a name to save the file.
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Image Files(*.BMP)|*.BMP";
            if (saveDialog.ShowDialog().Value == true)
            {
                // Load the images.
                BitmapFrame oldPhoto = BitmapDecoder.Create(new Uri(fileName), BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();
                BitmapFrame postcardEffect = BitmapDecoder.Create(new Uri(postcardFN), BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();

                // Get the size of the images.
                int oldPhotoWidth = oldPhoto.PixelWidth;
                int oldPhotoHeight = oldPhoto.PixelHeight;
                int postcardEffectWidth = postcardEffect.PixelWidth;
                int postcardEffectHeight = postcardEffect.PixelHeight;

                // Draw the images into a DrawingVisual component.
                DrawingVisual drawingVisual = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    drawingContext.DrawImage(oldPhoto, new Rect(0, 0, oldPhotoWidth, oldPhotoHeight));
                    drawingContext.DrawImage(postcardEffect, new Rect(oldPhotoWidth * .25, oldPhotoHeight * .75, postcardEffectWidth, postcardEffectHeight));
                }

                // Converts the DrawingVisual into a Bitmap.
                RenderTargetBitmap bitmapRender = new RenderTargetBitmap(oldPhotoWidth, oldPhotoHeight, 96, 96, PixelFormats.Pbgra32);
                bitmapRender.Render(drawingVisual);

                // Creates a PngBitmapEncoder and adds the Bitmap to the frames of the encoder.
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapRender));

                // Save the image into a file using the encoder.
                using (Stream stream = File.Open(saveDialog.FileName, FileMode.OpenOrCreate))
                    encoder.Save(stream);

                // Make the credentialsButton visible/
                credentialsButton.Visibility = 0;
            }


        }

        // On button click, has you type in your gmail address, password, and the receiver of your postcard.
        // Makes the previously invisible credentials box section visible.
        private void credentialsButtonClick(object sender, RoutedEventArgs e)
        {
            credentialsBackground.Visibility = 0;
            credentialsLabel.Visibility = 0;
            usernameLabel.Visibility = 0;
            passwordLabel.Visibility = 0;
            receiverLabel.Visibility = 0;
            usernameTextBox.Visibility = 0;
            passwordBox.Visibility = 0;
            receiverTextBox.Visibility = 0;
            enterCredentialsButton.Visibility = 0;
        }

        // On button click, gets the information you typed into the prompts. Makes the credentials box section invisible again.
        private void enterCredentialsButtonClick(object sender, RoutedEventArgs e)
        {
            if (usernameTextBox.Text == "" || passwordBox.Password == "" || receiverTextBox.Text == "")
            {
                MessageBox.Show("You must fill out all the fields.");
            }
            else
            {
                // Get the information from the textboxes.
                emailSender = usernameTextBox.Text;
                emailPassword = passwordBox.Password;
                emailReceiver = receiverTextBox.Text;

                // Make the credentials box section invisible again.
                credentialsBackground.Visibility = Visibility.Collapsed;
                credentialsLabel.Visibility = Visibility.Collapsed;
                usernameLabel.Visibility = Visibility.Collapsed;
                passwordLabel.Visibility = Visibility.Collapsed;
                receiverLabel.Visibility = Visibility.Collapsed;
                usernameTextBox.Visibility = Visibility.Collapsed;
                passwordBox.Visibility = Visibility.Collapsed;
                receiverTextBox.Visibility = Visibility.Collapsed;
                enterCredentialsButton.Visibility = Visibility.Collapsed;

                // Makes the sendButton visible.
                sendButton.Visibility = 0;
            }
        }

        // On button click, uses the provided credentials to send an email with the postcard you made as an attachment.
        private void sendButtonClick(object sender, EventArgs e)
        {
            try
            {

                MailMessage email = new MailMessage();
                                
                email.From = new MailAddress(emailSender);
                email.To.Add(emailReceiver);
                email.Subject = "Custom Postcard!";
                email.Body = "Here's your custom postcard, made by yours truly!";

                OpenFileDialog openFD = new OpenFileDialog();
                openFD.Title = "Select a picture";
                openFD.Filter = "Image Files(*.BMP)|*.BMP";

                if (openFD.ShowDialog() == true)
                {
                    BitmapImage newPostcard = new BitmapImage(new Uri(openFD.FileName));
                    newPostcardFN = openFD.FileName;
                }

                Attachment attachment = new Attachment(newPostcardFN);
                email.Attachments.Add(attachment);

                // SMTP Client.
                SmtpClient SMTP = new SmtpClient("smtp.gmail.com");
                SMTP.Port = 587;
                SMTP.UseDefaultCredentials = false;
                SMTP.Credentials = new NetworkCredential(emailSender, emailPassword);
                SMTP.EnableSsl = true;
                SMTP.Send(email);

                MessageBox.Show("Postcard sent to " + emailReceiver + "!");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Your username / password combination is incorrect.");
            }

        }

    }

}