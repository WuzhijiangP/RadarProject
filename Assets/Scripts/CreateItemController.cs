using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CreateItemController : MonoBehaviour, IPointerClickHandler
{

    public static Dictionary<string, Sprite> buttonInfo = new Dictionary<string, Sprite>();
    //public static List<Button> buttonsInfo = new List<Button>();


    public GameObject tips;
    public GameObject tipsImage;
    Sprite itemImage;
    public void OnPointerClick(PointerEventData eventData)
    {
        //获取鼠标点击事件不为null
        if (eventData.pointerPress != null)
        {
            //获取到点击按钮
            Button clickedButton = eventData.pointerPress.GetComponent<Button>();
            if (clickedButton != null)
            {
                //获取按钮图标
                itemImage = clickedButton.GetComponentInChildren<Image>().sprite;
                TipsShowOut();

                //如果是飞机
                if (clickedButton.tag.Equals("Plane"))
                {
                    if (!buttonInfo.ContainsKey(clickedButton.GetComponentInChildren<TMP_Text>().text + "+Plane"))
                        //按钮信息压入字典
                        buttonInfo.Add(clickedButton.GetComponentInChildren<TMP_Text>().text + "+Plane", clickedButton.GetComponentInChildren<Image>().sprite);
                }
                //如果是雷达
                if (clickedButton.tag.Equals("Radar"))
                {
                    if (!buttonInfo.ContainsKey(clickedButton.GetComponentInChildren<TMP_Text>().text + "+Radar"))
                        //按钮信息压入字典
                        buttonInfo.Add(clickedButton.GetComponentInChildren<TMP_Text>().text + "+Radar", clickedButton.GetComponentInChildren<Image>().sprite);
                }
                //如果是场景
                if (clickedButton.tag.Equals("Scene"))
                {
                    if (!buttonInfo.ContainsKey(clickedButton.GetComponentInChildren<TMP_Text>().text + "+Scene"))
                        //按钮信息压入字典
                        buttonInfo.Add(clickedButton.GetComponentInChildren<TMP_Text>().text + "+Scene", clickedButton.GetComponentInChildren<Image>().sprite);
                }
                //如果是干扰器
                if (clickedButton.tag.Equals("Jammer"))
                {
                    if (!buttonInfo.ContainsKey(clickedButton.GetComponentInChildren<TMP_Text>().text + "+Jammer"))
                        //按钮信息压入字典
                        buttonInfo.Add(clickedButton.GetComponentInChildren<TMP_Text>().text + "+Jammer", clickedButton.GetComponentInChildren<Image>().sprite);
                }


                //打印查看信息
                foreach (KeyValuePair<string, Sprite> kvp in buttonInfo)
                {
                    string key = kvp.Key;
                    Sprite value = kvp.Value;

                    // 使用 key 和 value 进行需要的操作
                    Debug.Log("字典中有" + buttonInfo.Count + "个数据：" + key + "   " + value);
                }

            }

        }


        Debug.Log("表中现在有" + buttonInfo.Count + "项数据");
    }

    public void TipsShowOut()
    {
        //获取点击按钮的飞机图片
        tipsImage.GetComponent<Image>().sprite = itemImage;

        tips.SetActive(true);
    }


}
