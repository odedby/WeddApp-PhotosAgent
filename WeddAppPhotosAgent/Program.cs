using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using Google.GData.Photos;

namespace WeddAppPhotosAgent
{
    class Program
    {
        private static TimeSpan ts = new TimeSpan(0, 0, 2);        //2 seconds sleep time;
        private static int imgCounter = 0;

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    string[] filePaths = Directory.GetFiles(@"C:\WeddApp\upload");
                    DateTime current = new DateTime();
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        PicasaService myPicasa = new PicasaService("Vikash-Test-Picasa");
                        //Passing GMail credentials(EmailID and Password)
                        myPicasa.setUserCredentials("hagit.oded@gmail.com", "0542686874");

                        //User ID and AlbumID has been used to create new URL
                        Uri newURI = new Uri(PicasaQuery.CreatePicasaUri("hagit.oded", "5780002529047522017"));

                        //Image path which we are uploading
                        current = DateTime.Now;
                        System.IO.FileInfo newFile = new System.IO.FileInfo(filePaths[i]);
                        System.IO.FileStream neFStream = newFile.OpenRead();
                        PicasaEntry newEntry = (PicasaEntry)myPicasa.Insert(newURI, neFStream, "Image/jpeg", Convert.ToString(current));
                        Console.Out.WriteLine("Image " + newFile.Name + " uploaded");
                        neFStream.Close();
                        File.Delete(filePaths[i]);
                    }
                    Thread.Sleep(ts);
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex.Message);
                    System.Diagnostics.EventLog.WriteEntry("WeddApp", ex.Message, System.Diagnostics.EventLogEntryType.Error, 626);
                }
            }
        }
    }
}
