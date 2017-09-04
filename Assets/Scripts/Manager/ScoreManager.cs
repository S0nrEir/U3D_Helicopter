using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {
	public GameObject ScorePrefab;//对score预制的引用
	private int score=0;//当前游戏的分数
	private List<GameObject> scoreList=new List<GameObject>();//存放当前分数的游戏对象
	public Vector3 startPos=new Vector3(0,-0.75f);
	private readonly string highestScore="highestScore";

	public string HighestScore
	{
		get{return highestScore;}
	}

	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score=value;
			if(scoreList == null)
			{
				scoreList=new List<GameObject>();
			}
			ChangeScore(score);
		}
	}

	/// <summary>
	/// 更新游戏分数
	/// </summary>
	public void ChangeScore(int score)
	{
		Debug.Log("ScoreManager.ChangeScore()");
		List<int> s=new List<int>();//保存各个位上的数字
		//求余拆分各个位上的数字
		do{
			s.Add(score%10);//最后一位
			score/=10;
		}while(score>0);
		if(s.Count != scoreList.Count)//当前分数和之前分数不一样时，实例化一个新的分数对象并平移其位置
		{

			GameObject go=(GameObject)Instantiate(ScorePrefab,startPos,Quaternion.identity);
			go.transform.parent=transform;
			startPos.x-=0.2f;
			scoreList.Add(go);
		}
		for(int i=0; i<scoreList.Count; i++)
		{
			scoreList[i].SendMessage("SetScore",s[i]);
		}
	}
	/// <summary>
	/// 以文件形式保存最高分数
	/// </summary>
	public void SaveScore()
	{
		Debug.Log("ScoreManager.SaveScore()");
		int t=0;
		if(PlayerPrefs.HasKey(highestScore))
		{
			t=PlayerPrefs.GetInt(highestScore);
		}
		if(score>t)
		{
			PlayerPrefs.SetInt(highestScore,score);
			PlayerPrefs.Save();//
		}
	}
	/// <summary>
	/// 清除分数
	/// </summary>
	public void ClearScore()
	{
		Debug.Log("ScoreManager.ClearScore()");
		foreach(var obj in scoreList)
		{
			Destroy(obj);
		}
		scoreList.Clear();
	}
}
