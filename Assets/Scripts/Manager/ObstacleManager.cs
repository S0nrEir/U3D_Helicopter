using UnityEngine;
using System.Collections;

/// <summary>
/// 挂载：Obstacle_group
/// 障碍物跟随背景的移动
/// </summary>
public class ObstacleManager : MonoBehaviour
{
	private static float lastX=0;

	/// <summary>
	/// 开始移动障碍物
	/// </summary>
	private void StartMove()
	{
		//使障碍物向下移动
		iTween.MoveTo (gameObject, iTween.Hash ("y",-6.5f,"speed",1,"easeType",iTween.EaseType.linear,"onComplete","SetOff"));
	}

	/// <summary>
	/// 隐藏该物体（障碍物）
	/// </summary>
	private void SetOff()
	{
		gameObject.SetActive (false);
	}

	/// <summary>
	/// 初始化障碍物位置
	/// </summary>
	private void SetInit()
	{
		Vector3 tempVec = new Vector3 ();
		do
		{
			tempVec.x=Random.Range(-0.1f,1.2f);
		}while(Mathf.Abs(lastX-tempVec.x) < 0.2f);//确保两个障碍物之间的位置不小于0.2个单位
		tempVec.y = 1.6f;
		transform.position = tempVec;
	}
	/// <summary>
	/// 
	/// </summary> 
	private void MoveUp(float distance)
	{
		iTween.MoveTo(gameObject,iTween.Hash("y",distance,"time",1f,"easeType",iTween.EaseType.linear));//向上移动distance的距离
	}
}