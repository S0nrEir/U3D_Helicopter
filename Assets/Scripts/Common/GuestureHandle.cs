using UnityEngine;
using System.Collections;

/// <summary>
/// 挂载：Tutorial
/// 处理手势（检测鼠标点击事件）
/// </summary>
public class GuestureHandle : MonoBehaviour
{
	//点击后隐藏该gameObject
	void OnMouseDown()
	{
		gameObject.SetActive (false);
		//点击该图集 游戏开始
		//调用ScrollManager中的方法
		if(GameRunState.Instance == null)
		{
			Debug.Log("game run state instance is null!");
		}
		GameRunState.Instance.PrepareGameStart ();//点击图集后，图标会消失，背景图片进行滚动
	}
}