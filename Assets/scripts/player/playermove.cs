using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

public class playermove : MonoBehaviourPun, IPunObservable
{
    float curtTime = 0;
    float lastTime = 0;
    bool isRendering;
    public GameObject XTXTXTXT;//引入他人看自己的血条
    public string name;
    public int old;//分数
    public int SRS;//杀人数
    public Text Timeing;//引入时间
    public Text olding;
    //引入角色控制器
    CharacterController playerFlags;
    //引入剑的游戏对象
    public GameObject JIAN;
    //生命值相关
    public Scrollbar HPllbar;
    public Animation HPDIDI;
    public float HP;
    public bool Die;
    public float HPMax;
    public Text HPTTT;
    public Canvas Canvas;
    public Text ms;
    //移动速度相关
    float Movespeed;
    float rotSpeed;
    //伤害相关
    public float AttackDage;
    //动画相关
    public Animator AN;
    bool oldYN;
    public GameObject SWshijiao;
    PhotonView pv;
    bool GGG;
    //初始化
    void Start()
    {
        AN = GetComponent<Animator>();
        playerFlags = GetComponent<CharacterController>();
        if (photonView.IsMine && PhotonNetwork.IsConnected)
        {
            name = PhotonNetwork.NickName;
        }
        else
        {
            XTXTXTXT.SetActive(true);
            return;
        }
        CameraWork _cameraWork = gameObject.GetComponent<CameraWork>();
        if (_cameraWork != null)
        {
            if (photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }
        pv = this.GetComponent<PhotonView>();
        pv.RPC("playlistXR", RpcTarget.All, "+");
        Die = false;
        oldYN = false;
        old = 0;
        SRS = 1;
        Canvas.gameObject.SetActive(true);
        gameObject.tag = "playering";
        AttackDage = 3.5F;
        HPMax = 100F;
        HP = HPMax;
        Movespeed = 10;
        rotSpeed = 50;
        GGG = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    //参数执行
    void FixedUpdate()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        if (HP < 0) SW();
        ms.text = PhotonNetwork.GetPing() + "ms";
        if (Time.time % 3 == 0) SGG();
        if(GameObject.FindWithTag("Time") != null)
        {
            if (GameObject.FindWithTag("Time").GetComponent<time>().playerlist <= 1)
            {
                Timeing.text = "等待玩家加入";
                if(GameObject.FindWithTag("Time").GetComponent<time>().SJJ !=0 || GameObject.FindWithTag("Time").GetComponent<time>().HHKS) LKYS();
            }
            else
            {
                Timeing.text = "" + GameObject.FindWithTag("Time").GetComponent<time>().SJS;
                if (GameObject.FindWithTag("Time").GetComponent<time>().SJS == 0.000F) Timeing.text = "战斗";
                if ((SRS == GameObject.FindWithTag("Time").GetComponent<time>().playerlist || GameObject.FindWithTag("Time") == null)) LKYS();
            }
        }else LKYS();
        if (HP > 0)
        {
            if (HP > HPMax) HP = HPMax;
            if(GGG) olding.text = "当前分数:" + old;
            HPMove();
            YD();
            GJ();
        }
        else
        {
            HPMove();
        }
        if (GameObject.FindWithTag("Time").GetComponent<time>().HHKS) oldXX();
    }
    public void LKYS()
    {
        GGG = false;
        if (GameObject.FindWithTag("Time") == null)
        {
            olding.text = "由于房主退出，稍后将返回主界面";
            Invoke("YCFH", 3);
        }
        else if(SRS == GameObject.FindWithTag("Time").GetComponent<time>().playerlist)
        {
            HP = HPMax;
            olding.text = "你胜利了，稍后将返回主界面";
            Invoke("YCFH", 3);
        }
        else 
        {
            olding.text = "未知错误，稍后返回主界面";
            Invoke("YCFH", 3);
        }
    }
    void YCFH()
    {
        pv.RPC("FH", RpcTarget.AllViaServer, null);
    }
    //返回主菜单
    [PunRPC]
    void FH()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
    //写入playerlist
    [PunRPC]
    void playlistXR(string c)
    {
        if(c == "+")
        {
            GameObject.FindWithTag("Time").GetComponent<time>().playerlist++;
        }
        else if(c == "-")
        {
            GameObject.FindWithTag("Time").GetComponent<time>().playerlist--;
        }
    }
    void SWsj()
    {
        Canvas.gameObject.SetActive(false);
        Instantiate(SWshijiao, transform.position, transform.rotation);
        gameObject.GetComponent<playermove>().enabled = false;
        gameObject.GetComponent<CameraWork>().enabled = false;
    }
    //分数转换
    public void oldXX()
    {
        if(!oldYN)
        {
            HPMax += old;
            AttackDage += old * 0.1F;
            Movespeed += old * 0.01F;
            HP = HPMax;
            oldYN = true;
        }
    }
    //数据同步
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.old);
            stream.SendNext(this.SRS);
            stream.SendNext(this.name);
            stream.SendNext(this.HPMax);
            stream.SendNext(this.Movespeed);
            stream.SendNext(this.AttackDage);
            stream.SendNext(this.HP);
        }
        else
        {
            this.old = (int)stream.ReceiveNext();
            this.SRS = (int)stream.ReceiveNext();
            this.name = (string)stream.ReceiveNext();
            this.HPMax = (float)stream.ReceiveNext();
            this.Movespeed = (float)stream.ReceiveNext();
            this.AttackDage = (float)stream.ReceiveNext();
            this.HP = (float)stream.ReceiveNext();
        }
    }
    //(临时)怪物生成
    void SGG()
    {
        if (GameObject.FindWithTag("Time").GetComponent<time>().playerlist > 1)
        {
            PhotonNetwork.Instantiate("TurtleShell", new Vector3(Random.Range(399, 607), 0, Random.Range(372, 618)), Quaternion.identity);
        }
    }
    //移动相关
    void YD()
    {
        float X = Input.GetAxis("Vertical");
        float XX = Input.GetAxis("Horizontal");
        Vector3 XYmove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float Y = Input.GetAxis("Mouse X");
        if (XX != 0)
        {
            AN.SetFloat("Runing", Mathf.Abs(XX));
        }
        else
        {
            AN.SetFloat("Runing", Mathf.Abs(X));
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            AN.SetBool("Defend", true);
            Movespeed = 5;
        }
        else
        {
            AN.SetBool("Defend", false);
            Movespeed = 10;
        }
        playerFlags.SimpleMove(transform.TransformDirection(XYmove * Movespeed));
        transform.Rotate(0, Y * rotSpeed * Time.deltaTime, 0);
       
    }
    //攻击相关
    void GJ()
    {
         if (!AN.GetBool("Attack")&& !AN.GetBool("Defend"))
         {
             if (Input.GetKey(KeyCode.Mouse0))
             {
                AN.SetBool("Attack", true);
                Movespeed = 3;
                Invoke("GJHFA", 0.4F);
             }
         }
    }
    //攻击恢复相关
    void GJHFA()
    {
        AN.SetBool("Attack", false);
        Movespeed = 10;
    }
    //血量显示
    void HPMove()
    {
        HPllbar.size = HP / HPMax;
        HPTTT.text = HP + " / " + HPMax;
        if (HP<HPMax*0.1 && HP !=0)
        {
            HPDIDI.Play();
        }
        else HPDIDI.Stop();
    }
    //死亡处理
    void SW()
    {
        AN.SetBool("Attack", false);
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            gameObject.tag = "Die";
            return;
        }
        HP = 0;
        AN.SetBool("Die", true);
        gameObject.tag = "Die";
        Die = true;
        pv.RPC("playlistXR", RpcTarget.All, "-");
        SWsj();
    }
    //攻击音效
    void GJYX()
    {
        JIAN.GetComponent<playDarge>().GJYX();
    }
}
