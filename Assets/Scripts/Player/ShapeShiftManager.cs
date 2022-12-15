using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShiftManager : MonoBehaviour
{
    private PlayerInput _input;
    [SerializeField] private GameObject[] shapes;
    
    void Start()
    {
        _input = GetComponent<PlayerInput>();
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BigBox") && _input.change)
        {
            DisableAll();
            shapes[2].SetActive(true);
        }
        else if (other.CompareTag("SmallBox") && _input.change)
        {
            DisableAll();
            shapes[1].SetActive(true);
        }
    }
}
