using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using JT.THB.ThirdPart.Config.Model;
using JT.THB.ThirdPart.Config.Utilities;

namespace JT.THB.ThirdPart.Config
{
    /// <summary>
    /// 配置信息帮助类
    /// </summary>
    public class ConfigInfoHelper
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        private Dictionary<int, ConfigInfo> _info_dic;
      

        /// <summary>
        /// 单例
        /// </summary>
        private static volatile ConfigInfoHelper _instance;
        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object lockhelper = new object();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ConfigInfoHelper()
        {
            //配置信息载入
            string filename = ConfigurationManager.AppSettings["ConfigInfoPath"];
            filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            List<ConfigInfo> list = XmlHelper.GetInfoList<ConfigInfo>(filename);
            this._info_dic = new Dictionary<int, ConfigInfo>();
            foreach (ConfigInfo obj in list)
            {
                _info_dic.Add(obj.Id, obj);             
            }
        }

        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static ConfigInfoHelper GetInstance()
        {
            if (_instance == null)
            {
                lock (lockhelper)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigInfoHelper();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ConfigInfo Get(int key)
        {
            if (this._info_dic.ContainsKey(key))
                return this._info_dic[key];
            return null;
        }
        

        /// <summary>
        /// 获取配置信息字典
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, ConfigInfo> GetDictionary()
        {
            return this._info_dic;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (_instance != null)
            {
                lock (lockhelper)
                {
                    if (_instance != null)
                    {
                        this._info_dic = null;
                        _instance = null;
                    }
                }
            }
        }

    }
}
