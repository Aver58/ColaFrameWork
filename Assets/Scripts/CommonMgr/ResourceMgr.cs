﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object=UnityEngine.Object;

/// <summary>
/// 资源信息
/// </summary>
public class ResourceInfo
{
    /// <summary>
    /// 实际的资源
    /// </summary>
    public Object Res;

    /// <summary>
    /// 资源的生存时间 -2 永久存在 -1 当前 大于0则按时间删除
    /// </summary>
    public int RemainSec;

    /// <summary>
    /// 资源的类型
    /// </summary>
    public Type ResourceType;
}

/// <summary>
/// 资源管理器
/// </summary>
public class ResourceMgr
{
    private static ResourceMgr instance;
    private ResourceLoader resourceLoader;

    /// <summary>
    /// 资源ID与实际资源的映射表
    /// </summary>
    private Dictionary<int, ResourceInfo> id2ResourceDic;

    /// <summary>
    /// 需要清理的资源列表
    /// </summary>
    private List<int> resClearList;

    /// <summary>
    /// 用于计时的变量
    /// </summary>
    private float timeCount = 0f;

    public static ResourceMgr GetInstance()
    {
        if (null == instance)
        {
            instance = new ResourceMgr();
        }
        return instance;
    }

    private ResourceMgr()
    {
        GameObject resourceLoaderObj = new GameObject("ResourceLoaderObj");
        GameObject.DontDestroyOnLoad(resourceLoaderObj);

        timeCount = 0f;
        resClearList = new List<int>();
        resourceLoader = resourceLoaderObj.AddComponent<ResourceLoader>();
        id2ResourceDic = new Dictionary<int, ResourceInfo>();
    }

    /// <summary>
    /// 加载文本(同步)
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <param name="callback"></param>
    public void LoadText(string path, string fileName, Action<string, string> callback)
    {
        TextAsset textAsset = resourceLoader.Load<TextAsset>(path);
        if (null != callback)
            callback(fileName, textAsset.text);
    }

    /// <summary>
    /// 支持从Resources以外目录读取
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <param name="callback"></param>
    public void LoadText(string path, string fileName, Action<string, byte[]> callback)
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        WWW www = new WWW(path);
        while (!www.isDone)
        {
        }
        if (null != callback)
        {
            callback(fileName,www.bytes);
        }
#else
        //支持从Resources以外目录读取
        var bytes = File.ReadAllBytes(path);
        if (null!=callback)
            callback(fileName, bytes);
#endif
    }

    /// <summary>
    /// 加载文本(异步)
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <param name="callback"></param>
    public void LoadTextAsync(string path, string fileName, Action<string, string> callback)
    {
        resourceLoader.LoadAsync<TextAsset>(path, (obj, name) =>
        {
            TextAsset textAsset = obj as TextAsset;
            if (null != callback)
                callback(fileName, textAsset.text);
        });
    }

    /// <summary>
    /// 模拟 Update
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        if (null != instance)
        {
            timeCount += deltaTime;
            if (timeCount >= 1f)
            {
                timeCount = 0;
                using (var enumator = id2ResourceDic.GetEnumerator())
                {
                    while (enumator.MoveNext())
                    {
                        if (enumator.Current.Value.RemainSec > 0)
                        {
                            enumator.Current.Value.RemainSec--;
                        }
                        if (enumator.Current.Value.RemainSec <= 0)
                        {
                            resClearList.Add(enumator.Current.Key);
                        }
                    }
                }
                RemoveResList(resClearList);
                resClearList.Clear();
            }
        }
    }

    /// <summary>
    /// 清理列表中对应ID的资源
    /// </summary>
    /// <param name="removeList"></param>需要清理的资源对应的ID列表
    private void RemoveResList(List<int> removeList)
    {
        if(null == removeList || 0 == removeList.Count)return;
        for (int i = 0; i < removeList.Count; i++)
        {
            id2ResourceDic.Remove(removeList[i]);
        }
    }
}
