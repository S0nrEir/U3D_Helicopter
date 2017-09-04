using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoiceManager : MonoBehaviour {
	public List<AudioClip> audioClips;
	private static VoiceManager instance;
	void Awake()
	{
		instance=this;
	}
	public static VoiceManager Instance
	{
		get{return instance;}
	}
	public void Play(int num)
	{
		AudioSource.PlayClipAtPoint(audioClips[num],Vector2.zero);

	}
}
