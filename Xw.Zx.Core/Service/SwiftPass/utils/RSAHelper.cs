using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace swiftpass.utils
{
    public class RSAHelper
    {
        /// <summary>
        /// 私钥签名
        /// </summary>
        /// <param name="signStr"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string RSASign(string signStr, string privateKey)
        {
            try
            {
                //// net
                //var rsa = new RSACryptoServiceProvider();
                //rsa.FromXmlString(privateKey);
                //byte[] signBytes = rsa.SignData(UTF8Encoding.UTF8.GetBytes(signStr), "md5");
                //return Convert.ToBase64String(signBytes);

                // net core 2.0
                var rsa = RSA.Create();
                rsa.FromXmlStringExtensions(privateKey);
                byte[] bytes = rsa.SignData(UTF8Encoding.UTF8.GetBytes(signStr), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 公钥签名
        /// </summary>
        /// <param name="encrypt_info"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string strEncryptInfo, string publicKey)
        {
            try
            {
                //// net
                //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                //rsa.FromXmlString(strPublicKey);
                //byte[] bytes = rsa.Encrypt(UTF8Encoding.UTF8.GetBytes(strEncryptInfo), false);
                //return Convert.ToBase64String(bytes);

                // net core 2.0
                var rsa = RSA.Create();
                publicKey = RSAPublicKeyJava2DotNet(publicKey);
                rsa.FromXmlStringExtensions(publicKey);
                byte[] bytes = rsa.Encrypt(UTF8Encoding.UTF8.GetBytes(strEncryptInfo), RSAEncryptionPadding.Pkcs1);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// RSA私钥格式转换
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string RSAPrivateKeyJava2DotNet(string privateKey)
        {
            if (string.IsNullOrEmpty(privateKey))
            {
                return string.Empty;
            }

            RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
            return string.Format(
                "<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned())
            );
        }
        /// <summary>
        /// RSA公钥格式转换
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns>格式转换结果</returns>
        public static string RSAPublicKeyJava2DotNet(string publicKey)
        {
            if (string.IsNullOrEmpty(publicKey))
            {
                return string.Empty;
            }

            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            return string.Format(
                "<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned())
            );
        }

        /// <summary>
        /// 公钥验签
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="publicKey"></param>
        /// <param name="signedData"></param>
        /// <returns></returns>
        public static bool ValidateRsaSign(string plainText, string publicKey, string signedData)
        {
            try
            {
                //// net
                //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                //rsa.FromXmlString(publicKey);
                //return rsa.VerifyData(UTF8Encoding.UTF8.GetBytes(plainText), "md5", Convert.FromBase64String(signedData));

                //// net core 2.0
                var rsa = RSA.Create();
                rsa.FromXmlStringExtensions(publicKey);
                return rsa.VerifyData(UTF8Encoding.UTF8.GetBytes(plainText), Convert.FromBase64String(signedData), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }

    /// <summary>
    /// System.Security.Cryptography.RSA 扩展方法
    /// </summary>
    internal static class RSAExtensions
    {
        // 处理 下面两种方式都会出现的 Operation is not supported on this platform 异常
        // RSA.Create().FromXmlString(privateKey) 
        // new RSACryptoServiceProvider().FromXmlString(privateKey) 

        /// <summary>
        /// 扩展FromXmlString
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="xmlString"></param>
        public static void FromXmlStringExtensions(this RSA rsa, string xmlString)
        {
            RSAParameters parameters = new RSAParameters();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "Exponent": parameters.Exponent = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "P": parameters.P = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "Q": parameters.Q = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "DP": parameters.DP = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "DQ": parameters.DQ = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "InverseQ": parameters.InverseQ = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "D": parameters.D = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }

        /// <summary>
        /// 扩展ToXmlString
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="includePrivateParameters"></param>
        /// <returns></returns>
        public static string ToXmlStringExtensions(this RSA rsa, bool includePrivateParameters)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);

            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                  parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null,
                  parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null,
                  parameters.P != null ? Convert.ToBase64String(parameters.P) : null,
                  parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null,
                  parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null,
                  parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null,
                  parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null,
                  parameters.D != null ? Convert.ToBase64String(parameters.D) : null);
        }

    }
}
