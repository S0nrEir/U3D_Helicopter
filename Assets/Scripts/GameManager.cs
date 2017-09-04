using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 挂载：相机
/// 负责游戏各状态之间类的通信
/// </summary>
public class GameManager : MonoBehaviour
{

	public IGameState currState;//指向当前游戏的状态
	public Transform btnGroup;
	public Transform board;
	public Transform obstacleGroup;
	public Transform gesture;//Tutorial
	public Transform obstaclePrefab;//，链接到obstacleGroup，障碍物组预制的引用
	public List<GameObject> obstacleList=new List<GameObject>(4);//对象池，用于保存当前场景中障碍物的集合
	private bool didInitObs=false;//用于标识是否第一次生成障碍物
	public GameObject player;//(运行状态下游戏角色的实例)
	public GameObject playerPrefab;//角色预制的引用
	public GameObject score_board;//记分板的引用
	public ScoreManager scoreManager;
	// Use this for initialization
	void Start ()
	{
		btnGroup = GameObject.Find ("BtnGroup").transform;
		board = GameObject.Find ("Board").transform;
		obstacleGroup = GameObject.Find ("ObstacleGroup").transform;
		//Quaternion.identity:无旋转，该物体完全对齐与世界坐标或父轴
		var obstacleTemp = Instantiate (obstaclePrefab.gameObject,transform.position,Quaternion.identity) as GameObject;//
		//InitCheck (obstacleTemp);
		obstacleTemp.SetActive (false);//隐藏障碍物预制
		obstacleList.Add (obstacleTemp);
		currState = new GameStartState (this);//开始时指向开始状态,
		//Debug.Log ("game start state inited");
		scoreManager=GetComponent<ScoreManager>();
	}
	/// <summary>
	/// 每一帧都调用指向的游戏状态对象的Update方法
	/// </summary>
	void Update ()
	{
		currState .Update ();
	}
	/// <summary>
	/// 设置UI组件是否激活
	/// </summary>
	/// <param name="isActive">If set to <c>true</c> is active.</param>
	public void SetUIActive(bool isActive)
	{
		if(isActive)
		{
			btnGroup.gameObject.SetActive (true);
			board.gameObject.SetActive (true);
			obstacleGroup.gameObject.SetActive (true);
		}
		else
		{
			btnGroup.gameObject.SetActive (false);
			board.gameObject.SetActive (false);
			obstacleGroup.gameObject.SetActive (false);
		}
	}

	/// <summary>
	/// 在GameRunState中生成障碍物
	/// </summary>
	public void SpawnObstacles()
	{
		if(!didInitObs)
		{
			Invoke("SpawnObstacles",1f);
			didInitObs=true;
			return;
		}
		else
		{
			if(obstacleList.Count < 4)
			{
				var obstacleTemp=Instantiate(obstaclePrefab.gameObject,obstaclePrefab.position,Quaternion.identity)as GameObject;
				obstacleTemp.SendMessage("SetInit");
				obstacleTemp.SendMessage("StartMove");
				obstacleList.Add(obstacleTemp);
			}
			else
			{
				foreach(var obj in obstacleList)
				{
					if(obj.activeSelf == false)
					{
						obj.SetActive(true);
						obj.SendMessage("SetInit");
						obj.SendMessage("StartMove");
						break;
					}//end if
				}//end foreach
			}//end else
			Invoke("SpawnObstacles",5f);//该方法在特定的时间后调用相关方法
		}//end else
	}

	/// <summary>
	/// 停止场景当中已经激活的obstacle
	/// </summary>
	public void ScrollBack(float distance)
	{
		Debug.Log("gamemanager.scrollback()");
		CancelInvoke("SpawnObstacles");
		foreach(var obj in obstacleList)
		{
			if(obj.activeSelf)
			{
				iTween.Stop(obj);
				obj.SendMessage("MoveUp",distance);
			}//end if
		}//end foreach
	}

	public void InitCheck(GameObject obj)
	{
		if(obj == null)
		{
			Debug.LogWarning(obj.name+"not been inited successful");
		}
	}
	/// <summary>
	/// 重置游戏状态
	/// </summary>
	public void ResetGame ()
	{
		Debug.Log("GameManager.ResetGame()");
		didInitObs=false;
		//隐藏游戏UI
		btnGroup.gameObject.SetActive(false);
		score_board.SetActive(false);
		Destroy(player);//销毁游戏实例（角色）
		scoreManager.ClearScore();
		//隐藏对象池的所有障碍物

		foreach(var obj in obstacleList)
		{
			obj.SetActive(false);
		}
	}
}
