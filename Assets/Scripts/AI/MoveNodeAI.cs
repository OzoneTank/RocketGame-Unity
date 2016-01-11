using UnityEngine;
using System.Collections;


public class MoveNodeAI : MonoBehaviour {

    //public variables
    public GameObject[] moveNodes;
    public float moveSpeed = 1.0f;
    public float minDistance = 0.1f;
    //true: follow nodes in a circuit, false: will reverse direction when hits and end node
    public bool circuit = true;
    //direction in node path the object will move
    public bool movingForward = true;

    //private variables
    int currentNode = 0;
	void Start () {
        if (moveNodes.Length > 0) {
            gameObject.transform.position = moveNodes[currentNode].transform.position;
            setNextNode();
        }
	}
    
    void Update() {
        moveTowardsNextNode();
    }

    private void moveTowardsNextNode() {
        if (moveNodes.Length > 1) {
            Vector3 currentTarget = moveNodes[currentNode].transform.position;
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);
            if (Vector3.Distance(transform.position, currentTarget) <= minDistance) {
                setNextNode();
            }
        }
    }
    
    //updates to the next node the object moves to
    private void setNextNode() {
        currentNode += (movingForward) ? 1 : -1;
        if (circuit) {
            currentNode = (currentNode + moveNodes.Length) % moveNodes.Length;
        } else {
            if (currentNode == 0 || currentNode == moveNodes.Length - 1) {
                movingForward = !movingForward;
            }
        }
        
    }
}
