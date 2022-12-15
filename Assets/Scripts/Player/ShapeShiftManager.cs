using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShiftManager : MonoBehaviour
{
    private PlayerInput _input;
    private CharacterController cc;
    [SerializeField] private GameObject[] shapes;

    public shape currentShape;
    public GameObject door;
    private Vector3 ccvec;

    [Header("CC")] 
    private float bigRad = 0.8f;
    private float bigH = 1.2f;
    private float bigY = 0.32f;
    private float smallRad = 0.1f;
    private float smallH = 0.31f;
    private float smallY = -0.25f;
    private float normRad;
    private float normH;
    private float normY = 0;
    private float currentRad;
    private float currentH;
    private float currentY;
    
    public enum shape
    {
        blob,
        smallBox,
        bigbox
    }
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        cc = GetComponent<CharacterController>();
        currentShape = shape.blob;

        normH = cc.height;
        normRad = cc.radius;
        currentRad = cc.radius;
        currentH = cc.height;
    }

    void Update()
    {
        Normal();
        cc.height = currentH;
        cc.radius = currentRad;
        cc.center = new Vector3(0, currentY, 0);
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
        currentH = normH;
        currentRad = normRad;
        currentY = normY;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BigBox") && _input.change)
        {
            DisableAll();
            shapes[2].SetActive(true);
            currentShape = shape.bigbox;
            currentH = bigH;
            currentRad = bigRad;
            currentY = bigY;
        }
        else if (other.CompareTag("SmallBox") && _input.change)
        {
            DisableAll();
            shapes[1].SetActive(true);
            currentShape = shape.smallBox;
            currentH = smallH;
            currentRad = smallRad;
            currentY = smallY;
        }
        else if (other.CompareTag("Pad") && currentShape == shape.bigbox)
        {
            door.SetActive(false);
        }
    }
}
