using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
    private bool gamePaused = false;
    // Use this for initialization

    public GameObject startCanvas;
    public GameObject playCanvas;
    public GameObject pauseCanvas;
    public GameObject settingsCanvas;
    public GameObject gameOverCanvas;

    public enum ScreenMode {
        Start,
        Play,
        Pause,
        Settings,
        GameOver
    }

    public ScreenMode currentMode = ScreenMode.Start;
    private GameObject currentCanvas;

	void Start () {
        DisableAllCanvases();
        SetScreenMode(currentMode);
	}

    private void DisableAllCanvases() {
        pauseCanvas.SetActive(false);
        playCanvas.SetActive(false);
        startCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    public void SetScreen(string screen) {;

        switch(screen) {
            case "play":
                SetScreenMode(ScreenMode.Play);
                break;
            case "pause":
                SetScreenMode(ScreenMode.Pause);
                break;
            case "start":
                SetScreenMode(ScreenMode.Start);
                break;
            case "settings":
                SetScreenMode(ScreenMode.Settings);
                break;
            case "gameover":
                SetScreenMode(ScreenMode.GameOver);
                break;
        }
    }

    public void SetScreenMode(ScreenMode mode) {
        if (currentCanvas != null) {
            currentCanvas.SetActive(false);
        }
        currentMode = mode;
        if (currentMode == ScreenMode.Play || currentMode == ScreenMode.GameOver) {
            ResumeGame();
        } else {
            PauseGame();
        }

        switch (currentMode) {
            case ScreenMode.Start:
                currentCanvas = startCanvas;
                break;
            case ScreenMode.Pause:
                currentCanvas = pauseCanvas;
                break;
            case ScreenMode.Play:
                currentCanvas = playCanvas;
                break;
            case ScreenMode.Settings:
                currentCanvas = settingsCanvas;
                break;
        }
        if (currentCanvas!= null) {
            currentCanvas.SetActive(true);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            switch(currentMode) {
                case ScreenMode.Start:
                    ExitGame();
                    break;
                case ScreenMode.Play:
                    SetScreenMode(ScreenMode.Pause);
                    break;
                case ScreenMode.Pause:
                    SetScreenMode(ScreenMode.Play);
                    break;
                case ScreenMode.Settings:
                    SetScreenMode(ScreenMode.Pause);
                    break;
            }
        }
    }

    public void PauseGame() {
        gamePaused = true;
        Time.timeScale = 0.0F;
    }

    public void ResumeGame() {
        gamePaused = false;
        Time.timeScale = 1.0F;
    }

    public void ExitGame() {
        Application.Quit();
    }
    
}
