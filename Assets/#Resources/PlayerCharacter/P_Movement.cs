using UnityEngine;
using UnityEngine.InputSystem;

public class P_Movement : MonoBehaviour
{

    private InputSystem_Actions m_inputActions;

    private void Awake()
    {
        m_inputActions = new InputSystem_Actions();
        m_inputActions.Enable();
    }
}
