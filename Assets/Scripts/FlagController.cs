using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class FlagController : MonoBehaviour
{

    public TMP_Text textMeshPro;

    void Start()
    {
        textMeshPro = GetComponentInChildren<TMP_Text>();
    }

    //设置旗帜文本的内容，并将其对齐到旗帜顶部
    public void SetText(string text)
    {
        textMeshPro.SetText(text);
        Debug.Log("坐标：" + text);
        textMeshPro.transform.position = transform.position + Vector3.up * 0.5f;
    }

}
