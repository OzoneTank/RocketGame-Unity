using UnityEngine;
using System.Collections;

public class DrawGizmo : MonoBehaviour {

    public Color color = Color.yellow;

    void OnDrawGizmos() {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
