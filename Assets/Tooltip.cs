using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Text tooltipText;
    private RectTransform backgroundRectTransform;

    private float lastUpdate = 0;

    private void Awake()
    {
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<Text>();

        Debug.Assert(tooltipText != null);
        HideTooltip();
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint + new Vector2(2f, 2f);

        Vector3 pos = gameObject.transform.position;
        pos.x -= tooltipText.preferredWidth;
        gameObject.transform.position = pos;

    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public void SetCurTooltip(string s)
    {
        if (s == "")
            HideTooltip();

        gameObject.SetActive(true);

        tooltipText.text = s;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f,
            tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint + new Vector2(2f, 2f);

        Vector3 pos = gameObject.transform.position;
        pos.x -= tooltipText.preferredWidth;
        gameObject.transform.position = pos;
    }
}
