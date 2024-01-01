using BepInEx;
using HarmonyLib;
using PrecursorSub;
using System;
using UnityEngine;

[BepInPlugin("com.blizzard.subnautica.precursorsub.mod", "PrecursorSub", "1.0")]
[BepInDependency("com.mikjaw.subnautica.vehicleframework.mod")]
[BepInDependency("com.snmodding.nautilus")]
public class MainPatcher : BaseUnityPlugin
{
    public void Start()
    {
        var harmony = new Harmony("com.blizzard.subnautica.precursorsub.mod");
        harmony.PatchAll();
        UWE.CoroutineHost.StartCoroutine(PrecursorSub.PrecursorSub.Register());
    }
   
}
