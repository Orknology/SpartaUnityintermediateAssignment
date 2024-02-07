using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveableUI : MonoBehaviour, IDragHandler
{
    //ToDo : �����̰� �� ȭ�� ����� ���� ��ġ�� ���ư��� <- UIButton���� ����-������ ��� �и��� �ϼ�

    [SerializeField] private RectTransform targetUI;
    [SerializeField] private Canvas canvas;

    public void OnDrag(PointerEventData eventData)
    {
        // ���� �̵��� ���ؼ� �󸶳� �̵��ߴ����� ������
        // ĵ������ �����ϰ� ����� �ϱ� ������
        targetUI.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
