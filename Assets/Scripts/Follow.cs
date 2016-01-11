using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public GameObject target;
    public bool followX = true;
    public bool followY = true;

    void Update () {
        jumpToTarget();
    }

    //move position to that of the target position
    private void jumpToTarget() {
        Vector3 targetPos = target.transform.position;
        float x = (followX) ? targetPos.x : gameObject.transform.position.x;
        float y = (followY) ? targetPos.y : gameObject.transform.position.y;
        Vector3 newPosition = new Vector3(x, y, gameObject.transform.position.z);
        gameObject.transform.position = newPosition;

    }
}
