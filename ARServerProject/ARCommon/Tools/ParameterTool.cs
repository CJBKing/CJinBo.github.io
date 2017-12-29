using LitJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARCommon.Tools
{
    public class ParameterTool
    {

        public static T GetParameter<T>(Dictionary<byte,object> parameter,ParameterCode parameterCode,bool isObject=true)
        {
            object o=null;
            //T t = default(T);
            parameter.TryGetValue((byte)parameterCode, out o);
            if(isObject==false)
            {
                return (T)o;
            }
            return JsonMapper.ToObject<T>(o.ToString());
        }
        public static void AddParameter<T>(Dictionary<byte,object> parameters,ParameterCode key,T value,bool isObject=true)
        {
            if (isObject)
            {
                string json = JsonMapper.ToJson(value);
                parameters.Add((byte)key, json);
            }
            else
            {
                parameters.Add((byte)key,value);
            }
        }
    }
}
