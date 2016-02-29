using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class MirrorColliders : MonoBehaviour {
    
    private float screenWidth;
    List<ColliderData> colliderDataList = new List<ColliderData>();

    private struct ColliderData
    {
        public Collider2D originalCollider;
        public Collider2D leftCollider;
        public Collider2D rightCollider;
    }

    public bool IsStatic = false;
    private GameObject leftObj;
    private GameObject rightObj;

    private GameObject DublicateGameObj(Vector3 offset) {
        GameObject cloneObj = Instantiate(gameObject, transform.position + offset, transform.rotation) as GameObject;
        cloneObj.layer = LayerMask.NameToLayer(Constants.LAYER_REFLECTION);

        MonoBehaviour[] leftComponents = cloneObj.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour comp in leftComponents) {
            comp.enabled = false;
        }
        return cloneObj;
    }

	void Start () {
        screenWidth = GetScreenWidth();
        Vector3 screenOffset = new Vector3(screenWidth, 0);
        leftObj = DublicateGameObj(-screenOffset);
        rightObj = DublicateGameObj(screenOffset);
        leftObj.transform.parent = gameObject.transform;
        rightObj.transform.parent = gameObject.transform;

        if (IsStatic) {
            enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 posOffset = new Vector2(transform.position.x, transform.position.y);
        Vector3 screenOffset = new Vector3(screenWidth, 0);
        leftObj.transform.position = transform.position + screenOffset;
        rightObj.transform.position = transform.position - screenOffset;
        leftObj.transform.rotation = transform.rotation;
        rightObj.transform.rotation = transform.rotation;
    }

    private float GetScreenWidth()
    {
        Camera cam = Camera.main;

        Vector3 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        Vector3 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        
        return screenTopRight.x - screenBottomLeft.x;
    }
}
