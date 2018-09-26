using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public float height; //移动的高度
    Vector3 pos; //移动的位置
    public Image[] sprites; //当前分数图片
    void OnEnable()
    {
        pos = transform.position + Vector3.down * height;
        ShowTopScore();
    }
    void Update()
    {
        if (gameObject.activeInHierarchy) //启用 
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 5);
    }
    void ShowTopScore() //显示最高分
    {
        if (PlayerPrefs.HasKey("最高分")) //获取最高分并显示
            new ScoreManager("Sprites/Number").ShowScore(PlayerPrefs.GetInt("最高分"), sprites);
    }
}
