using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMMXplayer : MonoBehaviour
{
    public GameObject name;
    public Vector3 V3 = new Vector3(0,1.5F,0);
    public GameObject HP;
    public Scrollbar XT;
    // Update is called once per frame
    void Start()
    {
        name.GetComponent<Text>().text = HP.GetComponent<playermove>().name;
    }
    void FixedUpdate()
    {
        if (GameObject.FindWithTag("playering") == null) return;
        name.GetComponent<Text>().text = HP.GetComponent<playermove>().name;
        transform.LookAt(new Vector3(GameObject.FindWithTag("playering").transform.position.x, transform.position.y, GameObject.FindWithTag("playering").transform.position.z));
        XT.size = HP.GetComponent<playermove>().HP/ HP.GetComponent<playermove>().HPMax;
    }
}
