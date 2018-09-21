using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public float height;
    Vector3 pos = Vector3.zero;
    void Update()
    {
        if (gameObject.activeInHierarchy) //启用
        {
            if (pos == Vector3.zero) //获取目标位置
                pos = transform.position + Vector3.down * height;
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, pos) < 0.1)
        {
            transform.position = pos;
        }
    }
}
