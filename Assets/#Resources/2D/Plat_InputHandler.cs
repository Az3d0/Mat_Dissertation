using UnityEngine;

public class Plat_InputHandler: MonoBehaviour
{
    [HideInInspector] public InputSystem_Actions m_inputActions;
    private void Awake()
    {
        m_inputActions = new InputSystem_Actions();
        m_inputActions.Enable();
    }

    public void EnablePlayer()
    {
        m_inputActions.Enable();
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
    public void DisablePlayer()
    {
        m_inputActions.Disable();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

}
