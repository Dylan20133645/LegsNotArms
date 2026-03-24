using BepInEx;
using ExitGames.Client.Photon;
using HarmonyLib;
using LegsNotArms.Patches;
using Photon.Pun;
using UnityEngine;

namespace LegsNotArms;

[BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
public class Plugin : MonoBehaviour
{
    private const float DefaultYPosition = 0.3432f;
    private const float LegsYPosition    = 0.0432f;

    private void Start()
    {
        GorillaTagger.OnPlayerSpawned(OnPlayerSpawned);

        PhotonNetwork.SetPlayerCustomProperties(new Hashtable { { Constants.Name, Constants.Version }, });

        Harmony harmony = new(Constants.GUID);
        harmony.PatchAll();

        RigSpawnedPatch.OnRigSpawned += rig =>
                                        {
                                            if (!rig.creator.GetPlayerRef().CustomProperties
                                                    .ContainsKey(Constants.Name))
                                                return;

                                            Transform left  = GetShoulderTransform(rig, true);
                                            Transform right = GetShoulderTransform(rig, false);

                                            Vector3 lPos = left.localPosition;
                                            Vector3 rPos = right.localPosition;

                                            lPos.y = LegsYPosition;
                                            rPos.y = LegsYPosition;

                                            left.localPosition  = lPos;
                                            right.localPosition = rPos;
                                        };

        RigCachedPatch.OnRigCached += rig =>
                                      {
                                          Transform left  = GetShoulderTransform(rig, true);
                                          Transform right = GetShoulderTransform(rig, false);

                                          Vector3 lPos = left.localPosition;
                                          Vector3 rPos = right.localPosition;

                                          lPos.y = DefaultYPosition;
                                          rPos.y = DefaultYPosition;

                                          left.localPosition  = lPos;
                                          right.localPosition = rPos;
                                      };
    }

    private Transform GetShoulderTransform(VRRig rig, bool isLeft) =>
            isLeft ? rig.leftHand.rigTarget.transform.parent.parent : rig.rightHand.rigTarget.transform.parent.parent;

    private void OnPlayerSpawned()
    {
        Debug.Log("[VRARARARARA] " + VRRig.LocalRig.leftHand.rigTarget.name);

        Transform left  = GetShoulderTransform(VRRig.LocalRig, true);
        Transform right = GetShoulderTransform(VRRig.LocalRig, false);

        Vector3 lPos = left.localPosition;
        Vector3 rPos = right.localPosition;

        lPos.y = LegsYPosition;
        rPos.y = LegsYPosition;

        left.localPosition  = lPos;
        right.localPosition = rPos;
    }
}
