using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    static Audio _instance;
    AudioSource AudioSou;
    public AudioClip QS;
    public AudioClip KS;
    public AudioClip RAN;
    public int C;
    void Start()
    {
        AudioSou = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if (GameObject.FindWithTag("Time") == null && AudioSou.clip != QS)
        {
            AudioSou.clip = QS;
            AudioSou.Play();
            
        }else if (GameObject.FindWithTag("Time") == null) return;
        C = (int)GameObject.FindWithTag("Time").GetComponent<time>().SJS;
        if (C > 50 && AudioSou.isPlaying != QS)
        {
            AudioSou.clip = QS;
            AudioSou.Play();
        }
        if (C <= 50 && C > 0 && AudioSou.clip != KS)
        {
            AudioSou.clip = KS;
            AudioSou.Play();
        }
        if (C == 0 && AudioSou.clip != RAN)
        {
            Debug.Log("执行");
            AudioSou.clip = RAN;
            AudioSou.Play();
        }
    }
    public static Audio instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Audio>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != _instance)
        {
            Destroy(gameObject);
        }
    }
    }
