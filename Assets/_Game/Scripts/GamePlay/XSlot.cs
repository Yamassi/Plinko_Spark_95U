using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tretimi;
using TMPro;
using System;
public class XSlot : MonoBehaviour
{
    [SerializeField] private float _coefficient;
    [SerializeField] private TextMeshPro _text;
    public Action<float> OnBallFallToXSlot;
    private void OnValidate()
    {
        _text = GetComponentInChildren<TextMeshPro>();
    }
    public void SetCoefficient(float coeficient)
    {
        _coefficient = coeficient;
        _text.text = $"{_coefficient}x";
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger enter {other.gameObject.name} Coeficient {_coefficient}");

        OnBallFallToXSlot?.Invoke(_coefficient);
        AudioSystem.Instance.BallFallToSlot();
        Destroy(other.gameObject);
    }
}
