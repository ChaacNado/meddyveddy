using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TreasureType
{
    point = 0,
    key = 1,
    loot = 2
}

public class TreasureScript : MonoBehaviour
{
    public GameObject Key;
    public Material mat;
    public int pointValue = 1;

    public RangedWeapon loot;

    float speed = 150f;
    float height = 0.5f;
    float startHeight;
    float angle = -90;
    float toDegrees = (float)Mathf.PI / 180;

    public int RoomID;
    public TreasureType type = TreasureType.point;
    // Start is called before the first frame update
    void Start()
    {
        startHeight = transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && type == TreasureType.point)
        {
            other.GetComponent<Player>().points += pointValue;
            Destroy(gameObject);
        }
        if (other.CompareTag("Player") && type == TreasureType.key)
        {
            other.GetComponent<Player>().Keys.Add(RoomID);

            Destroy(gameObject);
        }
        if (other.CompareTag("Player") && type == TreasureType.loot)
        {
            other.GetComponent<RangedWeaponController>().EquipWeapon(loot);

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (type != TreasureType.point)
        {
            angle += speed * Time.deltaTime;
            if (angle >= 360)
                angle = -360;
            transform.localPosition = new Vector3(transform.localPosition.x, startHeight + height * (1 + Mathf.Sin(angle * toDegrees)) / 2, transform.localPosition.z);
        }
       
    }

    public void Upgrade(TreasureType t)
    {
        type = t;
        if (t == TreasureType.key)
        {
            GameObject go = Instantiate(Key);
            go.transform.SetParent(transform);
            go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GetComponent<MeshRenderer>().material = mat;
        }
        if (t == TreasureType.loot)
        {
            transform.localScale *= 2;
        }    
    }
}
