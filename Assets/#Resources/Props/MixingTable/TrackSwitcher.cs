using UnityEngine;

public class TrackSwitcher : MonoBehaviour
{
    [SerializeField] AK.Wwise.Switch m_switch = new AK.Wwise.Switch();
    public AK.Wwise.Switch Switch => m_switch;
}
