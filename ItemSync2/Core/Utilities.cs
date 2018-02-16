using System.Xml;
using System;
using System.Security;
using WDBXLib.Definitions.WotLK;

namespace ItemSync2.Core
{
    class Utilities
    {
        /// <summary>
        /// Adds a new element as child under provided node in give xml.
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="innerText"></param>
        /// <param name="comment"></param>
        public static void XmlAddElement(XmlDocument xml, XmlNode parent, string name, string innerText, string comment)
        {
            XmlNode newNode = xml.CreateElement(name);
            if (comment != "" && comment != null)
            {
                XmlComment newComment = xml.CreateComment(name + ": " + comment);
                parent.AppendChild(newComment);
            }
            newNode.InnerText = innerText;
            parent.AppendChild(newNode);
        }

        public static bool AreEqual(Item a, Item b)
        {
            return ((a.ID == b.ID) && (a.ClassID == b.ClassID) && (a.SubclassID == b.SubclassID) && (a.Sound_override_subclassid == b.Sound_override_subclassid)
                && (a.Material == b.Material) && (a.DisplayInfoID == b.DisplayInfoID) && (a.InventoryType == b.InventoryType) && (a.SheatheType == b.SheatheType));
        }

        #region Nothing to see here.
        static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("Wizzard on a Lizzard in a Blizzard Entertainment");

        public static string EncryptString(SecureString input)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DecryptString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }
        #endregion
    }
}
