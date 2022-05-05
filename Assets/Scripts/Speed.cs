using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public float speed=6.0f;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos=gameObject.transform.position;
        pos=new Vector3(pos.x+(Time.deltaTime*speed), pos.y, pos.z);
        gameObject.transform.position=pos;
    }
}
