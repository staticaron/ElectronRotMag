using UnityEngine;

[CreateAssetMenu(fileName = "PlayStateChannelSO", menuName = "ElectronRotMag/PlayStateChannelSO", order = 0)]
public class PlayStateChannelSO : ScriptableObject
{
    public delegate void SetState(PlayState stateToSet);
    public event SetState ESetState;

    public void RaiseSetStateEvent(PlayState stateToSet)
    {
        if (ESetState != null) ESetState(stateToSet);
        else
        {
            Debug.LogWarning("SetStateEvent was raised but no one was listening to it");
        }
    }
}