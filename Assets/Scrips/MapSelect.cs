using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MapSelect : MonoBehaviour {

    public int starsNum = 0;
    public bool isSelect = false;

    public GameObject locks;
    public GameObject stars;

    public GameObject panel;
    public GameObject map;

    public int startNum=1;
    public int endNum=3;
    public Text startText;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();//清除数据
        if (PlayerPrefs.GetInt("totalNum", 0) >= starsNum)//如果总的星星数量>解锁每一个地图所需要的的数量，就可以解锁该map
        {
            isSelect = true;
        }
        if (isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);

            //text星星的显示
            int counts = 0;
            for (int i = starsNum; i <= endNum; i++)
            {
                counts += PlayerPrefs.GetInt("level" + i.ToString());
            }
            startText.text = counts.ToString()+"/20";
            
        }
    }

    public void Selected()
    {
        if (isSelect)
        {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }


    public void Return()
    {
        panel.SetActive(true);
        map.SetActive(false);
        SceneManager.LoadScene(1);

    }
    

}
