using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

// 암호화 기능을 제공하는 유틸
public class CryptoUtil : MonoBehaviour
{
    public static string EncryptAES(string plainText, string salt = "")
    {
        // 해시 암호화 알고리즘 인스턴스를 생성한다.
        SHA256 sha256 = SHA256.Create();

        // 운영체제 혹은 프로그래밍 언어별로 string 표현하는 방식이 다 다르므로
        // UTF8 버전 바이트로 배열을 바꿔야한다.
        byte[] bytes = Encoding.UTF8.GetBytes(plainText + salt);
        byte[] encrypted = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(encrypted);
    }

    public static bool VerifyAES(string encrypted, string hash, string salt = "")
    {
        return EncryptAES(encrypted, salt) == hash;
    }

}
