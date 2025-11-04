using UnityEngine;

public class Plat_CharBase: MonoBehaviour
{
    protected InputSystem_Actions m_inputActions;
    protected virtual void Awake()
    {
        m_inputActions = new InputSystem_Actions();
        m_inputActions.Enable();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
