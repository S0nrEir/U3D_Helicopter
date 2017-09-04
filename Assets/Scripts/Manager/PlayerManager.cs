using UnityEngine;
using System.Collections;

/// <summary>
/// 控制角色行为
/// </summary>
public class PlayerManager : MonoBehaviour
{
	//用于控制角色的idle动画和行为动画的转变
	private Animator bodyAnim;
	private Animator wheelAnim;
	private int faceRight=1;//当前角色朝向是否为右
	private Rigidbody2D rigidbody2d;
	void Awake()
	{
		bodyAnim = transform.GetChild (1).GetComponentInChildren<Animator> ();
		wheelAnim = transform.GetChild (0).GetComponentInChildren<Animator> ();
		rigidbody2d = transform.GetComponent<Rigidbody2D> ();
	}

	/// <summary>
	/// 游戏开始时调用该方法
	/// </summary>
	public void StartPlayer()
	{
		bodyAnim.SetTrigger ("blink");
		wheelAnim.SetInteger ("StateInt", 1);
		Invoke ("SpeedUpWheel",1f);
	}

	private void SpeedUpWheel()
	{
		//螺旋桨动画的播放速度设为5
		wheelAnim.speed = 5f;
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "score")
		{
			//得分+音效
			Debug.Log("bonus point!");
			GameRunState.Instance.AddScore();
			VoiceManager.Instance.Play(0);
		}
		//以下的else if 代码为test:
		else if(col.tag =="obstacle")
		{
			Debug.Log("touch a obstacle!");
			VoiceManager.Instance.Play(1);
			GameOver();
		}
		/*以下的else if代码为正常代码:
		else if(col.tag != "TriggerPad")
		{
			Debug.Log("peng chu");
			GameOver();
			//音效
		}*/
	}

	/// <summary>
	/// 游戏结束
	/// </summary>
	private void GameOver()
	{
		GetComponent<Collider2D> ().enabled = false;//取消2D碰撞
		rigidbody2d.velocity = Vector2.zero;//重置刚体
		rigidbody2d.Sleep ();//强制一个刚体休眠至少一帧
		//当刚体空闲时，如一个掉到地板上的盒子，他们就会开始休眠。休眠是性能优化的一个策略，
		//即物理引擎不会处理那些处于休眠中的刚体。这样一来，只要某刚体在正常情况下不移动，
		//那么你可以在你的场景中添加大量的该刚体。
		iTween.RotateTo (gameObject, iTween.Hash ("z",180,"time",1,"easeType",iTween.EaseType.linear,"loopType",iTween.LoopType.none));
		GameRunState.Instance.GameOver();
		Invoke("SwitchGame",1f);
	}
	/// <summary>
	/// 翻转
	/// </summary>
	public void SetFlip()
	{
		//transform.localScale = new Vector3 (faceRight, transform.position.y);
		transform.localScale=new Vector3(faceRight,1);//这里通过设置缩放为负值来实现方向转变
		rigidbody2d.velocity = new Vector2 (faceRight*2.5f, 0);//每次翻转时改编2D碰撞器在X轴上的速度
		faceRight *= -1;
	}
	/// <summary>
	/// 转换游戏状态
	/// </summary>
	private void SwitchGame()
	{
		Debug.Log("PlayerManager.SwitchGame()");
		GameRunState.Instance.SwitchNext();
	}
}
