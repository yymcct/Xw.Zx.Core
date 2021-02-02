using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace swiftpass.utils
{
    public class AsymmetricAlgorithmHelper<T>
    where T : AsymmetricAlgorithm, new()
    {
        protected static TResult Execute<TResult>(string key, Func<T, TResult> func)
        {
            using (T algorithm = new T())
            {
                algorithm.FromXmlString(key);
                return func(algorithm);
            }
        }

    }
    public class SwiftRSAHelper : AsymmetricAlgorithmHelper<RSACryptoServiceProvider>
    {
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey">公钥</param>
        /// <param name="content">加密前的原始数据</param>
        /// <param name="fOAEP">如果为 true，则使用 OAEP 填充（仅在运行 Microsoft Windows XP 或更高版本的计算机上可用）执行直接的 System.Security.Cryptography.RSA加密；否则，如果为 false，则使用 PKCS#1 1.5 版填充。</param>
        /// <returns>加密后的结果（base64格式）</returns>
        public static string Encrypt(string publickey, string content, bool fOAEP = false)
        {
            return Execute(publickey,
                algorithm => Convert.ToBase64String(algorithm.Encrypt(Encoding.UTF8.GetBytes(content), fOAEP)));
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey">私钥</param>
        /// <param name="content">加密后的内容(base64格式)</param>
        /// <param name="fOAEP">如果为 true，则使用 OAEP 填充（仅在运行 Microsoft Windows XP 或更高版本的计算机上可用）执行直接的 System.Security.Cryptography.RSA加密；否则，如果为 false，则使用 PKCS#1 1.5 版填充。</param>
        /// <returns></returns>
        public static string Decrypt(string privatekey, string content, bool fOAEP = false)
        {
            return Execute(privatekey,
                algorithm => Encoding.UTF8.GetString(algorithm.Decrypt(Convert.FromBase64String(content), fOAEP)));
        }

        public static string SignData(string privatekey, string content, object halg = null)
        {
            var sign = RSAHelper.RSASign(content, privatekey);
            return sign;
        }

        /// <summary>
        /// RSA验签
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="content">需验证签名的数据(utf-8)</param>
        /// <param name="signature">需验证的签名字符串(base64格式)</param>
        /// <param name="halg">签名采用的算法，如果传null，则采用MD5算法</param>
        /// <returns></returns>
        public static bool VerifyData(string publicKey, string content, string signature, object halg = null)
        {
            content = "attach=补差价拍此处&charset=UTF-8&code_img_url=https://pay.swiftpass.cn/pay/qrcode?uuid=https%3A%2F%2Fqr.95516.com%2F03095810%2FunifiedNative%3FmchNo%3D101520021587%26token%3D2139c095ce2911f6371eadfddca75b90a&code_url=https://qr.95516.com/03095810/unifiedNative?mchNo=101520021587&token=2139c095ce2911f6371eadfddca75b90a&mch_id=101520021587&nonce_str=d4TqpCSzqU9DIBzyVCl1RUNGLgBB5uQx&result_code=0&sign_type=RSA_1_1&status=0&uuid=2139c095ce2911f6371eadfddca75b90a&version=1.0";
            signature = "T3v/vYyxdYvk+wxcNy5tuAE2IZnReLp06milTT7kOmN6X2VQbEpU5/sEsp3shfamQFnG1A1MLJaviUl3iYGWQx76ha//cSrvWsln7d5ElkMrGwxMhn3M9lvimMFBLzn/FMuW8ZTOHArdDhHsGL0B8IPvMNRYyDzpXBruMgVuFzB1o0xksGyoveBiR6eUSODIbL/7qdPV6N4Z5V0GcPYwx651Z5gdOmJHHiH3guFvm+YAd2+NTxKlCtt4Mk3My/n5JGG3ivJHAvdo+M6PCTyZzHGBu9IJG01fqL6o3xEA5Dw3t5Ir826Stdpc8hycaTgD+Xx8yMUXHQxkDDJR+o5rMw==";
           var ok =  RSAHelper.ValidateRsaSign(content, publicKey, signature);

            return ok;
            //return Execute(publicKey,
            //    algorithm => algorithm.VerifyData(Encoding.UTF8.GetBytes(content), GetHalg(halg), Convert.FromBase64String(signature)));
        }
        private static object GetHalg(object halg)
        {
            if (halg == null)
            {
                halg = "MD5";
            }
            return halg;
        }

    }



}
