using UnityEngine;

public class PlayerCharacter : MonoBehaviour

{
    protected InputSystem_Actions m_inputActions;
    protected virtual void Awake()
    {
        m_inputActions = new InputSystem_Actions();
        m_inputActions.Enable();
    }
}
