using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography; //

namespace AES
{
    public class aesSifrecoz
    {
        private const string AES_IV = @"!&+QWSDF!123126+";
        private string aesAnahtar = @"QQsaw!257()%%ert";
        AesCryptoServiceProvider aesSaglayici = new AesCryptoServiceProvider();
        public string AESsifrele(string metin)
        {
           
            aesSaglayici.BlockSize = 128;
            aesSaglayici.KeySize = 128;

            aesSaglayici.IV = Encoding.UTF8.GetBytes(AES_IV);
            aesSaglayici.Key = Encoding.UTF8.GetBytes(aesAnahtar);
            aesSaglayici.Mode = CipherMode.CBC;
            aesSaglayici.Padding = PaddingMode.PKCS7;

            byte[] kaynak = Encoding.Unicode.GetBytes(metin);
            using (ICryptoTransform sifrele = aesSaglayici.CreateEncryptor()) 
            {
                byte[] hedef = sifrele.TransformFinalBlock(kaynak, 0, kaynak.Length);
                return Convert.ToBase64String(hedef);
            }

        }

        public string AESsifre_Coz(string sifreliMetin)
        {
            aesSaglayici.BlockSize = 128;
            aesSaglayici.KeySize = 128;

            aesSaglayici.IV = Encoding.UTF8.GetBytes(AES_IV);
            aesSaglayici.Key = Encoding.UTF8.GetBytes(aesAnahtar);
            aesSaglayici.Mode = CipherMode.CBC;
            aesSaglayici.Padding = PaddingMode.PKCS7;

            byte[] kaynak = System.Convert.FromBase64String(sifreliMetin);
            using (ICryptoTransform decrypt = aesSaglayici.CreateDecryptor())
            {
                byte[] hedef = decrypt.TransformFinalBlock(kaynak, 0, kaynak.Length); //[aes.AESsifre_coz]
                return Encoding.Unicode.GetString(hedef);
            }
        }
    }
}
