using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playDarge : MonoBehaviourPun,IPunObservable
{
    //引入碰撞者
    public GameObject PZZ;
    GameObject[] PZZSZ;
    public int PZZID;
    //导入伤害字体 
    public GameObject Mission;
    //引入玩家
    public GameObject player;
    //引入动画
    public Animator AN;
    //引入音效
    AudioSource Audio;
    //引入音频
    public AudioClip cutting;
    public AudioClip Jizhong;
    //攻击命中判定
    bool GJMZ = false;
    PhotonView pv;
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        pv = this.GetComponent<PhotonView>();
    }
    void OnTriggerEnter(Collider Collider)
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        if (Collider.gameObject.name == "Body")
        {
            PZZ = Collider.gameObject.transform.parent.parent.gameObject;
            PZZID = Collider.gameObject.transform.parent.parent.gameObject.GetComponent<PhotonView>().ViewID;
        }
        else if (Collider.gameObject.tag != "playering")
        {
            PZZ = Collider.gameObject;
            PZZID = Collider.gameObject.GetComponent<PhotonView>().ViewID;
        }
        if (PZZ == null) return;
        if (AN.GetBool("Attack") && PZZ.tag == "memm")
        {
            PZZ.GetComponent<TurtleShell>().HP -= player.GetComponent<playermove>().AttackDage;
            player.GetComponent<playermove>().old -= 1;
            if (PZZ.GetComponent<TurtleShell>().HP <= 0)
            {
                player.GetComponent<playermove>().HP += 1;
                player.GetComponent<playermove>().old -= 1;
            }
            GameObject Dramission = Instantiate(Mission, PZZ.transform.position, PZZ.transform.rotation);
            Dramission.transform.eulerAngles += new Vector3(0, 180, 0);
            Dramission.transform.position += new Vector3(0, 0.5F, 0);
            Dramission.GetComponent<Dramisson>().Text.text = "" + player.GetComponent<playermove>().AttackDage;
            GJMZ = true;
        }
        else if (AN.GetBool("Attack") && PZZ.tag == "MM")
        {
            pv.RPC("GJ", RpcTarget.Others, 1);
            PZZ.GetComponent<TurtleShell>().HP -= player.GetComponent<playermove>().AttackDage;
            if (PZZ.GetComponent<TurtleShell>().HP <= 0)
            {
                player.GetComponent<playermove>().HP += 3;
                player.GetComponent<playermove>().old += 1;
            }
            GameObject Dramission = Instantiate(Mission, PZZ.transform.position, PZZ.transform.rotation);
            PZZ.GetComponent<TurtleShell>().player = player;
            Dramission.transform.eulerAngles += new Vector3(0, 180, 0);
            Dramission.transform.position += new Vector3(0, 0.5F, 0);
            Dramission.GetComponent<Dramisson>().Text.text = "" + player.GetComponent<playermove>().AttackDage;
            GJMZ = true;
        }
        else if (AN.GetBool("Attack") && (PZZ.tag == "Player" && PZZ.transform != transform.parent.parent.parent) && GameObject.FindWithTag("Time").GetComponent<time>().HHKS)
        {
            pv.RPC("GJ", RpcTarget.Others, 2);
            PZZ.GetComponent<playermove>().HP -= player.GetComponent<playermove>().AttackDage;
            GameObject Dramission = Instantiate(Mission, PZZ.transform.position, PZZ.transform.rotation);
            Dramission.transform.eulerAngles += new Vector3(0, 180, 0);
            Dramission.transform.position += new Vector3(0, 0.5F, 0);
            Dramission.GetComponent<Dramisson>().Text.text = "" + player.GetComponent<playermove>().AttackDage;
            GJMZ = true;
        }
        
    }
    [PunRPC]
    void GJ(int i)
    {
        if (i==1)
        {
            PZZSZ = GameObject.FindGameObjectsWithTag("memm");
            foreach (GameObject PZZSS in PZZSZ)
            {
                if (PZZSS.GetComponent<PhotonView>().ViewID == PZZID)
                {
                    PZZ = PZZSS;
                    PZZ.GetComponent<TurtleShell>().HP -= player.GetComponent<playermove>().AttackDage;
                    break;
                }
            }
        }
        else if(i==2)
        {
            PZZSZ = GameObject.FindGameObjectsWithTag("playering");
            foreach (GameObject PZZSS in PZZSZ)
            {
                if (PZZSS.GetComponent<PhotonView>().ViewID == PZZID)
                {
                    PZZ = PZZSS;
                    PZZ.GetComponent<playermove>().HP -= player.GetComponent<playermove>().AttackDage;
                    break;
                }
            }
        }
    }
    //攻击播放音效
    public void GJYX()
    {
        if (GJMZ)
        {
            Audio.clip = Jizhong;
            Audio.Play();
            GJMZ = false;
        }
        else
        {
            Audio.clip = cutting;
            Audio.Play();
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.PZZID);
        }
        else
        {
            this.PZZID = (int)stream.ReceiveNext();
        }
    }
}
