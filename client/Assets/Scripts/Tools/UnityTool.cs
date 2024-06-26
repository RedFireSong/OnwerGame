using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityTool
{
    public static GameObject FindChild(GameObject parent, string childName)
    {
        
        Transform[] children;
        children = parent.GetComponentsInChildren<Transform>(true);
        bool isFinded = false;
        Transform child = null;
        foreach (Transform t in children)
        {
            if (t.name == childName)
            {
                if (isFinded)
                {
                    Debug.LogWarning("在游戏物体" + parent + "下存在不止一个子物体:" + childName);
                }
                isFinded = true;
                child = t;
               
            }
        }
        if (isFinded)
        {
            return child.gameObject;
        }
        Debug.Log("没有找到该物体：" + childName);
            return null;


    }
    public static T[] FindChilds<T>(GameObject parent)
    {
        return parent.transform.GetComponentsInChildren<T>();
    }


    public static void DeleteGameObject(GameObject go)
    {
        MonoBehaviour.Destroy(go.gameObject);
    }
}
