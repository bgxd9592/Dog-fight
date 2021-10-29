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
        C = -1;
    }
    void FixedUpdate()
    {
        if (GameObject.FindWithTag("Time") == null && AudioSou.clip != QS)
        {
            AudioSou.clip = QS;
            AudioSou.Play();
            
        }else if (GameObject.FindWithTag("Time") == null) return;
        if (GameObject.FindWithTag("Time") != null) C = (int)GameObject.FindWithTag("Time").GetComponent<time>().SJS;
        if (C > 50)
        {
            if (AudioSou.clip == QS) return;
            AudioSou.clip = QS;
            AudioSou.Play();
        }else
        if (C <= 50 && C > 0)
        {
            if (AudioSou.clip == KS) return;
            AudioSou.clip = KS;
            AudioSou.Play();
        }else
        if (C == 0)
        {
            if (AudioSou.clip == RAN) return;
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
