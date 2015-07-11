using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace JT.THB.ThirdPart.Config.Utilities
{
    /// <summary>
    /// 获取xml文件配置
    /// </summary>
    public class XmlHelper
    {
        private XmlHelper() { }

        /// <summary>
        /// 获取保险公司信息
        /// 载入文件地址:配置AppSettings["InsurerInfoPath"]
        /// </summary>
        /// <returns></returns>
        public static List<T> GetInfoList<T>(string filename)
        {
            return BaseConfigProvider.GetRealBaseConfig<T>(filename);
        }

        /// <summary>
        /// 保存保险公司信息(序列化成xml文件)
        /// 保存文件地址:配置AppSettings["InsurerInfoPath"]
        /// </summary>
        public static void SaveInfoList(object obj, string filename)
        {
            SerializationHelper.Save(obj, filename);
        }
    }

    #region 读取配置操作类

    public class BaseConfigProvider
    {
        /// <summary>
        /// 获取真实基础配置对象
        /// </summary>
        /// <returns></returns>
        public static List<T> GetRealBaseConfig<T>(string filename)
        {
            List<T> newBaseConfig = new List<T>();

            try
            {
                newBaseConfig = (List<T>)SerializationHelper.Load(typeof(List<T>), filename);
            }
            catch
            {
                newBaseConfig = null;
            }
            if (newBaseConfig == null)
            {
                throw new Exception("发生错误: " + filename + "目录下没有正确的文件");
            }
            return newBaseConfig;
        }
    }

    #endregion

    #region 序列化

    public class SerializationHelper
    {
        private SerializationHelper()
        {
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public static object Load(Type type, string filename)
        {
            FileStream fs = null;
            try
            {
                // open the stream...
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }


        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void Save(object obj, string filename)
        {
            FileStream fs = null;
            // serialize it...
            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

        }
    }

    #endregion
}
