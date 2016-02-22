using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ShipStats : MonoBehaviour {

    public int health = 3;
	public Text heightStatusText;
	public Text timeStatusText;
	public GameObject healthPanel;

    private float maxHeight = 0.0f;

    private int itemsCollected = 0;
    private int totalItems = 0;
    private DateTime startTime;

    //private Rigidbody2D rb;

    void Start () {
        itemsCollected = 0;
        totalItems = GameObject.FindGameObjectsWithTag(Constants.ITEM_TAG).Length;
        startTime = DateTime.Now;
		SetHealth (health);
        //rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > maxHeight) {
            maxHeight = transform.position.y;
        }
		timeStatusText.text = "Time: " + Convert.ToInt16(DateTime.Now.Subtract(startTime).TotalSeconds);
            //statusText.text = "Health:" + health + " Items:" + itemsCollected + "/" + totalItems + " Time:" + Convert.ToInt16(timePassed.TotalSeconds);
		heightStatusText.text = "Max Height: " + Convert.ToInt16(maxHeight);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        switch (other.gameObject.tag)
        {
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
        ChangeHealth(-1);
    }

    void collectItem(GameObject item) {
        Destroy(item);
        itemsCollected++;
    }

    void reachedGoal(GameObject goal) {

    }

	public void SetHealth(int health) {
		this.health = health;
		if (this.health > 3) {
			this.health = 3;
		} else if (this.health < 0) {
			this.health = 0;
		}
		for (int i = 0; i < healthPanel.transform.childCount; i++) {
			Transform t = healthPanel.transform.GetChild(i);
			t.gameObject.SetActive (i < health);
		}
	}

	public void ChangeHealth(int inc) {
		SetHealth (health + inc);
	}
}
