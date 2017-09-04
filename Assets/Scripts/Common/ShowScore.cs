using UnityEngine;
using System.Collections;

/// <summary>
/// 挂载：分数对象
/// 用于处理分数图Ji的更新
/// </summary>
public class ShowScore : MonoBehaviour
{
	private Sprite[] sprites;//表示分数的2D精灵
	public string spName="PreSprites";
	private SpriteRenderer spRender;
	private void Awake()
	{
		sprites=Resources.LoadAll<Sprite>(spName);
		spRender=GetComponent<SpriteRenderer>();
		if(sprites.Length <=1)
		{
			Debug.Log("not get sprites!");
		}
	}
	/// <summary>
	/// 传入一个数值 在2D精灵渲染器上更换并表示它
	/// </summary>
	public void SetScore(int num)
	{
		Debug.Log("ShowScore.SetScore()");
		//GetComponent<SpriteRenderer>().sprite=sprites[num+1];
		//spRender.sprite=sprites[num+1];
		spRender.sprite=sprites[num+1];
	}
}