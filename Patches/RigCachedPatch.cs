using System;
using HarmonyLib;

namespace LegsNotArms.Patches;

[HarmonyPatch(typeof(VRRigCache), nameof(VRRigCache.RemoveRigFromGorillaParent))]
public static class RigCachedPatch
{
    public static Action<VRRig> OnRigCached;

    private static void Prefix(NetPlayer player, VRRig vrrig)
    {
        if (!vrrig.isLocal)
            OnRigCached?.Invoke(vrrig);
    }
}