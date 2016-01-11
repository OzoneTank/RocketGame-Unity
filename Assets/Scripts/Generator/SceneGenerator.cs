using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneGenerator : MonoBehaviour {

    public GameObject sceneNode;
    public GameObject player;

    List<GameObject> nodes = new List<GameObject>();
    GameObject currentNode;

    private float leftOffset = -3.0f;
    private float rightOffset = 3.0f;
    private float nodeDistanceHeight = 6.0f;

    public float difficulty = 1.0f;

    public GameObject ground;
    public float buildBufferHeight = 15.0f;


    // Use this for initialization
    void Start () {
        DefineOffets();

        CreateFirstNode();
        for (int i = 0; i < 10; i++) {
            createNextNode();
        }
    }

	// Update is called once per frame
	void Update () {
	    if (player.transform.position.y > currentNode.transform.position.y + buildBufferHeight) {
            createNextNode();
        }
	}

    private void DefineOffets() {
        Camera cam = Camera.main;
        Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        Vector3 bottomRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

        leftOffset = topLeft.x;
        rightOffset = bottomRight.x;
        buildBufferHeight = (topLeft.y - bottomRight.y) * 2;
    }

    void CreateFirstNode() {
        Vector3 nudgeRight = new Vector3(0.5f, 0);
        GameObject node = Instantiate(sceneNode, transform.position, transform.rotation) as GameObject;

        Vector3 unit = new Vector3(1, 0);
        for (int i = 0; i < 6; i++) {
            AddPrefabToNode(ground, node, -nudgeRight + (unit * i));
            AddPrefabToNode(ground, node, nudgeRight - (unit * i));
        }

        currentNode = node;
        nodes.Add(node);
    }

    GameObject AddPrefabToNode(GameObject prefab, GameObject node, float x, float y) {
        return AddPrefabToNode(prefab, node, new Vector3(x, y));
    }
    GameObject AddPrefabToNode(GameObject prefab, GameObject node, Vector3 pos) {
        GameObject obj = Instantiate(prefab, node.transform.position + pos, node.transform.rotation) as GameObject;
        obj.transform.parent = node.transform;

        return obj;
    }

    void createNextNode() {
        Vector3 nudgeRight = new Vector3(0.5f, 0);
        Vector3 distanceFromLastNode = new Vector3(0, nodeDistanceHeight);

        GameObject node = Instantiate(sceneNode, currentNode.transform.position + distanceFromLastNode, transform.rotation) as GameObject;

        Vector3 newPosition = new Vector3(Random.Range(leftOffset, rightOffset), 0.0f);
        Vector3 widthVector = new Vector3(rightOffset - leftOffset, 0.0f);

        AddPrefabToNode(ground, node, newPosition + -nudgeRight);
        AddPrefabToNode(ground, node, newPosition + nudgeRight);
        AddPrefabToNode(ground, node, newPosition + -nudgeRight + widthVector);
        AddPrefabToNode(ground, node, newPosition + nudgeRight + widthVector);
        AddPrefabToNode(ground, node, newPosition + -nudgeRight - widthVector);
        AddPrefabToNode(ground, node, newPosition + nudgeRight - widthVector);
        currentNode = node;
        nodes.Add(node);
    }

}
