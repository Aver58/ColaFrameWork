//----------------------------------------------
//            ColaFramework
// Copyright © 2018-2020 ColaFramework 马三小伙儿
//----------------------------------------------

using UnityEngine;

/// <summary>
/// ugui版本的TweenRotation插值动画组件 Tween the object's rotation.
/// </summary>

[AddComponentMenu("UGUI/Tween/UGUI Tween Rotation")]
public class UGUITweenRotation : UITweener
{
    public Vector3 from;
    public Vector3 to;
    public bool quaternionLerp = false;

    RectTransform mTrans;

    public RectTransform cachedTransform { get { if (mTrans == null) mTrans = GetComponent<RectTransform>(); return mTrans; } set { mTrans = value; } }

    [System.Obsolete("Use 'value' instead")]
    public Quaternion rotation { get { return this.value; } set { this.value = value; } }

    /// <summary>
    /// Tween's current value.
    /// </summary>

    public Quaternion value { get { return cachedTransform.localRotation; } set { cachedTransform.localRotation = value; } }

    /// <summary>
    /// Tween the value.
    /// </summary>

    protected override void OnUpdate(float factor, bool isFinished)
    {
        value = quaternionLerp ? Quaternion.Slerp(Quaternion.Euler(from), Quaternion.Euler(to), factor) :
            Quaternion.Euler(new Vector3(
            Mathf.Lerp(from.x, to.x, factor),
            Mathf.Lerp(from.y, to.y, factor),
            Mathf.Lerp(from.z, to.z, factor)));
    }

    /// <summary>
    /// Start the tweening operation.
    /// </summary>

    static public TweenRotation Begin(GameObject go, float duration, Quaternion rot)
    {
        TweenRotation comp = UITweener.Begin<TweenRotation>(go, duration);
        comp.from = comp.value.eulerAngles;
        comp.to = rot.eulerAngles;

        if (duration <= 0f)
        {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
        return comp;
    }

    [ContextMenu("Set 'From' to current value")]
    public override void SetStartToCurrentValue() { from = value.eulerAngles; }

    [ContextMenu("Set 'To' to current value")]
    public override void SetEndToCurrentValue() { to = value.eulerAngles; }

    [ContextMenu("Assume value of 'From'")]
    void SetCurrentValueToStart() { value = Quaternion.Euler(from); }

    [ContextMenu("Assume value of 'To'")]
    void SetCurrentValueToEnd() { value = Quaternion.Euler(to); }
}
