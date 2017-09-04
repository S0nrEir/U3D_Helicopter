using UnityEngine;
using System.Collections;

/// <summary>
/// 挂载：BackGroundGroup
/// 处理背景图片的滚动
/// </summary>
public class ScrollManager : MonoBehaviour
{
	private BackGroundManager[] backGrounds;//两个背景
	private Transform landGround;//处理DownGround的位置
	private Vector3 startPos;

	void Awake()
	{
		//初始化
		backGrounds = FindObjectsOfType<BackGroundManager> ();//遍历场景中所有物体,获取泛型T类型物体(获取所有子物体中的对应该类型脚本)
		landGround = transform.GetChild (2);//0,1,2
		startPos = landGround.position;
	}

	public void StartMove()
	{
		//将DownGround向下移动三个单位
		iTween.MoveTo (landGround.gameObject, iTween.Hash ("y", -7.3f, "easeType", iTween.EaseType.linear, "speed", 1));
		for(int i=0; i<backGrounds.Length; i++)
		{
			backGrounds[i].SendMessage("StartMove");//调用子对象的StartMove方法
		}
	}
	/// <summary>
	/// 游戏结束，该函数使背景停止滚动
	/// 170816 1622 continue
	/// </summary>
	public void StopMove()
	{
		Debug.Log("scrollmanager.stopmove()");
		iTween.Stop(landGround.gameObject);//停止landground上的动画
		GameRunState.Instance.backDistance=startPos.y-landGround.transform.position.y;//确定distance的值
		iTween.MoveBy(landGround.gameObject,iTween.Hash("y",GameRunState.Instance.backDistance,"easeType",iTween.EaseType.linear,"time",1f));
		for(int i=0; i<backGrounds.Length; i++)
		{
			iTween.Stop(backGrounds[i].gameObject);//停止backGrounds挂载所有物体上的动画
		}
		for(int i=0; i<backGrounds.Length; i++)
		{
			backGrounds[i].SendMessage("MoveUp",GameRunState.Instance.backDistance);
		}
	}
}
