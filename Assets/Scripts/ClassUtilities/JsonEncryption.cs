using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
/// <summary>
/// ����д��һ�����ܴ������ѡ��ʹ�û���ɾ��
/// </summary>
public class JsonEncryption
{
    // ��ԿӦΪ32�ֽڳ��ȣ�256λ��
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("����һ�����ĵ�Key");
    // ��ʼ����������Ϊ16�ֽڳ��ȣ�128λ��
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("����һ����������");
    //����Json�ַ���
    public static string EncryptJson(string json)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(json);
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
    //����Json�ַ���
    public static string DecryptJson(string encryptedJson)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedJson)))
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
    //������ܺ��Json�ļ�(�Զ�����)
    public static void SaveEncryptedJsonToFile(string path, string json)
    {
        string encryptedJson = EncryptJson(json);
        File.WriteAllText(path, encryptedJson);
    }
    //���ؼ��ܺ��Json�ļ�(�Զ�����)
    public static string LoadEncryptedJsonFromFile(string path)
    {
        string encryptedJson = File.ReadAllText(path);
        return DecryptJson(encryptedJson);
    }
}