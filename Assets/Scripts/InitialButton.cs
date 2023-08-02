using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InitialButton : MonoBehaviour
{
    public List<Button> ButtonList;
    int index = 0;  //按钮计数器

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        foreach (KeyValuePair<string, Sprite> kvp in CreateItemController.buttonInfo)
        {
            string key = kvp.Key;
            Sprite value = kvp.Value;

            //判断标记位类型
            if (key.Split("+")[1].Equals("Plane"))
            {
                //激活一个按钮
                if (!ButtonList[index].gameObject.activeInHierarchy)
                {
                    ButtonList[index].gameObject.SetActive(true);
                }
                //动态生成按钮图片
                ButtonList[index].GetComponentInChildren<TMP_Text>().text = key.Split("+")[0];
                ButtonList[index].GetComponentInChildren<Image>().sprite = value;
            }
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        for (int i = 0; i < index; i++)
        {
            if (ButtonList[index].gameObject.activeInHierarchy)
            {
                ButtonList[index].gameObject.SetActive(false);
            }
        }
    }
}
