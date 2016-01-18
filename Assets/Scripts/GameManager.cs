using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum Difficulty {
        Easy,
        Medium,
        Hard
    }
    public Difficulty difficulty = Difficulty.Hard;

    public Dropdown startMenuDifficultyDropdown;

    public GameObject ship;
    private HingeJoint2D angleLocker;

    private float easyLockAngle = 30.0f;
    private float mediumLockAngle = 45.0f;

	// Use this for initialization
	void Start () {
        angleLocker = ship.GetComponent<HingeJoint2D>();
        SetDifficulty("easy");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetDifficulty() {
        switch (startMenuDifficultyDropdown.value) {
            case 0:
                SetDifficulty("easy");
                break;
            case 1:
                SetDifficulty("medium");
                break;
            case 2:
                SetDifficulty("hard");
                break;
        }
    }

    public void SetDifficulty(string difficultyString) {
        switch (difficultyString) {
            case "easy":
                difficulty = Difficulty.Easy;
                break;
            case "medium":
                difficulty = Difficulty.Medium;
                break;
            case "hard":
                difficulty = Difficulty.Hard;
                break;
        }
        EnableLock(true);
    }

    public void EnableLock(bool enable) {
        if (!enabled) {
            angleLocker.enabled = false;
            return;
        }
        JointAngleLimits2D limits = angleLocker.limits;
        switch (difficulty) {
            case Difficulty.Easy:
                angleLocker.enabled = true;
                limits.min = -easyLockAngle;
                limits.max = easyLockAngle;
                break;
            case Difficulty.Medium:
                angleLocker.enabled = true;
                limits.min = -mediumLockAngle;
                limits.max = mediumLockAngle;
                break;
            case Difficulty.Hard:
                angleLocker.enabled = false;
                break;
        }
        angleLocker.limits = limits;
    }
}
