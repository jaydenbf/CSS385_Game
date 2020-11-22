using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerClickHandler
{
    public GameObject tower;
    public GameObject towerUI;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var temp = Instantiate(towerUI, GetMouseWorldPosition(), Quaternion.identity);
        temp.GetComponent<TowerUI>().tower = tower;

        Color c = new Color32(255, 255, 255, 128);
        GetComponent<Image>().color = c;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Color c = new Color32(255, 255, 255, 255);
        GetComponent<Image>().color = c;
        //Color c = GetComponent<Image>().color = Color.red;
        //Color c = GetComponent<SpriteRenderer>().color;
        //c.a = 1f;
        //GetComponent<SpriteRenderer>().color = c;
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
}
