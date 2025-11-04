using UnityEngine;

public class FPS_CharBase : MonoBehaviour

{
    protected InputSystem_Actions m_inputActions;
    protected virtual void Awake()
    {
        m_inputActions = new InputSystem_Actions();
        m_inputActions.Enable();
    }
}
