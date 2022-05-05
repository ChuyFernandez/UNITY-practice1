using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOutOfRange : MonoBehaviour
{
    private float limPosX=18;
    void Start()
    {

    }

    void Update()
    {
        Vector3 pos=gameObject.transform.position;
        if(pos.x > limPosX) Destroy(gameObject);  
    }
}
