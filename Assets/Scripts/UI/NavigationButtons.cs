using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NavigationButtons : MonoBehaviour {

    public void GoToLevel(string level) {
        Debug.Log("LoadLevel " + level);
        SceneManager.LoadScene(level);
        
    }

    public void Exit() {
        Debug.Log("Exit");
        Application.Quit();
    }
}
