using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BJSwitch : MonoBehaviour
{
    public Transform player;
    public Transform other;
    public bool up=false;
	void Start ()
    {
		
	}
    void Update()
    {
        if (player.position.y > transform.position.y&&other.position.y<transform.position.y) //如果主角比自己高,另一个比自己低
            other.position += Vector3.up * 40; //让另一个背景板升高
    }
}
