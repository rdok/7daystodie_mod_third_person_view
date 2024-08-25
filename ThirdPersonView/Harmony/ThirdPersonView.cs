using System;
using HarmonyLib;
using InControl;
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
            var playerInput = __instance.playerInput;
            var entityPlayerLocal = __instance.entityPlayerLocal;
            var isDrivingVehicle = entityPlayerLocal.AttachedToEntity != null;

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
                return;
            }

            if (_isFirstPersonView == null)
            {
                _isFirstPersonView = entityPlayerLocal.bFirstPersonView;
                Logger.Info($"_isFirstPersonView initialized to: {_isFirstPersonView}");
            }

            var controllerUsed = playerInput.ControllerRebindableActions.Find(action =>
                action.LastDeviceClass == InputDeviceClass.Controller &&
                action.WasReleased &&
                action.Name == "ToggleCrouch"
            );

            if (controllerUsed != null)
            {
                var currentTime = Time.time;

                if (_controllerActivatedAt >= 0f && (currentTime - _controllerActivatedAt) <= DoubleTapTimeWindow)
                {
                    _isFirstPersonView = !_isFirstPersonView;
                    Logger.Info($"_isFirstPersonView toggled to: {_isFirstPersonView}");
                    entityPlayerLocal.SwitchFirstPersonView(!_isFirstPersonView.Value);
                    Logger.Info("First-person view toggled via controller double-tap.");
                }

                _controllerActivatedAt = currentTime;
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
            }

            if (entityPlayerLocal.bFirstPersonView == _isFirstPersonView)
            {
                return;
            }

            entityPlayerLocal.SwitchFirstPersonView(_isFirstPersonView.Value);
            Logger.Info("Switched player view based on current _isFirstPersonView value.");
        }
    }
}