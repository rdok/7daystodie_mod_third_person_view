using HarmonyLib;
using UnityEngine;

namespace ThirdPersonView.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal), nameof(EntityPlayerLocal.PlayStepSound))]
    public class PlayStepsSoundPatch
    {
        public static bool Prefix(EntityPlayerLocal __instance)
        {
            if (__instance.bFirstPersonView)
            {
                return true;
            }

            __instance.internalPlayStepSound();

            return false;
        }
    }
}