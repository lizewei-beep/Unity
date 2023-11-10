using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{

    public Transform target1;
    public Transform target2;
    public float smoothing;

    public Vector2 minPosition;
    public Vector2 maxPosition;

    public int disX;
    public int disY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (target1 != null && target2 != null) 
        {
            Vector3 targetPos = target1.position;
            Vector3 targetPos1 = target2.position;
            targetPos.x = Mathf.Clamp((targetPos.x + targetPos1.x) / 2 - disX, minPosition.x, maxPosition.x);
            targetPos.y = Mathf.Clamp((targetPos.y + targetPos1.y) / 2 - disY, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }

    public void SetCamPosLimit(Vector2 minPos, Vector2 maxPos)
    {
        minPosition = minPos;
        maxPosition = maxPos;
    }


}
