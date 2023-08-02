using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickToDraw : MonoBehaviour
{
    public GameObject dotPrefab;
    public TMP_Text coordText;
    public RectTransform canvasRT;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 positionInCoordinateSystem = transform.InverseTransformPoint(worldPosition);
            positionInCoordinateSystem.z = 0; // 可能需要调整深度
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPosition);

            GameObject dotObj = Instantiate(dotPrefab, positionInCoordinateSystem, Quaternion.identity);
            dotObj.transform.SetParent(transform, false);

            Vector2 coordTextPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRT, screenPoint, canvasRT.GetComponent<Canvas>().worldCamera, out coordTextPos);
            coordText.text = string.Format("({0:F1}, {1:F1})", positionInCoordinateSystem.x + 680f, positionInCoordinateSystem.y + 353f);
            coordText.transform.localPosition = coordTextPos;
        }
    }

}
