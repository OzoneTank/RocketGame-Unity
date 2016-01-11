using UnityEngine;
using System.Collections;

public class WrapAI : MonoBehaviour {

    public enum Direction { North, East, South, West, None };
    public Direction currDirection = Direction.South;
    public float speed = 1.0f;
    public bool isClockWise = true;
    public float collisionCheckDistance = 0.1f;
    public string tagType = Constants.GROUND_TAG;
    
    Vector2 size = Vector2.zero;
    bool foundGround = false;
    
    // Use this for initialization
    void Start () {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
        if (boxCollider2D != null) {
            size = boxCollider2D.size;
        }
        if (circleCollider2D != null) {
            size = new Vector2(circleCollider2D.radius * 2, circleCollider2D.radius * 2);
        }
        size.x *= transform.localScale.x;
        size.y *= transform.localScale.y;
    }
	
	// Update is called once per frame
	void Update () {
        Direction directionToGo = currDirection;
        if (foundGround) {
            if (allDirectionsFree()) {
                directionToGo = getNextDirection(currDirection, true);
            } else {
                currDirection = getFreeDirection(currDirection);
                directionToGo = currDirection;
            }
        } else if (!directionIsFree(currDirection)) {
            currDirection = getNextDirection(currDirection, false);
            directionToGo = currDirection;
            foundGround = true;
        }
        transform.position += getDirectionVector3(directionToGo) * speed * Time.deltaTime;
        

    }

    Direction getOppositeDirection(Direction dir) {
        switch (dir) {
            case Direction.North:
                return Direction.South;
            case Direction.South:
                return Direction.North;
            case Direction.East:
                return Direction.West;
            case Direction.West:
                return Direction.East;
            default:
                return Direction.None;
        }
    }
    Direction getNextDirection(Direction dir, bool next) {
        if (next == isClockWise) {
            switch (dir) {
                case Direction.North:
                    return Direction.East;
                case Direction.South:
                    return Direction.West;
                case Direction.East:
                    return Direction.South;
                case Direction.West:
                    return Direction.North;
                default:
                    return Direction.None;
            }
        } else {
            switch (dir) {
                case Direction.North:
                    return Direction.West;
                case Direction.South:
                    return Direction.East;
                case Direction.East:
                    return Direction.North;
                case Direction.West:
                    return Direction.South;
                default:
                    return Direction.None;
            }
        }
    }

    Direction getFreeDirection(Direction dir) {
        Direction next = getNextDirection(dir, true);
        if (directionIsFree(next)) {
            return next;
        }
        Direction forward = dir;
        if (directionIsFree(forward)) {
            return forward;
        }
        Direction counter = getNextDirection(dir, false);
        if (directionIsFree(counter)) {
            return counter;
        }
        return getOppositeDirection(dir);
    }

    Vector3 getDirectionVector3(Direction dir) {
        switch(dir) {
            case Direction.North:
                return Vector3.up;
            case Direction.South:
                return Vector3.down;
            case Direction.East:
                return Vector3.right;
            case Direction.West:
                return Vector3.left;
            default:
                return Vector3.zero;
        }

    }

    bool allDirectionsFree() {
        return directionIsFree(Direction.North) &&
            directionIsFree(Direction.South) &&
            directionIsFree(Direction.East) &&
            directionIsFree(Direction.West);
    }

    bool directionIsFree(Direction dir) {
        float dist = (size.x / 2) + 0.0001f;
        Vector3 sideOffset1 = getDirectionVector3(getNextDirection(dir, true)) * (size.y / 2);
        Vector3 sideOffset2 = getDirectionVector3(getNextDirection(dir, false)) * (size.y / 2);
        if (dir == Direction.North || dir == Direction.South) {
            dist = (size.y / 2) + 0.0001f;
            sideOffset1 = getDirectionVector3(getNextDirection(dir, true)) * (size.x / 2);
            sideOffset2 = getDirectionVector3(getNextDirection(dir, false)) * (size.x / 2);
        }
        Vector3 rayDirection = getDirectionVector3(dir);
        RaycastHit2D hitCorner1 = Physics2D.Raycast(transform.position + sideOffset1 + rayDirection * dist, rayDirection, collisionCheckDistance);
        RaycastHit2D hitMid = Physics2D.Raycast(transform.position + rayDirection * dist, rayDirection, collisionCheckDistance);
        RaycastHit2D hitCorner2 = Physics2D.Raycast(transform.position + sideOffset2 + rayDirection * dist, rayDirection, collisionCheckDistance);
        return (hitMid.collider == null || hitMid.collider.tag != tagType) &&
            (hitCorner1.collider == null || hitCorner1.collider.tag != tagType) &&
            (hitCorner2.collider == null || hitCorner2.collider.tag != tagType);
    }
}
