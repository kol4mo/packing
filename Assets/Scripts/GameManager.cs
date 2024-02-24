using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private enum gameState {
		start,
		startGame,
		Game,
		End,
		Controls
	}
	private gameState currentState = gameState.start;
	[Header("UI")]
	[SerializeField] TMP_Text timerText;
	[SerializeField] TMP_Text scoreText;
	[SerializeField] TMP_Text endScoreText;
	[SerializeField] TMP_Text endTotalScoreText;
	[SerializeField] TMP_Text endDayText;
	public GameObject StartScreen;
	public GameObject gameScreen;
	public GameObject controlScreen;
	public GameObject endScreen;
	[Header("Sound")]
	[SerializeField] AudioSource pressSound;
	[Header("Variables")]
	[SerializeField] FloatVariable score;
	[SerializeField] FloatVariable totalScore;
	[SerializeField] FloatVariable timer;
	[SerializeField] IntVariable day;
	[SerializeField] float setTimer = 300;

	public void Update() {
		switch (currentState) {
			case gameState.start:
				Cursor.lockState = CursorLockMode.None;
				StartScreen.SetActive(true);
				gameScreen.SetActive(false);
				controlScreen.SetActive(false);
				endScreen.SetActive(false);
				break;
			case gameState.startGame:
				Cursor.lockState = CursorLockMode.Locked;
				StartScreen.SetActive(false);
				gameScreen.SetActive(true);
				endScreen.SetActive(false);
				currentState = gameState.Game;
				timer.value = setTimer;
				score.value = 0;
				break;
			case gameState.Game:
				timer.value -= Time.deltaTime;
				var ts = TimeSpan.FromSeconds(timer.value);
				timerText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

				scoreText.text = "$" + string.Format("{0:0.00}", score.value);
				if (timer <= 0) {
					//check win lose
					totalScore.value += score.value;
					currentState = gameState.End;
					day.value += 1;
				}
				break;
			case gameState.End:
				Cursor.lockState = CursorLockMode.None;
				endTotalScoreText.text = "$" + string.Format("{0:0.00}", totalScore.value);
				endScoreText.text = "$" + string.Format("{0:0.00}", score.value);
				endDayText.text = "DAY: " + day.value;
				endScreen.SetActive(true);
				break;
			case gameState.Controls:
				StartScreen.SetActive(false);
				gameScreen.SetActive(false);
				controlScreen.SetActive(true);
				break;
		}
	}

	public void startGame() {
		pressSound.Play();
		currentState = gameState.startGame;
	}

	public void showControls() {
		pressSound.Play();
		currentState = gameState.Controls;
	}

	public void mainMenu() {
		pressSound.Play();
		currentState = gameState.start;
	}
}