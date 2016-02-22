using UnityEngine;
using System.Collections;

public class SpinObject : MonoBehaviour {

    public float xTurnSpeed = 0.0f;
    public float yTurnSpeed = 0.0f;
    public float zTurnSpeed = 0.0f;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.right, xTurnSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, yTurnSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward, zTurnSpeed * Time.deltaTime);
    }
}
