using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    void Update()
    {
        transform.position = new Vector3(0, player.position.y, -1); //获取主角Y轴信息,跟随主角升高
    }
}
