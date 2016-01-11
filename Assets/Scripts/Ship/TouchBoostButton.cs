using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchBoostButton : MonoBehaviour {
    public ShipMove shipMoveScript;
    public bool isLeft = false;
    Sprite image;

    // Use this for initialization
    void Start () {
        image = GetComponent<Sprite>();
    }
	
	// Update is called once per frame
	void Update () {
        bool isTouched = false;

        foreach (Touch touch in Input.touches) {
            if (image.bounds.Contains(touch.position)) {
                isTouched = true;
                break;
            }
        }

        
        shipMoveScript.toggleBoost(isLeft, isTouched);
    }

	void OnTouchDown() {
		shipMoveScript.toggleBoost(isLeft, true);
	}
}
