using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public bool isSelect = false;
    public Sprite levelBg;

    public GameObject[] stars;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    
    // Use this for initialization
	public void Start () {
        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }

        else
        {
            int beforeNum = int.Parse(gameObject.name)-1 ;
            if (PlayerPrefs.GetInt("level" + beforeNum.ToString()) > 0)
            {
                isSelect = true;
            }
        }

        if (isSelect)
        {
            image.overrideSprite = levelBg;//如果可以选择，level的解锁图片替换为可选择的背景图片
            transform.Find("num").gameObject.SetActive(true);

            int count = PlayerPrefs.GetInt("level" + gameObject.name);//获取每一关的星星数量
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                    stars[i].SetActive(true);
            }
        }
	}

    public void Selected()
    {
        if(isSelect)
        {
         
            PlayerPrefs.SetString("nowLevel","level"+gameObject.name);//存储这关的level等级的名字（nowlevel）
            SceneManager.LoadScene(2);
        }
    }

    
}
