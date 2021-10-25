using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DDLJ", 3);
    }
    void DDLJ()
    {
        if (GameObject.FindWithTag("Time") == null)
        {
            PhotonNetwork.Instantiate("Time", new Vector3(0, 0, 0), Quaternion.identity, 0);
            if (GameObject.FindWithTag("Time").GetComponent<time>().FJGB)
            {
                Invoke("LKYX", 3);
            }
            else PhotonNetwork.Instantiate("DogPBR", new Vector3(Random.Range(162, 759), 0, Random.Range(256, 825)), Quaternion.identity, 0);
        }
        else if (GameObject.FindWithTag("Time").GetComponent<time>().FJGB)
        {
            Invoke("LKYX", 3);
        }
        else PhotonNetwork.Instantiate("DogPBR", new Vector3(Random.Range(162, 759), 0, Random.Range(256, 825)), Quaternion.identity, 0);
    }
    void LKYX()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

}
