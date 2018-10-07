﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ColaFramework框架中负责UI创建与编辑的编辑器
/// </summary>
public class CreateColaUIEditor
{
    private static readonly string UIViewTag = "UIView";
    private static readonly string UIIgneroTag = "UIIgnero";
    private static readonly string UIPropertyTag = "UIProperty";

    /// <summary>
    /// 快速创建UI模版
    /// </summary>
    [MenuItem("GameObject/UI/ColaUI/UIView", false, 1)]
    public static void CreateColaUIView()
    {
        GameObject uguiRoot = GetOrCreateUGUIRoot();

        //创建新的UI Prefab
        GameObject view = new GameObject("NewUIView", typeof(RectTransform));
        view.layer = LayerMask.NameToLayer("UI");
        view.tag = "UIView";
        string uniqueName = GameObjectUtility.GetUniqueNameForSibling(uguiRoot.transform, view.name);
        view.name = uniqueName;
        Undo.RegisterCreatedObjectUndo(view, "Create" + view.name);
        Undo.SetTransformParent(view.transform, uguiRoot.transform, "Parent" + view.name);
        GameObjectUtility.SetParentAndAlign(view, uguiRoot);

        //设置RectTransform属性
        RectTransform rect = view.GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.pivot = new Vector2(0.5f, 0.5f);

        //设置新建的UIView被选中
        Selection.activeGameObject = view;
    }

    /// <summary>
    /// 获取或者创建UGUIRoot（编辑器状态下）
    /// </summary>
    /// <returns></returns>
    private static GameObject GetOrCreateUGUIRoot()
    {
        GameObject selectObj = Selection.activeGameObject;
        //先查找选中的物体的父节点是否是uUGIRoot
        Canvas canvas = (null != selectObj) ? selectObj.GetComponentInParent<Canvas>() : null;
        if (null != canvas && canvas.gameObject.activeInHierarchy)
        {
            return canvas.gameObject;
        }
        //再查找整个面板中是否存在UGIRoot
        canvas = UnityEngine.Object.FindObjectOfType<Canvas>();
        if (null != canvas && canvas.gameObject.activeInHierarchy)
        {
            return canvas.gameObject;
        }

        //如果以上步骤都没有找到，那就从Resource里面加载并实例化一个
        var uguiRootPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Arts/Gui/Prefabs/UGUIRoot.prefab");
        GameObject uguiRoot = CommonHelper.InstantiateGoByPrefab(uguiRootPrefab, null);
        GameObject canvasRoot = uguiRoot.GetComponentInChildren<Canvas>().gameObject;
        return canvasRoot;
    }
}
