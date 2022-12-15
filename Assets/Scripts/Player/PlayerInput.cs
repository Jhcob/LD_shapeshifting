using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    
    public float horizontalInput;
    public float verticalInput;
    public bool jump;
    public bool change;
    public bool revert;

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jump = Input.GetButtonDown("Jump");
        change = Input.GetKey(KeyCode.E);
        revert = Input.GetKeyDown(KeyCode.Q);
    }
}
