using UnityEngine;
using System.Collections;

/// <summary>
/// 挂载：BackGround/DownGround
/// 管理背景的移动
/// </summary>
public class BackGroundManager : MonoBehaviour
{
	public float offset=15f;//背景在Y轴上的偏移
	private void StartMove()
	{
		Invoke ("MoveDown",0);
	}

	/// <summary>
	/// 背景向下移动
	/// </summary>
	private void MoveDown()
	{
		//动画完成时，回调ResetPosition函数
		iTween.MoveTo (gameObject,iTween.Hash("y",-10,"easeType",iTween.EaseType.linear,"speed",1,"onComplete","ResetPosition"));
	}
	/// <summary>
	///
	/// </summary>
	private void MoveUp(float distance)
	{
		//在y轴上产生distance的位移
		Debug.Log("backgraound manager.moveup()");
		iTween.MoveBy(gameObject,iTween.Hash("y",distance,"easeType",iTween.EaseType.linear,"time",1f));
	}
	/// <summary>
	/// 重置背景的位置
	/// </summary>
	private void ResetPosition()
	{
		//首先将背景平移到上方
		transform.localPosition = new Vector3 (transform.position.x, transform.position.y + offset);
		Invoke ("MoveDown", 0);//重置位置完成之后，立马调用MoveDown函数，使背景继续向下移动
	}

}
