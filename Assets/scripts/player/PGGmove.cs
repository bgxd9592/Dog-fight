using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

public class PGGmove : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        CameraWork _cameraWork = gameObject.GetComponent<CameraWork>();
        if (_cameraWork != null)
        {
            if (photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float X = Input.GetAxis("Vertical");
        float XX = Input.GetAxis("Horizontal");
        float Y = Input.GetAxis("Mouse X");
        transform.Translate(X* Time.deltaTime*50, 0, XX* Time.deltaTime * 50);
        transform.Rotate(0, Y* Time.deltaTime * 50, 0);
    }
}
