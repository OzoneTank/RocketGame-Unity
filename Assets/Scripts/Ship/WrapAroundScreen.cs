using UnityEngine;
using System.Collections;

public class WrapAroundScreen : MonoBehaviour {

    private float leftScreen;
    private float rightScreen;
    private float screenWidth;
    
	// Use this for initialization
	void Start () {
        Camera cam = Camera.main;

        Vector3 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        Vector3 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

        leftScreen = screenBottomLeft.x;
        rightScreen = screenTopRight.x;
        screenWidth = rightScreen - leftScreen;
    }
	
	// Update is called once per frame
	void Update () {
	    if (transform.position.x < leftScreen) {
            transform.position = new Vector3(transform.position.x + screenWidth, transform.position.y);
        } else if (transform.position.x > rightScreen) {
            transform.position = new Vector3(transform.position.x - screenWidth, transform.position.y);
        }
        //Debug.Log(screenWidthVector + " " + leftScreen + " " + rightScreen);
	}
}
