using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAttle : MonoBehaviour
{
    //引入动画
    public Animator AN;
    //引入怪物
    public GameObject Turtle;
    void OnTriggerEnter(Collider other)
    {
        if(AN.GetBool("Attack") && (other.gameObject.tag == "Player"|| other.gameObject.tag == "playering"))
        {
            other.gameObject.GetComponent<playermove>().HP -= Turtle.GetComponent<TurtleShell>().AttackDage;
        }
    }
}
