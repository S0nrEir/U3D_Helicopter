	using UnityEngine;
using System.Collections;

/// <summary>
/// 挂载在board上，用显示board的上下浮动动画效果
/// </summary>
public class FloatSelf : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		iTween.MoveTo (gameObject, iTween.Hash ("y", -1.6f, "time", 1, "loopType", iTween.LoopType.pingPong, "easeType", iTween.EaseType.linear));
		Debug.Log("Board动画初始化完成");
	}
}
