using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dramisson : MonoBehaviour
{
    public TextMesh Text;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.5F);
    }
    void FixedUpdate()
    {
        transform.forward = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
        transform.Translate(0, 1 * Time.deltaTime , 0);
    }
}
