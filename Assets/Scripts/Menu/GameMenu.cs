using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {
    private bool gamePaused = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void pauseGame() {
        gamePaused = true;
        Time.timeScale = 0.0F;
    }

    public void unpauseGame() {
        gamePaused = false;
        Time.timeScale = 1.0F;
    }

    public void togglePauseGame() {
        gamePaused = !gamePaused;
        if (gamePaused) {
            Time.timeScale = 0.0f;
        } else {
            Time.timeScale = 1.0f;
        }
    }
}
