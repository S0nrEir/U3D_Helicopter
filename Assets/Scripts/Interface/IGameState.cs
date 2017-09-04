using UnityEngine;
using System.Collections;

/// <summary>
/// 各个state的父类
/// </summary>
public class IGameState
{
	public virtual void Update()
	{}
	/// <summary>
	//该方法指向下一个游戏状态
	/// </summary>
	public virtual void SwitchNext()
	{}
}
