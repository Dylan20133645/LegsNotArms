using BepInEx;
using UnityEngine;

namespace LegsNotArms;

[BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
public class Plugin : BaseUnityPlugin
{
    private readonly float      defaultYPosition = 0.3432f;
    private          GameObject leftShoulder;
    private readonly float      legsYPosition = 0.0432f;
    private          GameObject rightShoulder;
    private          void       Start() => GorillaTagger.OnPlayerSpawned(OnPlayerSpawned);

    private void OnPlayerSpawned()
    {
        string shoulderPathPrefix =
                "Player Objects/Local VRRig/Local Gorilla Player/GorillaPlayerNetworkedRigAnchor/rig/body/shoulder.";

        leftShoulder  = GameObject.Find(shoulderPathPrefix + "L");
        rightShoulder = GameObject.Find(shoulderPathPrefix + "R");

        NetworkSystem.Instance.OnJoinedRoomEvent        += TurnArmsToLegsIfInModded;
        NetworkSystem.Instance.OnReturnedToSinglePlayer += TurnArmsToLegs;
        
        Vector3 lPos = leftShoulder.transform.localPosition;
        Vector3 rPos = rightShoulder.transform.localPosition;

        lPos.y = legsYPosition;
        rPos.y = legsYPosition;

        leftShoulder.transform.localPosition  = lPos;
        rightShoulder.transform.localPosition = rPos;
    }

    private void TurnArmsToLegs()
    {
        Vector3 lPos = leftShoulder.transform.localPosition;
        Vector3 rPos = rightShoulder.transform.localPosition;

        lPos.y = legsYPosition;
        rPos.y = legsYPosition;

        leftShoulder.transform.localPosition  = lPos;
        rightShoulder.transform.localPosition = rPos;
    }

    private void TurnArmsToLegsIfInModded()
    {
        if (!NetworkSystem.Instance.GameModeString.Contains("MODDED"))
        {
            Vector3 lPos = leftShoulder.transform.localPosition;
            Vector3 rPos = rightShoulder.transform.localPosition;

            lPos.y = defaultYPosition;
            rPos.y = defaultYPosition;

            leftShoulder.transform.localPosition  = lPos;
            rightShoulder.transform.localPosition = rPos;

            return;
        }

        {
            Vector3 lPos = leftShoulder.transform.localPosition;
            Vector3 rPos = rightShoulder.transform.localPosition;

            lPos.y = legsYPosition;
            rPos.y = legsYPosition;

            leftShoulder.transform.localPosition  = lPos;
            rightShoulder.transform.localPosition = rPos;
        }
    }
}