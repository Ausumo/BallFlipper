using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;


public class LoseSceneControlls : MonoBehaviour
{
    [SerializeField]
    [FormerlySerializedAs("_scoreText")]
    private TMP_Text _scoreText;

	[SerializeField]
	[FormerlySerializedAs("_highscoreText")]
	private TMP_Text _highscoreText;

	private AudioManager _audioManager;

	private void Start()
    {
		_audioManager = GameObject.FindGameObjectWithTag("AudioManager").gameObject.GetComponent<AudioManager>();

		PlayMusic("menuMusic");

		var score = PlayerPrefs.GetInt("scoreCount");

		if (score > GameManager.Instance.Highscore)
        {
			GameManager.Instance.Highscore = score;
			GameManager.Instance.SaveHighscore();
		}

		_scoreText.text = "Score: " + score;
        _highscoreText.text = "Highscore: " + GameManager.Instance.Highscore;
	}

    public void RestartGame()
    {
		PlaySound("button");
		SceneManager.LoadScene(1);
    }

    public void QuitToMenu()
    {
		PlaySound("button");
		StopMusic("menuMusic");
		SceneManager.LoadScene(0);
    }

	public void PlaySound(string soundName)
	{
		_audioManager.PlaySound(soundName);
	}

	public void PlayMusic(string musicName)
	{
		_audioManager.PlayMusic(musicName);
	}

	public void StopMusic(string musicName)
	{
		_audioManager.StopMusic(musicName);
	}
}
