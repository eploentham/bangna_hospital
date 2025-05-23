﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public enum InstructionCode:byte
    {/// <summary>Erase binary</summary>
        [Description("ERASE BINARY")]
        EraseBinary = 0x0E,

        /// <summary>Verify</summary>
        [Description("VERIFY")]
        Verify = 0x20,

        /// <summary>Manage channel</summary>
        [Description("MANAGE CHANNEL")]
        ManageChannel = 0x70,

        /// <summary>External authenticate</summary>
        [Description("EXTERNAL AUTHENTICATE")]
        ExternalAuthenticate = 0x82,

        /// <summary>Get challenge</summary>
        [Description("GET CHALLENGE")]
        GetChallenge = 0x84,

        /// <summary>Internal authenticate</summary>
        [Description("INTERNAL AUTHENTICATE")]
        InternalAuthenticate = 0x86,

        /// <summary>Select file</summary>
        [Description("SELECT FILE")]
        SelectFile = 0xA4,

        /// <summary>Read binary</summary>
        [Description("READ BINARY")]
        ReadBinary = 0xB0,

        /// <summary>Read record(s)</summary>
        [Description("READ RECORD(S)")]
        ReadRecord = 0xB2,

        /// <summary>Get response</summary>
        [Description("GET RESPONSE")]
        GetResponse = 0xC0,

        /// <summary>Envelope</summary>
        [Description("ENVELOPE")]
        Envelope = 0xC2,

        /// <summary>Get data</summary>
        [Description("GET DATA")]
        GetData = 0xCA,

        /// <summary>Write binary</summary>
        [Description("WRITE BINARY")]
        WriteBinary = 0xD0,

        /// <summary>Write record</summary>
        [Description("WRITE RECORD")]
        WriteRecord = 0xD2,

        /// <summary>Update binary</summary>
        [Description("UPDATE BINARY")]
        UpdateBinary = 0xD6,

        /// <summary>Put data</summary>
        [Description("PUT DATA")]
        PutData = 0xDA,

        /// <summary>Update data</summary>
        [Description("UPDATE DATA")]
        UpdateData = 0xDC,

        /// <summary>Append record</summary>
        [Description("APPEND RECORD")]
        AppendRecord = 0xE2,

        /// <summary>Decrement value</summary>
        [Description("DECREMENT")]
        Decrement = 0xD8,

        /// <summary>Increment value</summary>
        [Description("INCREMENT")]
        Increment = 0xD4
    }
}
