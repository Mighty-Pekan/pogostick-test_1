using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] bool touchInputEnabled;
    [SerializeField] float butHittStartDelay = 0.1f;    //prevents accidental double hold

    float halfScreenWidth = Screen.width / 2;
    Vector3 rotationDirection;

    private Touch touch;

    private bool isDoubleHold = false;

    private void Start() {
        GameController.RegisterInputManager(this);
    }

    private void Update()
    {
        if (touchInputEnabled)
            MobileInput();
        else
            KeyboardInput();
    }

    private void MobileInput()
    {
        //Checks if is not UI click
        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        //if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                for (int i = 0; i < Input.touchCount; i++)
                {
                    Vector3 touchPosition = Input.touches[i].position;

                    if (touchPosition.x < halfScreenWidth)
                        rotationDirection = Vector3.forward;
                    else
                        rotationDirection = Vector3.back;
                    if (touch.phase == TouchPhase.Ended)
                        rotationDirection = Vector3.zero;
                }
            }
        }
    }

    float? doubleHoldStartTime = null;
    private void KeyboardInput() {

        //---------------------double hold handling
        if( 
            (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) &&
            (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            ) {
            rotationDirection = Vector3.zero;

            if (doubleHoldStartTime == null) {
                doubleHoldStartTime = Time.time;
            }
            else if(Time.time - doubleHoldStartTime >= butHittStartDelay) {
                isDoubleHold = true;
            }
        }
        //---------------------rotation handling
        else {
            isDoubleHold = false;
            doubleHoldStartTime = null;          
        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            rotationDirection = Vector3.forward;

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            rotationDirection = Vector3.back;

        else rotationDirection = Vector3.zero;
        }
    }

    public static Vector3 GetRotationDirection()
    {
        return instance.rotationDirection;
    }

    
    public static bool IsDoubleHold() {
        return instance.isDoubleHold;
    }

    public void ToggleTouchInput()
    {
        touchInputEnabled = !touchInputEnabled;
        Debug.Log("touch input switched: " + touchInputEnabled);
    }
}
