using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DroppedArrow : MonoBehaviour
{
    [SerializeField] private float _baseY;
    [SerializeField] private float _flowRange;

    private void Start()
    {
        _baseY = transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, _baseY + Mathf.Sin(Time.time) * _flowRange, transform.position.z);
    }
}
