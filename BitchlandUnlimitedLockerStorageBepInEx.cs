using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using HarmonyLib;
using SemanticVersioning;
using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine;

namespace BitchlandUnlimitedLockerStorageBepInEx
{
    [BepInPlugin("com.wolfitdm.BitchlandUnlimitedLockerStorageBepInEx", "BitchlandUnlimitedLockerStorageBepInEx Plugin", "1.0.0.0")]
    public class BitchlandUnlimitedLockerStorageBepInEx : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private ConfigEntry<bool> configEnableMe;

        public BitchlandUnlimitedLockerStorageBepInEx()
        {
        }

        public static Type MyGetType(string originalClassName)
        {
            return Type.GetType(originalClassName + ",Assembly-CSharp");
        }

        private static string pluginKey = "General.Toggles";

        public static bool enableThisMod = false;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            configEnableMe = Config.Bind(pluginKey,
                                              "EnableThisMod",
                                              true,
                                             "Whether or not you want enable this mod (default true also yes, you want it, and false = no)");


            enableThisMod = configEnableMe.Value;

            Harmony.CreateAndPatchAll(typeof(BitchlandUnlimitedLockerStorageBepInEx));

            Logger.LogInfo($"Plugin BitchlandUnlimitedLockerStorageBepInEx BepInEx is loaded!");
        }

        [HarmonyPatch(typeof(Int_Storage), "SendTo")]
        [HarmonyPrefix] // call before the original method is called
        public static bool Int_Storage_SendTo(GameObject item, Int_Storage storage, object __instance)
        {
            if (!enableThisMod)
            {
                return true;
            }

            try
            {
                if (storage == null)
                {
                    return true;
                }
                storage.StorageMax = int.MaxValue;
                Int_Storage _this = (Int_Storage)__instance;
                if (_this != null)
                {
                    _this.StorageMax = int.MaxValue;
                }
            }
            catch (Exception e)
            {
                return true;
            }

            return true;
        }

        [HarmonyPatch(typeof(int_personStorage), "SendTo")]
        [HarmonyPrefix] // call before the original method is called
        public static bool int_personStorage_SendTo(GameObject item, Int_Storage storage, object __instance)
        {
            if (!enableThisMod)
            {
                return true;
            }

            try
            {
                if (storage == null)
                {
                    return true;
                }
                storage.StorageMax = int.MaxValue;
                int_personStorage _this = (int_personStorage)__instance;
                if (_this != null)
                {
                    _this.StorageMax = int.MaxValue;
                }
            }
            catch (Exception e)
            {
                return true;
            }

            return true;
        }

        [HarmonyPatch(typeof(Int_Storage), "Interact")]
        [HarmonyPrefix] // call before the original method is called
        public static bool Int_Storage_Interact(Person person, object __instance)
        {
            if (!enableThisMod)
            {
                return true;
            }

            try
            {
                Int_Storage _this = (Int_Storage)__instance;
                if (_this != null)
                {
                    _this.StorageMax = int.MaxValue;
                }
            }
            catch (Exception e)
            {
                return true;
            }


            return true;
        }

        [HarmonyPatch(typeof(misc_invItem), "Click_TakeButton")]
        [HarmonyPrefix] // call before the original method is called
        public static bool misc_invItem_Click_TakeButton(object __instance)
        {
            if (!enableThisMod)
            {
                return true;
            }

            return true;
        }

        [HarmonyPatch(typeof(misc_invItem), "Click_PutButton")]
        [HarmonyPrefix] // call before the original method is called
        public static bool Click_PutButton(object __instance)
        {
            if (!enableThisMod)
            {
                return true;
            }

            try
            {
                misc_invItem _this = (misc_invItem)__instance;
                if (_this != null && _this.ThisStorage != null)
                {
                    _this.ThisStorage.StorageMax = int.MaxValue;
                }
            }
            catch (Exception e)
            {
                return true;
            }

            return true;
        }

        [HarmonyPatch(typeof(misc_invItem), "Click_Opem")]
        [HarmonyPrefix] // call before the original method is called
        public static bool Click_Opem(object __instance)
        {
            if (!enableThisMod)
            {
                return true;
            }

            try
            {
                misc_invItem _this = (misc_invItem)__instance;
                if (_this != null && _this.ThisStorage != null)
                {
                    _this.ThisStorage.StorageMax = int.MaxValue;
                }
            }
            catch (Exception e)
            {
                return true;
            }

            return true;
        }

        [HarmonyPatch(typeof(misc_invItem), "Click_SendInvToBackpack")]
        [HarmonyPrefix] // call before the original method is called
        public static bool Click_SendInvToBackpack(object __instance)
        {
            if (!enableThisMod)
            {
                return true;
            }

            try
            {
                misc_invItem _this = (misc_invItem)__instance;
                if (_this != null && _this.ThisStorage != null)
                {
                    _this.ThisStorage.StorageMax = int.MaxValue;
                }
            }
            catch (Exception e)
            {
                return true;
            }


            return true;
        }
    }
}
