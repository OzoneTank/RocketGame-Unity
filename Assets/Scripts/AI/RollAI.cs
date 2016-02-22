using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class RollAI : MonoBehaviour {
    Rigidbody2D rb;
    public float speed = 1.0f;
    public Vector2 direction = new Vector2(0, -1);
    private int points = 0;
    public float stuckRotateSpeed = 45.0f;
    public RollStyle rollStyle;

    public float changeDirectionAngle = 90.0f;

    public string[] ignoreTags;

    private Collider2D firstCollider;

    public enum RollStyle
    {
        FlatFooted,
        Static,
        Roll
    }

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        firstCollider = GetComponents<Collider2D>()[0];
	}

    void Update()
    {
        if (rb.velocity.magnitude < (speed / 10.0f))
        {
            direction = Rotate(direction, stuckRotateSpeed * Time.deltaTime);
        }
        rb.velocity = direction.normalized * speed;
        Vector3 end = new Vector3(transform.position.x + direction.normalized.x, transform.position.y + direction.normalized.y, 0);
        Debug.DrawLine(transform.position, end, Color.blue, 0.1f);
        SetRotation();
    }

    private void SetRotation()
    {
        switch(rollStyle)
        {
            case RollStyle.FlatFooted:
//                rb.rotation = 0.0f;
//                float angle = Mathf.Atan2(rb.velocity.normalized.y, rb.velocity.normalized.x) * Mathf.Rad2Deg;
//                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
//                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1);
                break;
            case RollStyle.Static:
                rb.rotation = 0.0f;
                break;
            case RollStyle.Roll:
                // You're perfect the way you are. never change.
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (IsIgnoreTag(coll.gameObject.tag)) {
            return;
        }
        
        direction = Rotate(-getLastNormal(coll), changeDirectionAngle);
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (IsIgnoreTag(coll.gameObject.tag))
        {
            return;
        }
        
        direction = -getLastNormal(coll);

    }

    private bool IsIgnoreTag(string tag)
    {
        foreach(string s in ignoreTags)
        {
            if (tag == s) {
                return true;
            }
        }
        return false;
    }

    private Vector2 getLastNormal(Collision2D coll) {
        return coll.contacts[coll.contacts.Length - 1].normal;
    }

    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}
