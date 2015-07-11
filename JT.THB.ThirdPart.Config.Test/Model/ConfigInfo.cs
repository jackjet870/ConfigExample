using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JT.THB.ThirdPart.Config.Utilities;

namespace JT.THB.ThirdPart.Config.Test.Model
{
    public class ConfigInfo
    {
        private static readonly ConfigInfoHelper InfoHelper = ConfigInfoHelper.GetInstance();
        /// <summary>
        /// 平安保单对接-sftp配置
        /// </summary>
        /// <returns></returns>
        public static PaSftpConfig PaSftp()
        {
            return JsonHelper.JsonDeserialize<PaSftpConfig>(InfoHelper.Get(1).Json);
        }
    }
}
