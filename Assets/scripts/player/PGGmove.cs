using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

public class PGGmove : MonoBehaviourPun
{
    public GameObject SWSJ;
    CameraWork _cameraWork;
    // Start is called before the first frame update
    void Start()
    {
        _cameraWork = gameObject.GetComponent<CameraWork>();
        if (_cameraWork != null)
        {
            _cameraWork.OnStartFollowing();
        }
        SWSJ.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        _cameraWork.OnStartFollowing();
        float X = Input.GetAxis("Vertical");
        float XX = Input.GetAxis("Horizontal");
        float Y = Input.GetAxis("Mouse X");
        transform.Translate(XX* Time.deltaTime*50, 0, X* Time.deltaTime * 50);
        transform.Rotate(0, Y* Time.deltaTime * 50, 0);
        if(Input.GetKey(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(0);
        }
    }
}
