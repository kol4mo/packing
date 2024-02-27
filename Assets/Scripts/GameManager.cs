using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private enum gameState {
		start,
		startGame,
		Game,
		End,
		Controls,
		Pause
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
	public GameObject pauseScreen;
	public GameObject tutorialScreen;
	public GameObject endScreen;
	[Header("Sound")]
	[SerializeField] AudioSource pressSound;
	[Header("Variables")]
	[SerializeField] FloatVariable score;
	[SerializeField] FloatVariable totalScore;
	[SerializeField] FloatVariable timer;
	[SerializeField] IntVariable day;
	[SerializeField] float setTimer = 300;

	public Image image;
	public Sprite image01;
	public Sprite image02;
	public Sprite image03;
	public Sprite image04;

	int currentImage = 1;

	public void Update() {
		switch (currentState) {
			case gameState.start:
				Cursor.lockState = CursorLockMode.None;
				Time.timeScale = 0;
				StartScreen.SetActive(true);
				gameScreen.SetActive(false);
				controlScreen.SetActive(false);
				endScreen.SetActive(false);
				break;
			case gameState.startGame:
				Time.timeScale = 1;
				Cursor.lockState = CursorLockMode.Locked;
				StartScreen.SetActive(false);
				gameScreen.SetActive(true);
				endScreen.SetActive(false);
				currentState = gameState.Game;
				timer.value = setTimer;
				score.value = 0;
				break;
			case gameState.Game:
				Cursor.lockState = CursorLockMode.Locked;
				Time.timeScale = 1;
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
				if (Input.GetKeyDown(KeyCode.Escape)) {
					pauseMenu();
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
			case gameState.Pause:
				Cursor.lockState = CursorLockMode.None;
				Time.timeScale = 0;
				StartScreen.SetActive(false);
				gameScreen.SetActive(false);
				controlScreen.SetActive(false);
				pauseScreen.SetActive(true);
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
		tutorialScreen.SetActive(false);
	}

	public void pauseMenu() {
		if (currentState == gameState.Game) {
			currentState = gameState.Pause;
		}
	}

	public void resume() {
		if (currentState == gameState.Pause) {
			currentState = gameState.Game;
			pauseScreen.SetActive(false);
			gameScreen.SetActive(true);
		}
	}

	public void exitGame() {
		Application.Quit();
	}

	public void showTutorial() {
		StartScreen.SetActive(false);
		gameScreen.SetActive(false);
		controlScreen.SetActive(false);
		pauseScreen.SetActive(false);
		endScreen.SetActive(false);
		tutorialScreen.SetActive(true);
	}

	public void OnClickNext() {
		switch (currentImage) {
			case 1:
				image.sprite = image01;
				currentImage = 2;
				break;
			case 2:
				image.sprite = image02;
				currentImage = 3;
				break;
			case 3:
				image.sprite = image03;
				currentImage = 4;
				break;
			case 4:
				image.sprite = image04;
				currentImage = 1;
				break;
		}
	}

	public void returnToMenu() {
		pauseScreen.SetActive(false);
		currentState = gameState.start;
	}
}