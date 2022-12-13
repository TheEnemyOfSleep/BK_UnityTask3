using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ObjectLogic : MonoBehaviour
{
    [SerializeField] private float _ObjectOpacity;
    [SerializeField] private Text _textBox;

    public float ObjectOpacity
    {
        get => _ObjectOpacity;
        set => _ObjectOpacity = value;
    }

    public Text TextBox
    {
        get => _textBox;
        set => _textBox = value;
    }

    public void SetOpacityToText(float opacity)
    {
        ObjectOpacity = opacity;
        Color color = TextBox.color;
        color.a = float.Parse(opacity.ToString());
        TextBox.color = color;
    }
}