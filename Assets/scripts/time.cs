using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class time : MonoBehaviourPun, IPunObservable
{
    public bool FJGB;
    public double SJJ;
    public float SJS;
    public bool HHKS;
    public GameObject KSZZ;//开始标志
    public int playerlist;
    public bool SFKS;
    bool one;
    GameObject player;
    void Start()
    {
        KSZZ = GameObject.FindWithTag("KSZZ");
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        playerlist = 0;
        HHKS = false;
        SFKS = false;
        one = false;
        SJS = 0;
        FJGB = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerlist>1 && !one)
        {
            player = GameObject.FindWithTag("playering");
            if (player != null) player.transform.position = new Vector3(Random.Range(162, 759), 0, Random.Range(256, 825));
            one = true;
            if (photonView.IsMine && PhotonNetwork.IsConnected)
            {
                SJJ = PhotonNetwork.Time + 300;
                SFKS = true;
            }
        }else if (SJS != 0 && playerlist <= 1)
        {
            player = GameObject.FindWithTag("playering");
            if (player != null) player.GetComponent<playermove>().LKYS();
        }
        if (KSZZ == null) KSZZ = GameObject.FindWithTag("KSZZ");
        if (SFKS)
        {
            if (PhotonNetwork.Time >= SJJ)
            {
                FJGB = true;
                KSZZ.GetComponent<Text>().text = "玩家伤害已启动!!";
                Invoke("KSZZExit", 3);
                if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
                HHKS = true;
                SJS = 0.000F;
                SFKS = false;
            }
            else if (photonView.IsMine && PhotonNetwork.IsConnected) SJS = (float)(SJJ - PhotonNetwork.Time);
        }
    }
    void KSZZExit()
    {
        KSZZ.SetActive(false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.SJJ);
            stream.SendNext(this.SJS);
            stream.SendNext(this.FJGB);
            stream.SendNext(this.HHKS);
            stream.SendNext(this.SFKS);
            stream.SendNext(this.playerlist);
        }
        else
        {
            this.SJJ = (double)stream.ReceiveNext();
            this.SJS = (float)stream.ReceiveNext();
            this.FJGB = (bool)stream.ReceiveNext();
            this.HHKS = (bool)stream.ReceiveNext();
            this.SFKS = (bool)stream.ReceiveNext();
            this.playerlist = (int)stream.ReceiveNext();
        }
    }
}
