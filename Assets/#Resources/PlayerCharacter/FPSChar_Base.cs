using UnityEngine;

public class FPSChar_Base : MonoBehaviour

{
    protected InputSystem_Actions m_inputActions;
    protected virtual void Awake()
    {
        m_inputActions = new InputSystem_Actions();
        m_inputActions.Enable();
    }
}
