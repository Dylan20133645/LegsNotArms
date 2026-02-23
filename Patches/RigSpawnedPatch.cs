using System;
using HarmonyLib;

namespace LegsNotArms.Patches;

[HarmonyPatch(typeof(VRRigCache), nameof(VRRigCache.AddRigToGorillaParent))]
public static class RigSpawnedPatch
{
    public static Action<VRRig> OnRigSpawned;

    private static void Postfix(NetPlayer player, VRRig vrrig)
    {
        if (!vrrig.isLocal)
            OnRigSpawned?.Invoke(vrrig);
    }
}