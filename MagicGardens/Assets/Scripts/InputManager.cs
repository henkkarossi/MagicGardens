using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	
public class InputManager : MonoBehaviour {

    public static InputManager instance;

    public enum ClickStates { idle, press, hold, release, released}
    public ClickStates clickState;
    [HideInInspector]
    public Vector2 cursorPosition;

    public Canvas canvasForMouse;
    
    private void Awake()
    {

        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;   
        }

    }

    void Update () {

        if (clickState == ClickStates.release)
        {
            clickState = ClickStates.released;
        }

        if (!Application.isEditor)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);


                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            clickState = ClickStates.press;
                            break;

                        case TouchPhase.Ended:
                            clickState = ClickStates.release;
                            break;

                        default:
                            clickState = ClickStates.hold;
                            break;
                    }


                cursorPosition = touch.position;
            }

        }
        else
        {
                      
                if (Input.GetMouseButtonDown(0))
                {
                    clickState = ClickStates.press;


                }
                else
                {
                    if (Input.GetMouseButton(0))
                    {
                        clickState = ClickStates.hold;
                    }
                    else
                    {
                        if (Input.GetMouseButtonUp(0))
                        {
                            clickState = ClickStates.release;

                        }
                    }
                }


            cursorPosition = Input.mousePosition;
        }

    }




}


