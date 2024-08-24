using HarmonyLib;

namespace ThirdPersonView.Harmony
{
    [HarmonyPatch(typeof(PlayerMoveController), nameof(PlayerMoveController.Update))]
    public class SetThirdPersonView
    {
        private static bool? isFirstPersonView = null;
        private static bool cameraChangeHandled = false;

        public static void Postfix(PlayerMoveController __instance)
        {
            var playerInput = __instance.playerInput;

            if (isFirstPersonView == null)
            {
                isFirstPersonView = __instance.entityPlayerLocal.bFirstPersonView;
            }

            if (playerInput.CameraChange.IsPressed && !cameraChangeHandled)
            {
                isFirstPersonView = !isFirstPersonView;
                cameraChangeHandled = true;
            }

            if (!playerInput.CameraChange.IsPressed)
            {
                cameraChangeHandled = false;
            }

            if (__instance.entityPlayerLocal.bFirstPersonView != isFirstPersonView)
            {
                __instance.entityPlayerLocal.SwitchFirstPersonView(isFirstPersonView.Value);
            }
        }
    }
}