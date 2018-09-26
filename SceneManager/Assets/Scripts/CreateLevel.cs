using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    Transform[] createPoints; //所有生成点
    public Transform player; //主角
    public GameObject enemy, milk; //敌人和牛奶预制体
    bool createType = false; //根据值来决定生成敌人还是牛奶
    float height = 3; //生成物体间隔
    void Start()
    {
        GetCreatePoints();
    }
    void GetCreatePoints() //获取所有生成点
    {
        createPoints = new Transform[transform.childCount];
        for (int i = 0; i < createPoints.Length; i++)
        {
            createPoints[i] = transform.GetChild(i);
        }
    }
    void Update()
    {
        if (player.position.y > height)
        {
            CreateObj(); //生成物体
            height += 3; //每走3米,生成一次物体
        }
    }
    void CreateObj() //根据bool值判断生成物体种类
    {
        if (createType)
            CreateEnemy();
        else
            CreateMilk();
        createType = !createType; //bool值取反
    }
    void CreateEnemy() //生成敌人
    {
        int index = Random.Range(0, createPoints.Length); //该索引处不生成敌人
        for (int i = 0; i < createPoints.Length; i++)
        {
            if (i != index && Random.Range(0, 10) > 0)//90%生成敌人
                Instantiate(enemy, createPoints[i].position + Vector3.up * height, Quaternion.identity);
        }
    }
    void CreateMilk() //生成牛奶
    {
        for (int i = 0; i < createPoints.Length; i++)
        {
            if (Random.Range(0, 10) > 8) //10%在该生成点生成牛奶
                Instantiate(milk, createPoints[i].position + Vector3.up * height, Quaternion.identity);
        }
    }
}
