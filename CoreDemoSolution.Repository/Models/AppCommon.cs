namespace CoreDemoSolution.Repository.Models
{

    using System.IO;
    public class AppCommon
    {
        private static readonly string currDirectory = Directory.GetCurrentDirectory();

        private const string applicationFilesFolderName = "ApplicationFiles";
        private const string wwwrootFolder = "wwwroot";
        private const string assetsFolder = "assets";
        private const string emailTemplatesFolderName = "EmailTemplates";
        private const string imagesFolderName = "GiftImages";
        private const string imageUserFolderName = "UserImages";
        private const string imageProfileFolderName = "ProfileImages";
        private const string imageCoverFolderName = "CoverImages";
        private const string invoiceFolderName = "Invoice";

        //="EmailinvoiceTemplate"
        public string UserFilesRequestName { get { return imagesFolderName; } }
        public string ImagesFolderPath
        {
            get
            {
                var directoryPath = Path.Combine(currDirectory, wwwrootFolder, assetsFolder, imagesFolderName);
                if (Directory.Exists(directoryPath))
                    return directoryPath;
                else
                    return "";
            }
        }

        public string ProfileImageFolderPath
        {
            get
            {
                var directoryPath = Path.Combine(currDirectory, wwwrootFolder, assetsFolder, imageUserFolderName, imageProfileFolderName);
                if (Directory.Exists(directoryPath))
                    return directoryPath;
                else
                    return "";
            }
        }

        public string CoverImageFolderPath
        {
            get
            {
                var directoryPath = Path.Combine(currDirectory, wwwrootFolder, assetsFolder, imageUserFolderName, imageCoverFolderName);
                if (Directory.Exists(directoryPath))
                    return directoryPath;
                else
                    return "";
            }
        }

        public string EmailTemplateFilePath
        {
            get
            {
                var filePath = Path.Combine(currDirectory, applicationFilesFolderName, emailTemplatesFolderName, "EmailTemplate.html");
                if (File.Exists(filePath))
                    return filePath;
                else
                    return "";
            }
        }

        public string EmailNotificationFilePath
        {
            get
            {
                var filePath = Path.Combine(currDirectory, applicationFilesFolderName, emailTemplatesFolderName, "EmailNotification.html");
                if (File.Exists(filePath))
                    return filePath;
                else
                    return "";
            }
        }

        public string EmaiInvoiceFilePath
        {
            get
            {
                var filePath = Path.Combine(currDirectory, applicationFilesFolderName, emailTemplatesFolderName, "EmailinvoiceTemplate.html");
                if (File.Exists(filePath))
                    return filePath;
                else
                    return "";
            }
        }

        public string EmailContactUsPath
        {
            get
            {
                var filePath = Path.Combine(currDirectory, applicationFilesFolderName, emailTemplatesFolderName, "EmailContactUs.html");
                if (File.Exists(filePath))
                    return filePath;
                else
                    return "";
            }
        }

        public string EmailFundNotificaionPath
        {
            get
            {
                var filePath = Path.Combine(currDirectory, applicationFilesFolderName, emailTemplatesFolderName, "EmailFundNotification.html");
                if (File.Exists(filePath))
                    return filePath;
                else
                    return "";
            }
        }

        public string InvoicePdfPath
        {
            get
            {
                var directoryPath = Path.Combine(currDirectory, wwwrootFolder, invoiceFolderName);
                if (Directory.Exists(directoryPath))
                {
                    return directoryPath;
                }
                else
                {
                    Directory.CreateDirectory(directoryPath);
                    if (Directory.Exists(directoryPath))
                    {
                        return directoryPath;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }


    }
}

