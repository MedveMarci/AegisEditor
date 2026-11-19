using AegisEditor.API.Features.ServerObject;
using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;

namespace AegisEditor.Patches;

/// <summary>
/// Makes SCP-079 see and interact with doors spawned by <see cref="ServerDoorSpawner"/> from any camera.
/// </summary>
[HarmonyPatch(typeof(Scp079DoorAbility), nameof(Scp079DoorAbility.CheckVisibility))]
public static class Scp079CustomDoorVisibilityPatch
{
    public static bool Prefix(DoorVariant door, Scp079Camera currentCamera, ref bool __result)
    {
        if (!ServerDoorSpawner.CameraAffected.Contains(door.netId))
            return true;

        __result = door is not INonInteractableDoor nonInteractableDoor || !nonInteractableDoor.IgnoreLockdowns;
        return false;
    }
}