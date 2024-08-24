using HarmonyLib;

namespace ThirdPersonView.Harmony
{
    [HarmonyPatch(typeof(PlayerMoveController), nameof(PlayerMoveController.Update))]
    public class SetThirdPersonView
    {
        private static bool? _isFirstPersonView = null;
        private static bool _cameraChangeHandled = false;

        public static void Postfix(PlayerMoveController __instance)
        {
            var playerInput = __instance.playerInput;
            var entityPlayerLocal = __instance.entityPlayerLocal;

            var isDrivingVehicle = entityPlayerLocal.AttachedToEntity != null;

            if (isDrivingVehicle)
            {
                if (!entityPlayerLocal.bFirstPersonView) return;

                entityPlayerLocal.SwitchFirstPersonView(false); // Switch to third-person view
                _isFirstPersonView = false;

                return;
            }

            if (_isFirstPersonView == null)
            {
                _isFirstPersonView = entityPlayerLocal.bFirstPersonView;
            }

            if (playerInput.CameraChange.IsPressed && !_cameraChangeHandled)
            {
                _isFirstPersonView = !_isFirstPersonView;
                _cameraChangeHandled = true;
            }

            if (!playerInput.CameraChange.IsPressed)
            {
                _cameraChangeHandled = false;
            }

            if (entityPlayerLocal.bFirstPersonView != _isFirstPersonView)
            {
                entityPlayerLocal.SwitchFirstPersonView(_isFirstPersonView.Value);
            }
        }
    }
}