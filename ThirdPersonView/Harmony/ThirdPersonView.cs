using System;
using HarmonyLib;

namespace ThirdPersonView.Harmony
{
    [HarmonyPatch(typeof(PlayerMoveController), nameof(PlayerMoveController.Update))]
    public class SetThirdPersonView
    {
        private static bool? _isFirstPersonView;
        private static bool _cameraChangeHandled;

        private static DateTime _controllerActivatedAt = DateTime.MinValue;
        private const float DoubleTapTimeWindow = .5f; 

        public static void Postfix(PlayerMoveController __instance)
        {
            var playerInput = __instance.playerInput;
            var entityPlayerLocal = __instance.entityPlayerLocal;
            var isDrivingVehicle = entityPlayerLocal.AttachedToEntity != null;

            if (isDrivingVehicle)
            {
                if (!entityPlayerLocal.bFirstPersonView) return;

                entityPlayerLocal.SwitchFirstPersonView(false);
                _isFirstPersonView = false;
                return;
            }

            if (_isFirstPersonView == null)
            {
                _isFirstPersonView = entityPlayerLocal.bFirstPersonView;
            }

            var controllerUsed = playerInput.ControllerRebindableActions
                .Find(action => action.WasReleased && action.Name == "ToggleCrouch");

            if (controllerUsed != null)
            {
                var timePassedSinceLastActivatedAt = (DateTime.Now - _controllerActivatedAt).TotalSeconds;

                if (timePassedSinceLastActivatedAt <= DoubleTapTimeWindow)
                {
                    _isFirstPersonView = !_isFirstPersonView;
                    entityPlayerLocal.SwitchFirstPersonView(!_isFirstPersonView.Value);
                }

                _controllerActivatedAt = DateTime.Now;

                return;
            }

            if (playerInput.CameraChange.IsPressed && !_cameraChangeHandled)
            {
                _isFirstPersonView = !_isFirstPersonView;
                _cameraChangeHandled = true;
            }

            if (!playerInput.CameraChange.IsPressed) _cameraChangeHandled = false;

            if (entityPlayerLocal.bFirstPersonView == _isFirstPersonView) return;

            entityPlayerLocal.SwitchFirstPersonView(_isFirstPersonView.Value);
        }
    }
}