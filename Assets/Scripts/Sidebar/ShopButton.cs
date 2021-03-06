﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Tooltip tooltip;

    public Tower1 tower;
    public TowerUI towerUI;
    bool canAfford = false;

    void Start()
    {

    }

    void Update()
    {
        checkAffordability();
        updateColor();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(canAfford)
        {
            var temp = Instantiate(towerUI, GetMouseWorldPosition(), Quaternion.identity);
            temp.GetComponent<TowerUI>().tower = tower;

            Color c = new Color32(255, 255, 255, 128);
            GetComponent<Image>().color = c;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Color c = new Color32(255, 255, 255, 255);
        GetComponent<Image>().color = c;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 v = Input.mousePosition;
        v = Camera.main.ScreenToWorldPoint(v);
        v.z = 0.0f;

        return v;
    }

    private void onMouseOver()
    {

    }

    private void onMouseUp()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(canAfford)
        {
            GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
        tooltip.SetCurTooltip(tower.getTowerInfo());
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.75f, 0.75f);
        tooltip.HideTooltip();
    }

    void checkAffordability()
    {
        int cost = tower.cost;

        if (GameManager.cash - cost >= 0)
        {
            canAfford = true;
        } else
        {
            canAfford = false;
        }
    }

    void updateColor()
    {
        if(canAfford)
        {
            Color c = new Color32(255, 255, 255, 255);
            GetComponent<Image>().color = c;
        } else
        {
            Color c = new Color32(191, 64, 64, 128);
            GetComponent<Image>().color = c;
        }
    }
}
