using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public string planeNum; // 飞机信息
    public string redarNUm; //雷达信息
    public string sceneNum; //场景选择编号
    public LineRendererData lineInfo; // 射线信息，使用 Vector3Wrapper 列表保存射线顶点的位置
}

public class SavingData : MonoBehaviour
{
    //静态数据变量
    public static Data data = new Data();
}
