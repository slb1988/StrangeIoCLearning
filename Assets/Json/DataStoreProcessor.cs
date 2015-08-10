
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

using LitJson;

/// <summary>
/// Json Helper 类
/// </summary>
public class DataStoreProcessor
{

    private static DataStoreProcessor _dataStoreProcessor = null;

    public static DataStoreProcessor SharedInstance
    {
        get
        {
            if (_dataStoreProcessor == null)
                _dataStoreProcessor = new DataStoreProcessor();
            return _dataStoreProcessor;
        }
    }

    /// <summary>
    /// 加密数据    
    /// </summary>
    /// <returns>The data.</returns>
    /// <param name="dataToEncrypt">Data to encrypt.</param>
    public string EncryptData(string dataToEncrypt)
    {
        //给明文加密用GetBytes
        byte[] dataToEncryptArray = Encoding.UTF8.GetBytes(dataToEncrypt);
        byte[] dataAfterEncryptArray = GlobalDataHelper.DataEncryptAlgorithm().CreateEncryptor()
    .TransformFinalBlock(dataToEncryptArray, 0, dataToEncryptArray.Length);

        return Convert.ToBase64String(dataAfterEncryptArray, 0, dataAfterEncryptArray.Length);
    }

    /// <summary>
    /// 解密数据 
    /// </summary>
    /// <returns>The data.</returns>
    /// <param name="dataToDecrypt">Data to decrypt.</param>
    public string DecryptData(string dataToDecrypt)
    {
        //给密文解密用FromBase64String
        byte[] dataToDecryptArray = Convert.FromBase64String(dataToDecrypt);
        byte[] dataAfterDecryptArray = GlobalDataHelper.DataEncryptAlgorithm().CreateDecryptor()
    .TransformFinalBlock(dataToDecryptArray, 0, dataToDecryptArray.Length);

        return Encoding.UTF8.GetString(dataAfterDecryptArray);
    }

    /// <summary>
    /// 数据保存
    /// </summary>
    /// <param name="tobject">Tobject.</param>
    /// <param name="path">Path.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public void Save(Object tobject, string path, bool isEncrypt = true)
    {
        string serializedString = JsonMapper.ToJson(tobject);

        using (StreamWriter sw = File.CreateText(path))
        {
            if (isEncrypt)
                sw.Write(EncryptData(serializedString));
            else
                sw.Write(serializedString);
        }
    }

    /// <summary>
    /// 载入数据
    /// </summary>
    /// <param name="path">Path.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public T Load<T>(string path, bool isEncrypt = true)
    {
        if (File.Exists(path) == false)
            return default(T);

        using (StreamReader sr = File.OpenText(path))
        {
            string stringEncrypt = sr.ReadToEnd();

            if (string.IsNullOrEmpty(stringEncrypt))
                return default(T);

            if (isEncrypt)
                return JsonMapper.ToObject<T>(DecryptData(stringEncrypt));
            else
                return JsonMapper.ToObject<T>(stringEncrypt);
        }
    }
}
