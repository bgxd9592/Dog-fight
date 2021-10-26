using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMMX : MonoBehaviour
{
    GameObject ls;
    public Transform tagg;
    public Vector3 V3 = new Vector3(0,1,0);
    public GameObject HP;
    Scrollbar XT;
    public Image HPclor;
    // Update is called once per frame
    void Start()
    {
        ls = new GameObject("ls");
        transform.parent.parent = ls.transform;
        XT = GetComponent<Scrollbar>();
    }
    void FixedUpdate()
    {
        if (tagg != null)
        {
            ls.transform.position = tagg.position;
            transform.position = Camera.main.WorldToScreenPoint(ls.transform.position + V3);
        }
    }
}
