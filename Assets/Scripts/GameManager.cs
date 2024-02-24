using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private enum gameState {
		start,
		startGame,
		Game,
		Win,
		Lose
	}
	private gameState currentState = gameState.start;

	public void Update() {
		switch (currentState) {
			case gameState.start:
				Cursor.lockState = CursorLockMode.None;

				break;
			case gameState.startGame:
				Cursor.lockState = CursorLockMode.Locked;

				currentState = gameState.Game;
				break;
			case gameState.Game: 
				break;
			case gameState.Win: 
				break;
			case gameState.Lose: 
				break;
		}
	}

	public void startGame() {
		currentState = gameState.startGame;
	}
}
