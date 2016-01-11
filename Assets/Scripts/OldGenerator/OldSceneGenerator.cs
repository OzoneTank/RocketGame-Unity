using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OldSceneGenerator : MonoBehaviour {

    public GameObject prefabNode;
    public GameObject prefabWall;
    List<GameObject> nodes = new List<GameObject>();
    //GameObject currentNode;

    float leftWidth = 3.0f;
    float rightWidth = 3.0f;
    float nodeDistance = 2.0f;
    float nextAngle = 0.0f;

    public float difficulty = 1.0f;
    
    // Use this for initialization
	void Start () {
        createFirstNode();
        for (int i = 0; i < 10; i++) {
            createNextNode();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void createFirstNode() {
        GameObject node = Instantiate(prefabNode, transform.position, transform.rotation) as GameObject;
        node.transform.GetChild(0).Translate(-leftWidth, 0.0f, 0.0f);
        node.transform.GetChild(1).Translate(rightWidth, 0.0f, 0.0f);

        buildWall(node.transform.GetChild(0).gameObject, node.transform.GetChild(1).gameObject);
        //currentNode = node;
        nodes.Add(node);
    }

    void createNextNode() {
        setNextStep();

        GameObject latestNode = nodes[nodes.Count - 1];
        GameObject node = Instantiate(prefabNode, latestNode.transform.position, latestNode.transform.rotation) as GameObject;

        //move walls
        node.transform.GetChild(0).Translate(-leftWidth, 0.0f, 0.0f);
        node.transform.GetChild(1).Translate(rightWidth, 0.0f, 0.0f);

        nodes.Add(node);


        node.transform.Translate(0, nodeDistance, 0);

        node.transform.RotateAround(latestNode.transform.position, Vector3.forward, latestNode.transform.rotation.z + nextAngle);


        //build walls
        buildWall(latestNode.transform.GetChild(0).gameObject, node.transform.GetChild(0).gameObject);
        buildWall(latestNode.transform.GetChild(1).gameObject, node.transform.GetChild(1).gameObject);

    }

    void setNextStep() {

        leftWidth = 3.0f;
        rightWidth = 3.0f;
        //nodeDistance = 1.0f;
        nextAngle = 5.0f;
    }

    void buildWall(GameObject first, GameObject second) {
        Vector3 currentPosition = first.transform.position;
        Vector3 lastPosition = second.transform.position;

        float height = prefabWall.GetComponent<Renderer>().bounds.size.y;

        do {
            GameObject newObject = Instantiate(prefabWall, currentPosition , first.transform.rotation) as GameObject;
            newObject.transform.parent = first.transform;
            currentPosition = Vector3.MoveTowards(currentPosition, lastPosition, height);
        } while (Vector3.Distance(currentPosition, lastPosition) != 0.0f);
        
    }

}
