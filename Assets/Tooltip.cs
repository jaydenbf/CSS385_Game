using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text text;
    public Tower1 tower1;
    public Tower1 tower2;
    public Tower1 tower3;
    public Tower1 tower4;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTooltip1()
    {
        text.text = tower1.getTowerInfo();
    }
    public void ShowTooltip2()
    {
        text.text = tower2.getTowerInfo();
    }
    public void ShowTooltip3()
    {
        text.text = tower3.getTowerInfo();
    }
    public void ShowTooltip4()
    {
        text.text = tower4.getTowerInfo();
    }
    public void ResetTooltip()
    {
        text.text = "";
    }
}
