using UnityEngine;
using System.Collections;

/// <summary>
/// 结束状态
/// </summary>
public class GameEndState : IGameState
{
	private readonly GameManager manager;
	private int layerInt;
	public GameEndState(GameManager mgr)
	{
		manager = mgr;
		manager.score_board.SetActive(true);
		manager.btnGroup.gameObject.SetActive(true);
		layerInt = LayerMask.NameToLayer ("UI");
		var rankArr=manager.score_board.GetComponents<ScoreManager>();
		rankArr[0].Score=manager.scoreManager.Score;//dang qian de fen
		rankArr[1].Score=PlayerPrefs.GetInt("highestScore");//zui gao fen
		Debug.Log("game end state...");
	}
	public override void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 mouseVec=Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit;
			hit=Physics2D.Raycast(Camera.main.transform.position,mouseVec,100,1<<layerInt);
			Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
			if(hit.collider != null)
			{
				switch(hit.collider.name)
				{
				case "BtnStart":
					SwitchNext();
					break;
				case "BtnRank":
					Debug.Log("rank btn");
					break;
				}
			}
		}
	}

	/// <summary>
	/// 游戏结束 下一状态跳转至游戏重新开始
	/// </summary>
	public override void SwitchNext()
	{
		manager.ResetGame();
		manager.currState=new GameStartState(manager);
	}
}
