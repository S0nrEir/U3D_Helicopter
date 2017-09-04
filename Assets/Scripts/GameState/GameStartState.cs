using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏开始时的状态
/// </summary>
public class GameStartState : IGameState
{
	private readonly GameManager manager;
	public Transform btnGroup;
	public Transform board;
	public Transform obstacleGroup;
	private int layerInt;//UI层ID


	/// <summary>
	/// 构造函数指定一个GameManager对象
	/// </summary>
	/// <param name="mgr">Mgr.</param>
	public GameStartState(GameManager mgr)
	{
		manager = mgr;
		btnGroup = manager.btnGroup;
		board = manager.board;
		obstacleGroup = manager.obstacleGroup;

		manager.SetUIActive(true);//jihuo
		layerInt = LayerMask.NameToLayer ("UI");//拿到UI层的ID
		Debug.Log("进入game start状态");
	}

	public override void Update()//重写调用的Update方法
	{
		if(Input.GetMouseButtonDown(0))//检测鼠标左键按下
		{
			//ScreenToWorldPoint(Vector3):屏幕坐标转世界坐标
			//将鼠标的屏幕位置转换为世界坐标位置
			//Raycast(vect3,vect3,int,int):射线原点，方向，射线的距离（长度），触发特定层的碰撞器
			Vector3 mouseVec=Camera.main.ScreenToWorldPoint(Input.mousePosition);//获取鼠标位置
			RaycastHit2D hit;
			//hit=Physics2D.Raycast(Camera.main.transform.position,mouseVec,100,1<<layerInt);
			//hit=Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,100,1 << layerInt);
			hit=Physics2D.Raycast(Camera.main.transform.position,mouseVec,100,1<<layerInt);
			Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
			if(hit.collider != null)//检查射线触发的碰撞器位置
			{
				switch(hit.collider.name)
				{
					case "BtnStart":
						SwitchNext();
						break;
					case "BtnRank":
						Debug.Log("pressed rank btn");
						break;
				}
			}
		}
		#region
		/*
		Debug.Log("正在game start状态中");
		if(Input.GetKeyDown(KeyCode.A))//按下A则跳入到下一个状态(GameRunState)
		{
			SwitchNext();
		}
		*/
		#endregion
	}
	public override void SwitchNext()
	{
		//隐藏UI
		manager.SetUIActive (false);
		//Debug.Log("zhun bei jin ru run state...");
		manager.currState = new GameRunState (manager);//跳转到下一个状态
	}
}
