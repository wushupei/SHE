using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    SpriteRenderer[] son; //保存所有子物体
    Sprite[] numbers; //储存所有的数字图片
    void Start()
    {
        GetTexts();
        numbers = Resources.LoadAll<Sprite>("Sprites/Number1");        
    }
    void GetTexts() //获取所有子物体;
    {
        son = new SpriteRenderer[transform.childCount];
        for (int i = 0; i < son.Length; i++)
        {
            son[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

    }
    public void ShowScore(int score) //根据分数更换精灵图片
    {     
        switch (score.ToString().Length) //判断分数的位数
        {
            case 1:
                son[0].sprite = numbers[score]; //取个位
                break;
            case 2:
                son[0].sprite = numbers[score % 10];

                son[1].gameObject.SetActive(true); //取十位
                son[1].sprite = numbers[score / 10]; 
                break;
            case 3:
                son[0].sprite = numbers[score % 10];
                son[1].sprite = numbers[score / 10 % 10];

                son[2].gameObject.SetActive(true); //取百位
                son[2].sprite = numbers[score / 100]; 
                break;
            case 4:
                son[0].sprite = numbers[score % 10];
                son[1].sprite = numbers[score / 10 % 10];              
                son[2].sprite = numbers[score / 100%10];

                son[2].gameObject.SetActive(true); //取千位
                son[3].sprite = numbers[score / 1000];
                break;
        }
    }
}
