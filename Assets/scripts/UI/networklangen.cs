using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class networklangen : MonoBehaviourPunCallbacks
{
    public GameObject texting;
    public GameObject ms;
    public GameObject loginUI;
    public GameObject nameUI;
    public GameObject LJSB;
    public GameObject HY;
    public GameObject GZan;
    public GameObject GZNR;
    public InputField roomName;
    public InputField playername;
    public static bool Moble = false;
    public Toggle Mobleing;
    public void Start()
    {
        if (PhotonNetwork.IsConnected) PhotonNetwork.LeaveRoom();
    }
    void FixedUpdate()
    {
        ms.GetComponent<Text>().text = PhotonNetwork.GetPing() + "ms";
        if (nameUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter)) playButton();
        }
        else if (loginUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter)) roomButton();
        }
    }
    public void mobleplay()
    {
        Moble = Mobleing.isOn;
    }
    public override void OnConnectedToMaster()
    {
        texting.SetActive(false);
        if (PhotonNetwork.NickName == "")
        {
            nameUI.SetActive(true);
        }
        else
        {
            loginUI.SetActive(true);
            HY.GetComponent<Text>().text = "欢迎  " + PhotonNetwork.NickName;
        }
        base.OnConnectedToMaster();
    }
    public void playButton()
    {
        if (playername.text.Length < 2 || playername.text.Length > 6) return;
        nameUI.SetActive(false);
        PhotonNetwork.NickName = playername.text;
        loginUI.SetActive(true);
        HY.GetComponent<Text>().text = "欢迎  " + PhotonNetwork.NickName;
    }
    public void loginshuru()
    {
        playername.text += "0";
    }
    public void roomshuru()
    {
        roomName.text += "0";
    }
    public void roomButton()
    {
        if (roomName.text.Length < 2|| roomName.text.Length > 6) return;
        loginUI.SetActive(false);
        RoomOptions options = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(roomName.text, options, default);
        texting.SetActive(true);
        Invoke("LSJBexit", 10);
    }
    public void LJSBexit()
    {
        LJSB.SetActive(true);
        texting.SetActive(false);
        Invoke("LJSBtext", 2);
    }
    public void LJSBtext()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
        base.OnJoinedRoom();
    }
    public void GZ()
    {
        GZan.SetActive(true);
        GZNR.SetActive(false);
    }
    public void GZK()
    {
        GZan.SetActive(false);
        GZNR.SetActive(true);
    }
    public void Mail()
    {
        Application.OpenURL("mailto:959208879@qq.com");
    }
    public void Github()
    {
        Application.OpenURL("https://github.com/bgxd9592/Dog-fight");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
