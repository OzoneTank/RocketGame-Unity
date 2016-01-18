using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ShipStats : MonoBehaviour {

    public int health = 1;
    public Text statusText;

    private float maxHeight = 0.0f;

    private int itemsCollected = 0;
    private int totalItems = 0;
    private DateTime startTime;

    //private Rigidbody2D rb;

    void Start () {
        itemsCollected = 0;
        totalItems = GameObject.FindGameObjectsWithTag(Constants.ITEM_TAG).Length;
        startTime = DateTime.Now;
        //rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > maxHeight) {
            maxHeight = transform.position.y;
        }
        if (statusText) {
            TimeSpan timePassed = DateTime.Now.Subtract(startTime);
            //statusText.text = "Health:" + health + " Items:" + itemsCollected + "/" + totalItems + " Time:" + Convert.ToInt16(timePassed.TotalSeconds);
            statusText.text = "Max Height:" + Convert.ToInt16(maxHeight);
        }
	}


    void OnTriggerEnter2D(Collider2D other) {
        switch(other.tag) {
            case "Enemy":
                damagePlayer(other.gameObject);
                break;
            case "CollectItem":
                collectItem(other.gameObject);
                break;
            case "Goal":
                reachedGoal(other.gameObject);
                break;
        }
    }

    void damagePlayer(GameObject enemy) {
        health--;
    }

    void collectItem(GameObject item) {
        Destroy(item);
        itemsCollected++;
    }

    void reachedGoal(GameObject goal) {

    }
}
