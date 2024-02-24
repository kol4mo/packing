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
		Win,
		Lose,
		Controls
	}
	private gameState currentState = gameState.start;
	[Header("UI")]
	[SerializeField] TMP_Text timerText;
	[SerializeField] TMP_Text scoreText;
	public GameObject StartScreen;
	public GameObject gameScreen;
	public GameObject controlScreen;
	[Header("Sound")]
	[SerializeField] AudioSource pressSound;
	[Header("Variables")]
	[SerializeField] FloatVariable score;
	[SerializeField] FloatVariable timer;
	[SerializeField] float setTimer = 300;
	[SerializeField] Transform cameraPos;

	public void Update() {
		switch (currentState) {
			case gameState.start:
				Cursor.lockState = CursorLockMode.None;
				StartScreen.SetActive(true);
				gameScreen.SetActive(false);
				controlScreen.SetActive(false);
				break;
			case gameState.startGame:
				Cursor.lockState = CursorLockMode.Locked;
				StartScreen.SetActive(false);
				gameScreen.SetActive(true);
				currentState = gameState.Game;
				timer.value = setTimer;
				break;
			case gameState.Game: 
				timer.value -= Time.deltaTime;
				timerText.text = ((int)timer.value).ToString();
				scoreText.text = score.value.ToString();
				if (timer <= 0) {
				//check win lose
				}
				break;
			case gameState.Win: 
				break;
			case gameState.Lose: 
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

	private void OnTriggerEnter2D(Collider2D collision) {
		cameraPos.position += new Vector3(30, 0, 0);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		cameraPos.position += new Vector3(30, 0, 0);
	}
}
