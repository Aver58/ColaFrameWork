﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏核心管理中心
/// </summary>
public class GameManager
{

    private static GameManager instance;

    /// <summary>
    /// Launcher的Obj
    /// </summary>
    private GameObject gameLauncherObj;

    /// <summary>
    /// 系统管理器
    /// </summary>
    private SubSysMgr subSysMgr;

    /// <summary>
    /// 场景/关卡管理器
    /// </summary>
    private LevelMgr levelMgr;

    /// <summary>
    /// 资源管理器
    /// </summary>
    private ResourceMgr resourceMgr;

    private GameManager()
    {

    }

    public static GameManager GetInstance()
    {
        if (null == instance)
        {
            instance = new GameManager();
        }
        return instance;
    }

    /// <summary>
    /// 初始化游戏核心
    /// </summary>
    public void InitGameCore(GameObject gameObject)
    {
        gameLauncherObj = gameObject;
        LocalDataMgr.GetInstance().LoadStartConfig(() =>
        {
            
        });

        gameObject.AddComponent<LevelMgr>();
    }


    /// <summary>
    /// 模拟 Update
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        if (null != subSysMgr)
        {
            subSysMgr.Update(deltaTime);
        }
        if (null != resourceMgr)
        {
            resourceMgr.Update(deltaTime);
        }
    }

    /// <summary>
    /// 模拟 LateUpdate
    /// </summary>
    /// <param name="deltaTime"></param>
    public void LateUpdate(float deltaTime)
    {
        
    }

    /// <summary>
    /// 模拟 FixedUpdate
    /// </summary>
    /// <param name="fixedDeltaTime"></param>
    public void FixedUpdate(float fixedDeltaTime)
    {
        
    }

    public void OnApplicationQuit()
    {

    }

    public void OnApplicationPause(bool pause)
    {

    }

    public void OnApplicationFocus(bool focus)
    {

    }
}
