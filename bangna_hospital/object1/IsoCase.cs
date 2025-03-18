using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public enum IsoCase
    {
        Case1 = 0,

        /// <summary>No command data. Expected response data.</summary>
        /// <remarks>
        ///     <list type="bullet">
        ///         <item><description>Lc is valued to 0.</description></item>
        ///         <item><description>Le is valued from 1 to 256.</description></item>
        ///         <item><description>No data byte is present.</description></item>
        ///     </list>
        /// </remarks>
        Case2Short = 1,

        /// <summary>Command data. No response data.</summary>
        /// <remarks>
        ///     <list type="bullet">
        ///         <item><description>Lc is valued from 1 to 255.</description></item>
        ///         <item><description>Le is valued to 0.</description></item>
        ///         <item><description>Lc data bytes are present.</description></item>
        ///     </list>
        /// </remarks>
        Case3Short = 2,

        /// <summary>Command data. Expected response data.</summary>
        /// <remarks>
        ///     <list type="bullet">
        ///         <item><description>Lc is valued from 1 to 255.</description></item>
        ///         <item><description>Le is valued from 1 to 256.</description></item>
        ///         <item><description>Lc data bytes are present.</description></item>
        ///     </list>
        /// </remarks>
        Case4Short = 3,

        /// <summary>No command data. Expected response data.</summary>
        /// <remarks>
        ///     <list type="bullet">
        ///         <item><description>Lc is valued to 0.</description></item>
        ///         <item><description>Le is valued from 1 to 65536.</description></item>
        ///         <item><description>No data byte is present.</description></item>
        ///     </list>
        /// </remarks>
        Case2Extended = 4,

        /// <summary>Command data. No response data.</summary>
        /// <remarks>
        ///     <list type="bullet">
        ///         <item><description>Lc is valued from 1 to 65536.</description></item>
        ///         <item><description>Le is valued to 0.</description></item>
        ///         <item><description>Lc data bytes are present.</description></item>
        ///     </list>
        /// </remarks>
        Case3Extended = 5,

        /// <summary>Command data. Expected response data.</summary>
        /// <remarks>
        ///     <list type="bullet">
        ///         <item><description>Lc is valued from 1 to 65535.</description></item>
        ///         <item><description>Le is valued from 1 to 65536.</description></item>
        ///         <item><description>Lc data bytes are present.</description></item>
        ///     </list>
        /// </remarks>
        Case4Extended = 6
    }
}
