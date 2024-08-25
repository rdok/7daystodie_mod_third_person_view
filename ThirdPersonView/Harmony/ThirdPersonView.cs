using System;
using HarmonyLib;

namespace ThirdPersonView.Harmony
{
    [HarmonyPatch(typeof(PlayerMoveController), nameof(PlayerMoveController.Update))]
    public class SetThirdPersonView
    {
        private static bool? _isFirstPersonView = null;
        private static DateTime _controllerActivatedAt = DateTime.MinValue;
        private static readonly ILogger Logger = new Logger();
        private const float DoubleTapTimeWindow = .5f; // Double tap detection window (in seconds)

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

            var controllerActivate = playerInput.ControllerRebindableActions
                .Find(action => action.WasReleased && action.Name == "ToggleCrouch");

            if (controllerActivate != null)
            {
                var timePassedSinceLastActivatedAt = (DateTime.Now - _controllerActivatedAt).TotalSeconds;
                Logger.Info($"timePassedSinceLastActivatedAt: {timePassedSinceLastActivatedAt}");

                if (timePassedSinceLastActivatedAt <= DoubleTapTimeWindow)
                {
                    _isFirstPersonView = !_isFirstPersonView; 
                    entityPlayerLocal.SwitchFirstPersonView(_isFirstPersonView.Value);
                }

                _controllerActivatedAt = DateTime.Now;
            }

            if (entityPlayerLocal.bFirstPersonView != _isFirstPersonView)
            {
                entityPlayerLocal.SwitchFirstPersonView(_isFirstPersonView.Value);
            }
        }
    }
}