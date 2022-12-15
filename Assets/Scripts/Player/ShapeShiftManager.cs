using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShiftManager : MonoBehaviour
{
    private PlayerInput _input;
    [SerializeField] private GameObject[] shapes;

    public shape currentShape;

    public GameObject door;
    
    public enum shape
    {
        blob,
        smallBox,
        bigbox
    }
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        currentShape = shape.blob;
    }

    void Update()
    {
        Normal();
    }
    
    private void Normal()
    {
        if (!_input.revert) return;
        DisableAll();
        shapes[0].SetActive(true);
    }

    private void DisableAll()
    {
        foreach (var go in shapes)
        {
            go.SetActive(false);
        }

        currentShape = shape.blob;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BigBox") && _input.change)
        {
            DisableAll();
            shapes[2].SetActive(true);
            currentShape = shape.bigbox;
        }
        else if (other.CompareTag("SmallBox") && _input.change)
        {
            DisableAll();
            shapes[1].SetActive(true);
            currentShape = shape.smallBox;
        }
        else if (other.CompareTag("Pad") && currentShape == shape.bigbox)
        {
            door.SetActive(false);
        }
    }
}
