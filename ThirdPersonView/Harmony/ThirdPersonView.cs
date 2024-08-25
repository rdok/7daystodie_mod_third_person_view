using System;
using HarmonyLib;
using UnityEngine;

namespace ThirdPersonView.Harmony
{
    [HarmonyPatch(typeof(PlayerMoveController), nameof(PlayerMoveController.Update))]
    public class SetThirdPersonView
    {
        private static readonly ILogger Logger = new Logger();

        private static bool? _isFirstPersonView;
        private static bool _cameraChangeHandled;

        private static float _controllerActivatedAt = -1f;
        private const float DoubleTapTimeWindow = .5f;

        public static void Postfix(PlayerMoveController __instance)
        {
            Logger.Info("Postfix called in SetThirdPersonView.");

            var playerInput = __instance.playerInput;
            Logger.Info("Player input captured.");

            var entityPlayerLocal = __instance.entityPlayerLocal;
            Logger.Info("EntityPlayerLocal captured.");

            var isDrivingVehicle = entityPlayerLocal.AttachedToEntity != null;
            Logger.Info($"isDrivingVehicle evaluated to: {isDrivingVehicle}");

            if (isDrivingVehicle)
            {
                Logger.Info("Player is driving a vehicle.");
                if (!entityPlayerLocal.bFirstPersonView)
                {
                    Logger.Info("Player is already in third-person view. Exiting.");
                    return;
                }

                entityPlayerLocal.SwitchFirstPersonView(false);
                Logger.Info("Switched to third-person view while driving a vehicle.");
                _isFirstPersonView = false;
                Logger.Info("_isFirstPersonView set to false.");
                return;
            }

            if (_isFirstPersonView == null)
            {
                _isFirstPersonView = entityPlayerLocal.bFirstPersonView;
                Logger.Info($"_isFirstPersonView initialized to: {_isFirstPersonView}");
            }

            var controllerUsed = playerInput.ControllerRebindableActions
                .Find(action => action.WasReleased && action.Name == "ToggleCrouch");

            Logger.Info($"ControllerUsed evaluated to: {controllerUsed != null}");

            if (controllerUsed != null)
            {
                var currentTime = Time.time;
                Logger.Info($"Current time: {currentTime}");

                if (_controllerActivatedAt >= 0f && (currentTime - _controllerActivatedAt) <= DoubleTapTimeWindow)
                {
                    _isFirstPersonView = !_isFirstPersonView;
                    Logger.Info($"_isFirstPersonView toggled to: {_isFirstPersonView}");
                    entityPlayerLocal.SwitchFirstPersonView(!_isFirstPersonView.Value);
                    Logger.Info("First-person view toggled via controller double-tap.");
                }

                // Update the time when the controller action was activated
                _controllerActivatedAt = currentTime;
                Logger.Info($"_controllerActivatedAt updated to: {_controllerActivatedAt}");
                return;
            }

            if (playerInput.CameraChange.IsPressed && !_cameraChangeHandled)
            {
                _isFirstPersonView = !_isFirstPersonView;
                Logger.Info($"_isFirstPersonView toggled to: {_isFirstPersonView}");
                _cameraChangeHandled = true;
                Logger.Info("_cameraChangeHandled set to true.");
            }

            if (!playerInput.CameraChange.IsPressed)
            {
                _cameraChangeHandled = false;
                Logger.Info("_cameraChangeHandled reset to false.");
            }

            if (entityPlayerLocal.bFirstPersonView == _isFirstPersonView)
            {
                Logger.Info("No view change necessary. Exiting.");
                return;
            }

            entityPlayerLocal.SwitchFirstPersonView(_isFirstPersonView.Value);
            Logger.Info("Switched player view based on current _isFirstPersonView value.");
        }
    }
}