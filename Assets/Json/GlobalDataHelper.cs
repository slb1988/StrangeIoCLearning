
using System.Text;
using System.Security.Cryptography;

public class GlobalDataHelper
{
    private const string DATA_ENCRYPT_KEY = "a234827890654c3678d77234567890O2";
    private static RijndaelManaged _encryptAlgorithm = null;

    public static RijndaelManaged DataEncryptAlgorithm()
    {
        if (_encryptAlgorithm == null)
        {
            _encryptAlgorithm = new RijndaelManaged();
            _encryptAlgorithm.Key = Encoding.UTF8.GetBytes(DATA_ENCRYPT_KEY);
            _encryptAlgorithm.Mode = CipherMode.ECB;
            _encryptAlgorithm.Padding = PaddingMode.PKCS7;
        }

        return _encryptAlgorithm;
    }
}
