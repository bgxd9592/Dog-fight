using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurtleShell : MonoBehaviourPun, IPunObservable
{
    //引入敌对标识符
    public GameObject DIDI;
    //引入玩家
    public GameObject player;
    //定义血量差值
    public float HPCC;
    //定义血量
    public float HP;
    //定义动画
    public Animator Shell;
    //定义刚体
    Rigidbody TurtleS;
    //定义血量上限
    float HPMax;
    //定义攻击力
    public float AttackDage;
    //定义移动速度
    float Movespeed;
    void Start()
    {
        TurtleS = GetComponent<Rigidbody>();
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        HP = 10F;
        HPMax = 10F;
        DIDI.SetActive(false);
        Movespeed = 10F;
        AttackDage = 3F;
        gameObject.tag = "memm";
        player = GameObject.FindWithTag("Player");
        transform.Translate(Random.Range(-5, 5), 0 ,Random.Range(-5, 5));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        HPJC();
        if (player != null)
        {
            if (!player.GetComponent<playermove>().Die)
            {
                ZDXD();
                HPCC = HP / HPMax;
            }else player = GameObject.FindWithTag("Player");
        }
    }
    void HPJC()
    {
        if(HP <= 0 && photonView.IsMine)
        {
            Shell.SetBool("Die", true);
            gameObject.tag = "Die";
            TurtleS.detectCollisions = false;
            TurtleS.useGravity = false;
            Invoke("Diedelete", 3);
        }
        if(HP <= 0 && !photonView.IsMine)
        {
            gameObject.tag = "Die";
            TurtleS.detectCollisions = false;
            TurtleS.useGravity = false;
        }
    }

    void Diedelete()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    void ZDXD()
    {
        if(PTJL(transform,player.transform)<50F&&!Shell.GetBool("Die"))
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y,player.transform.position.z));
            if (PTJL(transform, player.transform) < 20F)
            {
                transform.Translate(0, 0, Movespeed * Time.deltaTime);
                Shell.SetBool("Runing", true);
                if (PTJL(transform, player.transform) < 1.7F)
                {
                    Movespeed = 0;
                    Shell.SetBool("Runing", false);
                    if (!Shell.GetBool("Attack"))
                    {
                        Shell.SetBool("Attack", true);
                        Invoke("GJHF", 0.5F);
                    }
                }
                else Movespeed = 10F;
            }
            else
            {
                Shell.SetBool("Runing", false);
            }
        }
    }
    void GJHF()
    {
        Shell.SetBool("Attack", false);
    }
    float PTJL(Transform t1,Transform t2)
    {
        return Vector3.Distance(t1.position, t2.position);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.HPMax);
            stream.SendNext(this.Movespeed);
            stream.SendNext(this.AttackDage);
            stream.SendNext(this.HP);
        }
        else
        {
            this.HPMax = (float)stream.ReceiveNext();
            this.Movespeed = (float)stream.ReceiveNext();
            this.AttackDage = (float)stream.ReceiveNext();
            this.HP = (float)stream.ReceiveNext();
        }
    }
}
