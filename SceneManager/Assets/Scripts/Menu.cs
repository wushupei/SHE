using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public float height;
    Vector3 pos;
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
        if (Vector3.Distance(transform.position, pos) < 0.1) //接近时吸附
            transform.position = pos;
    }
    void ShowTopScore() //显示最高分
    {
        int topScore = 0;
        if (PlayerPrefs.HasKey("最高分")) //获取最高分
        {
            topScore = PlayerPrefs.GetInt("最高分");
        }
        print(topScore);
        new ScoreManager("Sprites/Number").ShowScore(topScore, sprites);
    }
}
