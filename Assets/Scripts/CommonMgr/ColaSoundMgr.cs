﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 声音管理接口
/// </summary>
public interface ISound
{
    /// <summary>
    /// 所有类型的音乐全部都静音
    /// </summary>
    void MuteAllSound();
    /// <summary>
    /// 静音指定类型的音乐
    /// </summary>
    /// <param name="soundType"></param>
    void MuteSoundByType(SoundType soundType);
    /// <summary>
    /// 调整指定类型声音的音量
    /// </summary>
    /// <param name="soundTypem"></param>
    /// <param name="value"></param>
    void SetVolume(SoundType soundTypem, float value);
    /// <summary>
    /// 获取指定类型声音的音量
    /// </summary>
    /// <param name="soundType"></param>
    float GetVolume(SoundType soundType);
    /// <summary>
    /// 指定类型的音乐是否静音
    /// </summary>
    /// <param name="soundType"></param>
    /// <returns></returns>
    bool GetMuteByType(SoundType soundType);
    /// <summary>
    /// 播放背景音乐(参数：声音类型/资源id/淡入时长/循环次数)
    /// </summary>
    /// <param name="soundType"></param>
    /// <param name="id"></param>
    /// <param name="loopTimes"></param>
    void PlayBackgroundMusicById(SoundType soundType, int id, float fadeInTime = 0.0f, int loopTimes = 1);
    /// <summary>
    /// 停止播放背景音乐
    /// </summary>
    /// <param name="soundType"></param>
    /// <param name="fadaOutTime"></param>
    void StopBackgroundMusic(SoundType soundType, float fadaOutTime);

    /// <summary>
    /// 播放3D音频
    /// </summary>
    /// <param name="id"></param>
    /// <param name="postion"></param>
    /// <param name="fadeInTime"></param>
    /// <param name="loopTimes"></param>
    void Play3DSoundById(int id, Vector3 postion, float fadeInTime = 0.0f, int loopTimes = 1);
    /// <summary>
    /// 播放2D音频
    /// </summary>
    /// <param name="id"></param>
    /// <param name="postion"></param>
    /// <param name="fadeInTime"></param>
    /// <param name="loopTimes"></param>
    void Play2DSoundById(int id, Vector3 postion, float fadeInTime = 0.0f, int loopTimes = 1);
}

/// <summary>
/// 声音类型枚举
/// </summary>
public enum SoundType : byte
{
    GUI = 1,
    ENVIRONMENT = 2,
    BACKGROUND = 3,
    Skill = 4,
}

/// <summary>
/// ColaFramework声音管理器
/// </summary>
public class ColaSoundMgr : MonoBehaviour
{

}