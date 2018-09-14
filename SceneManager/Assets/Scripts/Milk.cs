using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : MonoBehaviour
{
    int number; //长高数量
    void OnEnable()
    {
        number = Random.Range(1, 10);
        transform.GetComponentInChildren<TextMesh>().text = number.ToString();
    }
    void OnTriggerEnter2D() //触发时调用一次
    {
        GrowTaller();     
    }
    void GrowTaller() //牺牲自己,令主角长高
    {
        BodyManager bm = FindObjectOfType<BodyManager>(); //找到身体管理器脚本
        for (int i = 0; i < number; i++)
        {
            FindObjectOfType<BodyManager>().AddBody(); //根据长高数量增加肢节
        }
        Destroy(gameObject); //销毁自身
    }
}
