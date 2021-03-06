﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods
{
    public static T CastExamp1<T>(object input)
    {
        return (T)input;
    }

    public static T ConvertExamp1<T>(object input)
    {
        return (T)Convert.ChangeType(input, typeof(T));
    }

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static void ToInt(this string vString, Action<int> actionIfOK, string valName = "value_name")
    {
        int val = 0;
        if (int.TryParse(vString, out val))
            actionIfOK.Invoke(val);
        else
            throw (new Exception(string.Format("Khong the parse int value, attribute name = {0}", valName)));
    }

    public static T ToInt<T>(this string vString, Func<int, T> actionIfOK, string valName = "value_name")
    {
        int val = 0;
        if (int.TryParse(vString, out val))
            return actionIfOK.Invoke(val);
        else
            throw (new Exception(string.Format("Khong the parse int value, attribute name = {0}", valName)));
    }

    public static void ToFloat(this string vString, Action<float> actionIfOK, string valName = "value_name")
    {
        float val = 0;
        if (float.TryParse(vString, out val))
            actionIfOK.Invoke(val);
        else
            throw (new Exception(string.Format("Khong the parse float value, attribute name = {0}", valName)));
    }

    public static T ToFloat<T>(this string vString, Func<float, T> actionIfOK, string valName = "value_name")
    {
        float val = 0;
        if (float.TryParse(vString, out val))
            return actionIfOK.Invoke(val);
        else
            throw (new Exception(string.Format("Khong the parse float value, attribute name = {0}", valName)));
    }

    public static void Loop<T>(this T[,] arr, Action<T, int, int> loopDo)
    {
        int x = arr.GetUpperBound(0);
        int y = arr.GetUpperBound(1);

        for (int i = 0; i <= x; i++)
        {
            for (int j = 0; j <= y; j++)
            {
                loopDo?.Invoke(arr[i, j], i, j);
            }
        }
    }

    /*
    public static T[] Insert<T>(this T[] arr, T ele)
    {
        T[] b = new T[arr.Length + 1];
        arr.CopyTo(b, 0);
        b[arr.Length] = ele;
        return b;
    }
    */

    public static int IndexOf<T>(this T[] arr, T ele)
    {
        int result = -1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].Equals(ele))
            {
                result = i;
                break;
            }
        }
        return result;
    }

    public static T[] Insert<T>(this T[] arr, T ele)
    {
        Array.Resize(ref arr, arr.Length + 1);
        arr[arr.Length - 1] = ele;
        return arr;
    }

    public static T[] ToEnum<T>(this string[] str) where T : struct
    {
        T[] result = new T[str.Length];

        for (int i = 0; i < str.Length; i++)
        {
            result = result.Insert(str[i].ToEnum<T>());
        }

        return result;
    }

    public static T ToEnum<T>(this string str) where T : struct
    {
        T result;
        if (!System.Enum.TryParse(str, out result))
        {
            result = default(T);
            UnityEngine.Debug.LogErrorFormat("Cannot parse to enum {0}", str);
        }
        return result;
    }

    public static T[] ToEnums<T>(this string[] strs) where T : struct
    {
        T[] result = new T[strs.Length];
        for (int i = 0; i < strs.Length; i++)
        {
            result[i] = strs[i].ToEnum<T>();
        }
        return result;
    }

    public static bool Available<V>(this V[] array)
    {
        if (array != null && array.Length > 0)
            return true;

        return false;
    }

    public static bool Had<T>(this IEnumerable<T> iCollection)
    {
        if (iCollection != null)
            if (iCollection.Count<T>() > 0)
                return true;

        return false;
    }
    public static bool Available<V>(this List<V> list)
    {
        if (list != null && list.Count > 0)
            return true;

        return false;
    }

    public static bool Available<K, V>(this Dictionary<K, V> dic)
    {
        if (dic != null)
        {
            if (dic.Count > 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// This function check null Component and Ensure gameobject be atteched exsist
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    public static bool Available(this UnityEngine.MonoBehaviour component)
    {
        if (component)
            if (component.gameObject)
                return true;

        return false;
    }

    public static T DeepCopy<T>(T other)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, other);
            ms.Position = 0;
            return (T)formatter.Deserialize(ms);
        }
    }

    public static void ResetLocal(this UnityEngine.Transform Target)
    {
        Target.transform.localPosition = UnityEngine.Vector3.zero;
        Target.transform.localRotation = new UnityEngine.Quaternion(0, 0, 0, 0);
        Target.transform.localScale = UnityEngine.Vector3.one;
    }
    public static void SetLossyScale(this UnityEngine.Transform go, UnityEngine.Vector3 lossyScale)
    {
        var par = go.parent;
        go.SetParent(null);
        go.localScale = lossyScale;
        go.SetParent(par);
    }

    public static void TryAdd<K, V>(this Dictionary<K, V> dic, K key, V val)
    {
        if (dic.ContainsKey(key))
            dic[key] = val;
        else
            dic.Add(key, val);
    }

    public static bool Exist<T>(this T[] arr, Predicate<T> check)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (check(arr[i])) return true;
        }
        return false;
    }

    public static bool IsTrue(this Dictionary<string, string> dict, string key)
    {
        if (!dict.ContainsKey(key))
            return false;
        bool result = false;

        bool.TryParse(dict[key], out result);

        return result;
    }

    public static bool IsTrue(this Dictionary<string, object> dict, string key)
    {
        if (!dict.ContainsKey(key))
            return false;
        bool result = false;

        result = (bool)dict[key];

        return result;
    }

    public static T GetKeyBasedOnValue<T, V>(this Dictionary<T, V> dic, V val)
    {
        if (dic.Available())
        {
            foreach (var e in dic)
            {
                if (e.Value.Equals(val))
                {
                    return e.Key;
                }
            }
        }


        return default(T);
    }
}

