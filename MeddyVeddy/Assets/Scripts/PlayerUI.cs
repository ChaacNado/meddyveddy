using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hp;
    public GameObject score;
    GameObject player = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = LoadMapStatic.player;
        }
        else
        {
            hp.GetComponent<Text>().text = "Health: " + player.GetComponent<Player>().GetHP();
            score.GetComponent<Text>().text = "Score: " + player.GetComponent<Player>().points;
        }
    }
}
