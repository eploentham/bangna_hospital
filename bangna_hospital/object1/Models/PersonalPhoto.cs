using System.IO;
using System;
using System.Drawing;

namespace bangna_hospital.Models
{
    public class PersonalPhoto : Personal
    {
        public string Photo { get; set; }

        public PersonalPhoto(Personal personal)
        {
            CitizenID = personal.CitizenID;
            ThaiPersonalInfo = personal.ThaiPersonalInfo;
            EnglishPersonalInfo = personal.EnglishPersonalInfo;
            DateOfBirth = personal.DateOfBirth;
            Sex = personal.Sex;
            AddressInfo = personal.AddressInfo;
            IssueDate = personal.IssueDate;
            ExpireDate = personal.ExpireDate;
            Issuer = personal.Issuer;
            dobDD = personal.dobDD;
            dobMM = personal.dobMM;
            dobYYYY = personal.dobYYYY;
        }
        public Image GetPhotoAsImage()
        {
            if (string.IsNullOrEmpty(Photo))
            {
                return null;
            }

            // Remove the data:image/jpeg;base64, prefix if it exists
            var base64Data = Photo.Contains(",") ? Photo.Split(',')[1] : Photo;

            byte[] imageBytes = Convert.FromBase64String(base64Data);
            using (var ms = new MemoryStream(imageBytes))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
