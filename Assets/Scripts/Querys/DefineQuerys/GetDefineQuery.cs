using Define;
using Models;
using QFramework;
using System.Collections.Generic;
using UnityEngine;

public class GetDefineQuery<T> : AbstractQuery<T> where T:IDefine
{
    int id;
    public GetDefineQuery(int id)
    {
        this.id = id;
    }
    protected override T OnDo()
    {
        DefineModel model = this.GetModel<DefineModel>();
        if (model.allDefines.TryGetValue(typeof(T), out dynamic dic))
        {
            Dictionary<int, T> defines = (dic as Dictionary<int, T>);
            if (defines.ContainsKey(id))
                return defines[id];
            else
            {
                Debug.LogError(string.Format("id:{0} ��{1}�����в�����", id, typeof(T)));
                return default;
            }
        }
        else
        {
            Debug.LogError(typeof(T) + " ��allDefine�����в�����");
            return default;
        }
    }
}
