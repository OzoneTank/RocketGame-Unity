using UnityEngine;
using System.Collections;

public class OffsetByScreen : MonoBehaviour {

    public Vector2 offset;

	// Use this for initialization
	void Start ()
    {
        Camera cam = Camera.main;

        Vector3 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        Vector3 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

        Vector3 screenDim = screenTopRight - screenBottomLeft;

        Vector3 pos = transform.position;
        pos.x += (offset.x * screenDim.x);
        pos.y += (offset.y * screenDim.y);
        transform.position = pos;
    }
}
