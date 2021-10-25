using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMMXplayer : MonoBehaviour
{
    GameObject ls;
    public GameObject name;
    public Vector3 V3 = new Vector3(0,1.5F,0);
    public GameObject HP;
    Scrollbar XT;
    // Update is called once per frame
    void Start()
    {
        ls = new GameObject("ls");
        transform.parent.parent = ls.transform;
        XT = GetComponent<Scrollbar>();
        name.GetComponent<Text>().text = HP.GetComponent<playermove>().name;
    }
    void FixedUpdate()
    {
        if (HP != null)
        {
            name.transform.position = Camera.main.WorldToScreenPoint(HP.transform.position + V3);
            ls.transform.position = HP.transform.position;
            transform.position = Camera.main.WorldToScreenPoint(HP.transform.position + V3);
            XT.size = HP.GetComponent<playermove>().HP/ HP.GetComponent<playermove>().HPMax;
            if (HP.GetComponent<playermove>().HP <= 0) Destroy(transform.parent.parent.gameObject);
        }
        else if (HP == null)
        {
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
