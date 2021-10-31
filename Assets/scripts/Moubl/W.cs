using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class W : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject player;
    GameObject SWSJ;
    float X;
    float XX;
    Vector3 XYmove;
    void Start()
    {
        if(!networklangen.Moble)
        {
            gameObject.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        if(player == null)
        {
            return;
        }
        else if(!player.GetComponent<playermove>().AN.GetBool("Die") || player.GetComponent<playermove>() == null)
        {
            if (XX != 0)
            {
                player.GetComponent<playermove>().AN.SetFloat("Runing", Mathf.Abs(XX));
            }
            else if (X != 0)
            {
                player.GetComponent<playermove>().AN.SetFloat("Runing", Mathf.Abs(X));
            }
            player.GetComponent<playermove>().playerFlags.SimpleMove(transform.parent.parent.TransformDirection(XYmove * player.GetComponent<playermove>().Movespeed));
        }else
        {
            if(SWSJ==null) SWSJ = GameObject.FindWithTag("SWSJ");
            transform.Translate(XX * Time.deltaTime * 50, 0, X * Time.deltaTime * 50);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        X = 0;
        XX = 1;
        XYmove = new Vector3(0, 0, 1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        X = 0;
        XX = 0;
        XYmove = new Vector3(0, 0, 0);
    }
}
