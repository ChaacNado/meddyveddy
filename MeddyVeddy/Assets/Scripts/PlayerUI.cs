using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject hpBar;
    public GameObject Points;
    GameObject currentHpBar;
    GameObject currentPoints;
    float fullHP;
    Canvas c;
    // Start is called before the first frame update
    void Start()
    {
        c = FindObjectOfType<Canvas>();
        //currentHpBar = Instantiate(hpBar, c.transform);
        //currentHpBar.transform.parent = c.transform;
        //currentPoints = Instantiate(Points, c.transform);
        //currentPoints.transform.parent = c.transform;
        //fullHP = gameObject.GetComponent<Player>().startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 newPos = new Vector3(c. ,0,0);
        Points.GetComponentInChildren<Text>().text = "Points: " + gameObject.GetComponent<Player>().points;



    }
}
