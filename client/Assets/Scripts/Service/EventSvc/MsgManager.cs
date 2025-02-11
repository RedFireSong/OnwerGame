
using System.Collections.Generic;
using System;

public class MsgManager<T>
{
    Dictionary<T, List<Action<object, object>>> msgHandlerDic = new Dictionary<T, List<Action<object, object>>>();
    Dictionary<object, List<T>> targetMsgDic = new Dictionary<object, List<T>>();

    public void AddMsgHandler(T t, Action<object, object> cb)
    {
        if (!msgHandlerDic.ContainsKey(t))
        {
            msgHandlerDic[t] = new List<Action<object, object>>();
        }
        List<Action<object, object>> list = msgHandlerDic[t];
        Action<object, object> cbt = list.Find((Action<object, object> callback) => {
            return callback.Equals(cb);
        });
        if (cbt != null)
        {
            return;
        }
        list.Add(cb);

        if (cb != null && cb.Target != null)
        {
            if (!targetMsgDic.ContainsKey(cb.Target))
            {
                targetMsgDic[cb.Target] = new List<T>();
            }
            List<T> evtLst = targetMsgDic[cb.Target];
            evtLst.Add(t);
        }
    }
    public void RmvMsgHandler(T id)
    {
        if (msgHandlerDic.ContainsKey(id))
        {
            var handlerLst = msgHandlerDic[id];
            foreach (Action<object, object> cb in handlerLst)
            {
                if (cb != null
                    && cb.Target != null
                    && targetMsgDic.ContainsKey(cb.Target))
                {
                    var idLst = targetMsgDic[cb.Target];
                    idLst.RemoveAll((T t) => {
                        return id.Equals(t);
                    });
                    if (idLst.Count == 0)
                    {
                        targetMsgDic.Remove(cb.Target);
                    }
                }
            }
        }
        msgHandlerDic.Remove(id);
    }
    public void RmvTargetHandler(object target)
    {
        if (targetMsgDic.ContainsKey(target))
        {
            List<T> evtLst = targetMsgDic[target];
            for (int i = evtLst.Count - 1; i >= 0; --i)
            {
                T evt = evtLst[i];
                if (msgHandlerDic.ContainsKey(evt))
                {
                    List<Action<object, object>> cbLst = msgHandlerDic[evt];
                    cbLst.RemoveAll((Action<object, object> cb) => {
                        return cb.Target == target;
                    });
                    if (cbLst.Count == 0)
                    {
                        msgHandlerDic.Remove(evt);
                    }
                }
            }
            targetMsgDic.Remove(target);
        }
    }

    public List<Action<object, object>> GetMsgAllHandler(T t)
    {
        msgHandlerDic.TryGetValue(t, out List<Action<object, object>> lst);
        return lst;
    }
}
