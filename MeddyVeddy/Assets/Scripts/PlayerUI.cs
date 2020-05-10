using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hp;
    public GameObject score;
    GameObject player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            hp.GetComponent<Text>().text = "Health: " + player.GetComponent<Player>().GetHP();
            score.GetComponent<Text>().text = "Score: " + player.GetComponent<Player>().points;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if (GameObject.FindGameObjectsWithTag("Player").Length <= 0)
            {
                hp.GetComponent<Text>().text = "Health: " + 0;
            }
        }
    }
}
