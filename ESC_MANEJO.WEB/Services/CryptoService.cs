using System;
using ESC_MANEJO.CORE.Interfaces;

namespace ESC_MANEJO.WEB.Services
{
    public class CryptoService : ICryptoService
    {
        public CryptoService()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);            
        }

        public string Encode(string data)
        {
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(data);
            string result = Convert.ToBase64String(encryted);
            return result;
        }

        public string Decode(string data)
        {
            byte[] decryted = Convert.FromBase64String(data);
            string result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

    }
}
