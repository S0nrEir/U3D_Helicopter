using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 运行时状态
/// </summary>
public class GameRunState : IGameState
{
	private readonly GameManager manager;
	public int stateInt = 0;//状态标识符
	private int layerInt;
	private ScrollManager scrollManager;
	private static GameRunState instance;//设置一个静态变量与其他脚本更好地交互，通过引用该类的静态实例
	private GameObject _player;//游戏角色的引用
	private PlayerManager playerManager;//当前游戏角色身上的PM脚本
	public float backDistance=0;//huigunjuli

	public GameRunState(GameManager mgr)
	{
		manager = mgr;
		manager.gesture.transform.gameObject.SetActive (true);//游戏开始后，显示提示
		scrollManager = GameObject.FindObjectOfType<ScrollManager> ();
		Vector3 tempVec = new Vector3 (Random.Range (0.1f, 1.2f),-0.3f);//随机生成障碍物的位置
		manager.obstacleList [0].SetActive(true);//将当前场景中的第一个障碍物激活
		manager.obstacleList [0].transform.position = tempVec;
		_player = GameObject.Instantiate (manager.playerPrefab, new Vector3 (0, -3.6f), Quaternion.identity)as GameObject;
		#region
		/*if(player != null)
		{
			Debug.Log("player instatiate successful!!");
		}
		else
		{
			Debug.Log("player instatiate faild!");
		}*/
		#endregion
		manager.player = _player;
		playerManager = _player.GetComponent<PlayerManager> ();
		//playerManager=manager.player.GetComponent<PlayerManager>();
		layerInt=LayerMask.GetMask("TriggerPad");//TriggerPad层的ID
		instance = this;
		manager.scoreManager.Score=0;
		Debug.Log("layer int:"+layerInt);
		Debug.Log("开始game run state状态");
	}
	/// <summary>
	/// 
	/// </summary>
	public override void Update()
	{
		if(Input.GetMouseButtonDown(0) && stateInt ==1)
		{
			//Vector3 mouseVec=Camera.main.ScreenToWorldPoint(Input.mousePosition);//获取鼠标位置
			RaycastHit2D hit;
			hit=Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,100,layerInt);
			Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
			if(hit.collider != null)
			{
				Debug.Log(hit.collider.name);
				//根据玩家的输入控制角色的反转
				playerManager.SetFlip();
			}
		}
		Debug.Log("正在game run state状态中");
	}

	public override void SwitchNext()
	{
		manager.currState = new GameEndState (manager);
	}

	public void PrepareGameStart()
	{
		scrollManager.StartMove ();//背景滚动
		manager.obstacleList [0].SendMessage ("StartMove");//游戏开始时，障碍物向下移动
		manager.SpawnObstacles ();//开始生成障碍物
		stateInt++;
	}

	//只读访问，静态引用该类的实例
	public static GameRunState Instance
	{
		get{return instance;}
	}
	#region
	/*
	public GameManager Manager
	{
		get{return manager;}
	}*/
	#endregion

	/// <summary>
	/// 游戏结束
	/// </summary>
	public bool GameOver()
	{

		Debug.Log("gamerunstate.gameover()");

		scrollManager.StopMove();
		stateInt++;
		//背景回滚
		manager.scoreManager.SaveScore();//游戏结束时保存分数
		manager.ScrollBack(backDistance);
		Debug.Log("game over");
		return true;
	}
	public void AddScore()
	{
		manager.scoreManager.Score++;
	}
}
