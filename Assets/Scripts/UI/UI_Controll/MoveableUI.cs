using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveableUI : MonoBehaviour, IDragHandler
{
    //ToDo : 움직이고 난 화면 종료시 원래 위치로 돌아가기 <- UIButton에서 진입-나가기 기능 분리로 완성

    [SerializeField] private RectTransform targetUI;
    [SerializeField] private Canvas canvas;

    public void OnDrag(PointerEventData eventData)
    {
        // 이전 이동과 비교해서 얼마나 이동했는지를 보여줌
        // 캔버스의 스케일과 맞춰야 하기 때문에
        targetUI.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
