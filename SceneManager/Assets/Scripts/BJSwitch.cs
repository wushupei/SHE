using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BJSwitch : MonoBehaviour
{
    public Transform player; //主角
    public Transform other; //另一张背景
    void Update()
    {
        if (player.position.y > transform.position.y && other.position.y < transform.position.y) //如果主角比自己高,另一个比自己低
            other.position += Vector3.up * 40; //让另一个背景板升高
    }
}
