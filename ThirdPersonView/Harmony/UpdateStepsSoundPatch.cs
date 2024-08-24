using HarmonyLib;
using UnityEngine;

namespace ThirdPersonView.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal), nameof(EntityPlayerLocal.updateStepSound))]
    public class UpdateStepsSoundPatch
    {
        /**
         * This function replicates the EntityAlive.updateStepSound code.
         * For future version of the game, simple go to this definition, and
         * update the code with any changes as required.
         *
         * Alternatively, if you can find the proper way to call this base call
         * it would the optimal solution. My attempt, however, was just causing
         * crashes without any exception; making it impossible to debug with my
         * limited experience in C# and limited time.
         */
        public static bool Prefix(EntityPlayerLocal __instance, float _distX, float _distZ)
        {
            if (__instance.bFirstPersonView)
            {
                return true;
            }

            if (__instance.blockValueStandingOn.isair)
            {
                return false;
            }

            if (!__instance.onGround || __instance.isHeadUnderwater)
            {
                __instance.distanceSwam += Mathf.Sqrt(_distX * _distX + _distZ * _distZ);

                if (__instance.distanceSwam <= __instance.nextSwimDistance)
                {
                    return false;
                }

                __instance.nextSwimDistance++;

                if (__instance.nextSwimDistance < __instance.distanceSwam ||
                    __instance.nextSwimDistance > __instance.distanceSwam + 1.0f)
                    __instance.nextSwimDistance = __instance.distanceSwam + 1f;

                __instance.internalPlayStepSound();
            }
            else
            {
                var num = Mathf.Sqrt(_distX * _distX + _distZ * _distZ);
                __instance.distanceWalked += num;
                __instance.stepDistanceToPlaySound -= num;

                if (__instance.stepDistanceToPlaySound > 0.0f)
                    return false;

                __instance.stepDistanceToPlaySound = __instance.getNextStepSoundDistance();
                __instance.internalPlayStepSound();
            }

            return false;
        }
    }
}