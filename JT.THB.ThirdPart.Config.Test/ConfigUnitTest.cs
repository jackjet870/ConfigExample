using System;
using JT.THB.ThirdPart.Config.Test.Model;
using JT.THB.ThirdPart.Config.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JT.THB.ThirdPart.Config.Test
{
    [TestClass]
    public class ConfigUnitTest
    {
        [TestMethod]
        public void ModelToJson_TestMethod()
        {
            //JSON在线序列化
            //http://www.bejson.com/
            //序列化的JSON放在<![CDATA[]]>标记中
            var model = new PaSftpConfig
            {
                FtpServerIp = "1",
                FtpRemotePath = "1",
                FtpUserId = "1",
                FtpPassword = "1",
            };
            var jsonStr=JsonHelper.ToJsJson(model);
        }
        
        [TestMethod]
        public void JsonToModel_TestMethod()
        {
            var configInfo = ConfigInfoHelper.GetInstance().Get(1);
            var configModel = JsonHelper.JsonDeserialize<PaSftpConfig>(configInfo.Json);
        }

        [TestMethod]
        public void PaSftpConfig_TestMethod()
        {
            var config=ConfigInfo.PaSftp();
        }
    }
}
