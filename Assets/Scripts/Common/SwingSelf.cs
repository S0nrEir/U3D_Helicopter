 using UnityEngine;
using System.Collections;

/// <summary>
/// 挂载在hammer上，用于显示其来回摆动的动画效果
/// </summary>
public class SwingSelf : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		//1s内Z轴旋转30度(?),loopType循环类型，进/出方式
		iTween.RotateTo (gameObject, iTween.Hash ("z", 30, "time", 1,"loopType",iTween.LoopType.pingPong,"easeType",iTween.EaseType.linear));
		Debug.Log("hammer动画初始化完成");
	}
	// Update is called once per frame
	/*
	void Update ()
	{
	
	}*/
}
