using UnityEngine;
using System.Collections;

public class NavigationButtons : MonoBehaviour {

    public void GoToLevel(string level) {
        Debug.Log("LoadLevel " + level);
        Application.LoadLevel(level);
        
    }

    public void Exit() {
        Debug.Log("Exit");
        Application.Quit();
    }
}
