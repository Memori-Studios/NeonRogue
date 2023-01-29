using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private Color highlightedColor;
    Color normalColor;
    public void OnPointerEnter()
    {
        normalColor = buttonImage.color;
        buttonImage.color = highlightedColor;
    }
    public void OnPointerExit()
    {
        buttonImage.color = normalColor;
    }
}
