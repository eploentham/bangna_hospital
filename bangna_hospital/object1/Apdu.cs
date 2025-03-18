using PCSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public abstract class Apdu
    {/// <summary>The currently used ISO case.</summary>
        public IsoCase Case { get; protected set; }

        /// <summary>The currently used protocol.</summary>
        public SCardProtocol Protocol { get; protected set; }

        /// <summary>Converts the APDU structure to a transmittable byte array.</summary>
        /// <returns>A byte array containing the APDU parameters and data in the correct order.</returns>
        public abstract byte[] ToArray();

        /// <summary>Indicates if the APDU is valid.</summary>
        /// <value><see langword="true" /> if the APDU is valid.</value>
        public abstract bool IsValid { get; }

        /// <summary>Converts the APDU structure to a transmittable byte array.</summary>
        /// <param name="apdu">The APDU.</param>
        /// <returns>The supplied APDU as byte array.</returns>
        public static explicit operator byte[](Apdu apdu)
        {
            return apdu.ToArray();
        }
        public byte[] citizenID = {0x80, 0xb0, 0x00, 0x04, 0x02, 0x00, 0x0d};
        public byte[] fullNameTH = { 0x80, 0xb0, 0x00, 0x11, 0x02, 0x00, 0x64 };
        public byte[] fullNameEN = { 0x80, 0xb0, 0x00, 0x75, 0x02, 0x00, 0x64 };
        public byte[] dateOfBirth = {0x80, 0xb0, 0x00, 0xd9, 0x02, 0x00, 0x08};
        public byte[] gender = { 0x80, 0xb0, 0x00, 0xe1, 0x02, 0x00, 0x01};
        public byte[] cardIssuer = { 0x80, 0xb0, 0x00, 0xf6, 0x02, 0x00, 0x64};
        public byte[] issueDate = { 0x80, 0xb0, 0x01, 0x67, 0x02, 0x00, 0x08};
        public byte[] expireDate = { 0x80, 0xb0, 0x01, 0x6f, 0x02, 0x00, 0x08};
        public byte[] address = { 0x80, 0xb0, 0x15, 0x79, 0x02, 0x00, 0x64};
        
    }
}
