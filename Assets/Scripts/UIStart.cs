using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStart : MonoBehaviour
{
	/// <summary>
	/// 开始按钮
	/// </summary>
	private Button btnPlay;

	/// <summary>
	/// 声音按钮
	/// </summary>
	private Button btnSound;

	/// <summary>
	/// 背景音乐播放器
	/// </summary>
	private AudioSource audioSourceBG;

	/// <summary>
	/// 声音的图片
	/// </summary>
	private Image imgSound;

	/// <summary>
	/// 声音的图片
	/// </summary>
	public Sprite[] soundSprites;

	void Start ()
	{
		getComponents ();

		btnPlay.onClick.AddListener (onPlayClick);
		btnSound.onClick.AddListener (onSoundClick);
	}


	void OnDestroy ()
	{
		btnPlay.onClick.RemoveListener (onPlayClick);
		btnSound.onClick.RemoveListener (onSoundClick);
	}


	/// <summary>
	/// 寻找组件
	/// </summary>
	private void getComponents ()
	{
		btnPlay = transform.Find ("btnPlay").GetComponent<Button> ();
		btnSound = transform.Find ("btnSound").GetComponent<Button> ();
		audioSourceBG = transform.Find ("btnSound").GetComponent<AudioSource> ();
		imgSound = transform.Find ("btnSound").GetComponent<Image> ();
	}

	/// <summary>
	/// 当开始按钮按下的点击事件
	/// </summary>
	void onPlayClick ()
	{
		SceneManager.LoadScene ("play", LoadSceneMode.Single);
	}

	/// <summary>
	/// 当声音按钮点击时候调用
	/// </summary>
	void onSoundClick ()
	{
		if (audioSourceBG.isPlaying) {
			//正在播放
			audioSourceBG.Pause();
			imgSound.sprite = soundSprites[1];
		} else {
			//停止播放
			audioSourceBG.Play();
			imgSound.sprite = soundSprites[0];
		}
	}
}