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
    private Vector3 widthVector = new Vector3(6.0f, 0.0f);

    public float difficulty = 1.0f;

    public GameObject startPrefab;
    public GameObject[] groundPrefabs;
    public GameObject crystalPrefab;
    public GameObject wrapEnemyPrefab;
    public float buildBufferHeight = 15.0f;

    public float crystalChance = 0.1f;

    // Use this for initialization
    void Start () {
        Random.seed = 0;
        DefineOffets();

        CreateFirstNode();
        for (int i = 0; i < 10; i++) {
            createNextNode();
        }
    }

	// Update is called once per frame
	void Update () {
        if (currentNode == null) {
            return;
        }
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
        widthVector = new Vector3(rightOffset - leftOffset, 0.0f);
    }

    void CreateFirstNode() {
        GameObject node = Instantiate(sceneNode, transform.position, transform.rotation) as GameObject;
        
        AddPrefabToNode(startPrefab, node, Vector2.zero);

        currentNode = node;
        nodes.Add(node);
    }

    void AddPrefabToNodeForWrap(GameObject prefab, GameObject node, Vector3 pos) {
        Vector3 widthVector = new Vector3(rightOffset - leftOffset, 0.0f);
        AddPrefabToNode(prefab, node, pos);
        AddPrefabToNode(prefab, node, pos + widthVector);
        AddPrefabToNode(prefab, node, pos - widthVector);
    }

    void AddPrefabToNode(GameObject prefab, GameObject node, float x, float y) {
        AddPrefabToNode(prefab, node, new Vector3(x, y));
    }

    void AddPrefabToNode(GameObject prefab, GameObject node, Vector3 pos) {
        if (prefab == null)
        {
            return;
        }
        GameObject obj = Instantiate(prefab, node.transform.position + pos, node.transform.rotation) as GameObject;
        obj.transform.parent = node.transform;
    }

    void createNextNode() {
        Vector3 nudgeRight = new Vector3(0.5f, 0);
        Vector3 crystalOffset = new Vector3(0, 1f, 0);
        Vector3 distanceFromLastNode = new Vector3(0, nodeDistanceHeight);

        GameObject node = Instantiate(sceneNode, currentNode.transform.position + distanceFromLastNode, transform.rotation) as GameObject;

        Vector3 newPosition = new Vector3(Random.Range(leftOffset, rightOffset), 0.0f);

        GameObject groundPrefab = groundPrefabs[Random.Range(0, groundPrefabs.Length)];
        AddPrefabToNodeForWrap(groundPrefab, node, newPosition);
        if (Random.value <= crystalChance)
        {
            //AddPrefabToNodeForWrap(crystalPrefab, node, newPosition + crystalOffset);
            //AddPrefabToNode(wrapEnemyPrefab, node, newPosition + crystalOffset);
        }
        AddWrapEnemy(node, newPosition);


        currentNode = node;
        nodes.Add(node);
    }

    void AddWrapEnemy(GameObject node, Vector3 tilePosition) {

        Vector3 crystalOffset = new Vector3(0, 1f, 0);
        AddPrefabToNode(wrapEnemyPrefab, node, tilePosition + crystalOffset);
    }

}
