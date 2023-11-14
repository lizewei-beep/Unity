using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgControl : MonoBehaviour
{
    public float Speed = 4;

    void Update()
    {
        if(Speed<=12)
            Speed += 1*Time.deltaTime/5;
        else if(Speed >= 12 && Speed <= 20)
            Speed += 1*Time.deltaTime/10;
        else
            Speed += 1*Time.deltaTime/15;
       foreach(Transform tran in transform)
        {
            Vector3 pos = tran.position;
            pos.x -= Speed*Time.deltaTime;
            if (pos.x < -645)
            {
                pos.x += 1290;

            }
            tran.position = pos;
        }

    }
    
}
