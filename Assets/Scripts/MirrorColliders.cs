using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MirrorColliders : MonoBehaviour {
    
    private float screenWidth;
    List<ColliderData> colliderDataList = new List<ColliderData>();

    private struct ColliderData
    {
        public Collider2D originalCollider;
        public Collider2D leftCollider;
        public Collider2D rightCollider;
    }

	void Start () {
        screenWidth = GetScreenWidth();
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach(Collider2D coll in colliders)
        {
            ColliderData data = new ColliderData();
            data.originalCollider = coll;
            data.leftCollider = CopyComponent<Collider2D>(coll, gameObject);
            data.rightCollider = CopyComponent<Collider2D>(coll, gameObject);
            colliderDataList.Add(data);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    foreach(ColliderData data in colliderDataList)
        {
            Vector3 rotateOffset = new Vector3(screenWidth, 0);
            rotateOffset = Quaternion.Inverse(gameObject.transform.rotation) * rotateOffset;
            Vector2 rightOffset = new Vector2(rotateOffset.x, rotateOffset.y);
            Vector2 leftOffset = -rightOffset;
            data.leftCollider.offset = leftOffset;
            data.rightCollider.offset = rightOffset;
        }
	}

    private float GetScreenWidth()
    {
        Camera cam = Camera.main;

        Vector3 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        Vector3 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        
        return screenTopRight.x - screenBottomLeft.x;
    }

    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
}
