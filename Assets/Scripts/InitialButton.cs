using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[SerializeField]
public enum GameScene { Plane, Radar, Scene, }
public class InitialButton : MonoBehaviour
{
    public List<Button> ButtonList;
    int index = 0;  //按钮计数器

    public GameScene gamescene;

    // Start is called before the first frame update
    void Start()
    {
        switch (gamescene)
        {
            case GameScene.Plane:
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
                            Debug.Log("当前下标：" + index);
                        }
                        //动态生成按钮图片
                        ButtonList[index].GetComponentInChildren<TMP_Text>().text = key.Split("+")[0];
                        ButtonList[index].GetComponentInChildren<Image>().sprite = value;
                        index++;
                    }

                }
                index = 0;
                break;
            case GameScene.Radar:
                index = 0;
                foreach (KeyValuePair<string, Sprite> kvp in CreateItemController.buttonInfo)
                {
                    string key = kvp.Key;
                    Sprite value = kvp.Value;

                    //判断标记位类型
                    if (key.Split("+")[1].Equals("Radar"))
                    {
                        //激活一个按钮
                        if (!ButtonList[index].gameObject.activeInHierarchy)
                        {
                            ButtonList[index].gameObject.SetActive(true);
                            Debug.Log("当前下标：" + index);
                        }
                        //动态生成按钮图片
                        ButtonList[index].GetComponentInChildren<TMP_Text>().text = key.Split("+")[0];
                        ButtonList[index].GetComponentInChildren<Image>().sprite = value;
                        index++;
                    }
                }
                index = 0;
                break;
            case GameScene.Scene:
                index = 0;
                foreach (KeyValuePair<string, Sprite> kvp in CreateItemController.buttonInfo)
                {
                    string key = kvp.Key;
                    Sprite value = kvp.Value;

                    //判断标记位类型
                    if (key.Split("+")[1].Equals("Scene"))
                    {
                        //激活一个按钮
                        if (!ButtonList[index].gameObject.activeInHierarchy)
                        {
                            ButtonList[index].gameObject.SetActive(true);
                            Debug.Log("当前下标：" + index);
                        }
                        //动态生成按钮图片
                        ButtonList[index].GetComponentInChildren<TMP_Text>().text = key.Split("+")[0];
                        ButtonList[index].GetComponentInChildren<Image>().sprite = value;
                        index++;
                    }
                }
                index = 0;
                break;
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
