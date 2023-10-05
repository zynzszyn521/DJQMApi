// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Tea;
using Tea.Utils;


namespace SMSLib
{
    public class AliSMS
    {

        /**
         * 使用AK&SK初始化账号Client
         * @param accessKeyId
         * @param accessKeySecret
         * @return Client
         * @throws Exception
         */
        public static AlibabaCloud.SDK.Dysmsapi20170525.Client CreateClient(string accessKeyId, string accessKeySecret)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 必填，您的 AccessKey ID
                AccessKeyId = accessKeyId,
                // 必填，您的 AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // Endpoint 请参考 https://api.aliyun.com/product/Dysmsapi
            config.Endpoint = "dysmsapi.aliyuncs.com";
            return new AlibabaCloud.SDK.Dysmsapi20170525.Client(config);
        }

        /**
        * 使用STS鉴权方式初始化账号Client，推荐此方式。
        * @param accessKeyId
        * @param accessKeySecret
        * @param securityToken
        * @return Client
        * @throws Exception
        */
        public static AlibabaCloud.SDK.Dysmsapi20170525.Client CreateClientWithSTS(string accessKeyId, string accessKeySecret, string securityToken)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 必填，您的 AccessKey ID
                AccessKeyId = accessKeyId,
                // 必填，您的 AccessKey Secret
                AccessKeySecret = accessKeySecret,
                // 必填，您的 Security Token
                SecurityToken = securityToken,
                // 必填，表明使用 STS 方式
                Type = "sts",
            };
            // Endpoint 请参考 https://api.aliyun.com/product/Dysmsapi
            config.Endpoint = "dysmsapi.aliyuncs.com";
            return new AlibabaCloud.SDK.Dysmsapi20170525.Client(config);
        }

        public static string CallMessage(string phoneNumber, string smsCode)
        {
            // 请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID 和 ALIBABA_CLOUD_ACCESS_KEY_SECRET。
            // 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考，建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378671.html
            AlibabaCloud.SDK.Dysmsapi20170525.Client client = CreateClient("LTAI5tLtg6p9tS3mTyvnk9KV", "LVnsRnIzdtOjVu2X9xq3iEslZvONpe");
            AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest sendSmsRequest = new AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest
            {
                SignName = "阿里云短信测试",
                TemplateCode = "SMS_154950909",
                PhoneNumbers = phoneNumber,
                TemplateParam = "{\"code\":\"" + smsCode + "\"}",
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsResponse resp = client.SendSmsWithOptions(sendSmsRequest, runtime);
            AlibabaCloud.TeaConsole.Client.Log(AlibabaCloud.TeaUtil.Common.ToJSONString(resp));
            if (resp.Body.Code == "OK")
            {
                return "TRUE^";
            }
            else
            {
                return "FALSE^" + resp.Body.Message;
            }
        }


    }
}
