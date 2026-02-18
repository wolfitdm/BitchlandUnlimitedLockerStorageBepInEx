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

            PatchAllHarmonyMethods();

            Logger.LogInfo($"Plugin BitchlandUnlimitedLockerStorageBepInEx BepInEx is loaded!");
        }

        public static void PatchAllHarmonyMethods()
        {
            if (!enableThisMod)
            {
                return;
            }

            try
            {
                PatchHarmonyMethodUnity(typeof(Int_Storage), "SendTo", "Int_Storage_SendTo", true, false);
                PatchHarmonyMethodUnity(typeof(Int_Storage), "Interact", "Int_Storage_Interact", true, false);
                PatchHarmonyMethodUnity(typeof(int_personStorage), "SendTo", "int_personStorage_SendTo", true, false);
                PatchHarmonyMethodUnity(typeof(misc_invItem), "Click_TakeButton", "misc_invItem_Click_TakeButton", true, false);
                PatchHarmonyMethodUnity(typeof(misc_invItem), "Click_PutButton", "Click_PutButton", true, false);
                PatchHarmonyMethodUnity(typeof(misc_invItem), "Click_Opem", "Click_Opem", true, false);
                PatchHarmonyMethodUnity(typeof(misc_invItem), "Click_SendInvToBackpack", "Click_SendInvToBackpack", true, false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }
        }

        public static void PatchHarmonyMethodUnity(Type originalClass, string originalMethodName, string patchedMethodName, bool usePrefix, bool usePostfix, Type[] parameters = null)
        {
            string uniqueId = "com.wolfitdm.BitchlandUnlimitedLockerStorageBepInEx";
            Type uniqueType = typeof(BitchlandUnlimitedLockerStorageBepInEx);

            // Create a new Harmony instance with a unique ID
            var harmony = new Harmony(uniqueId);

            if (originalClass == null)
            {
                Logger.LogInfo($"GetType originalClass == null");
                return;
            }

            MethodInfo patched = null;

            try
            {
                patched = AccessTools.Method(uniqueType, patchedMethodName);
            }
            catch (Exception ex)
            {
                patched = null;
            }

            if (patched == null)
            {
                Logger.LogInfo($"AccessTool.Method patched {patchedMethodName} == null");
                return;

            }

            // Or apply patches manually
            MethodInfo original = null;

            try
            {
                if (parameters == null)
                {
                    original = AccessTools.Method(originalClass, originalMethodName);
                }
                else
                {
                    original = AccessTools.Method(originalClass, originalMethodName, parameters);
                }
            }
            catch (AmbiguousMatchException ex)
            {
                Type[] nullParameters = new Type[] { };
                try
                {
                    if (patched == null)
                    {
                        parameters = nullParameters;
                    }

                    ParameterInfo[] parameterInfos = patched.GetParameters();

                    if (parameterInfos == null || parameterInfos.Length == 0)
                    {
                        parameters = nullParameters;
                    }

                    List<Type> parametersN = new List<Type>();

                    for (int i = 0; i < parameterInfos.Length; i++)
                    {
                        ParameterInfo parameterInfo = parameterInfos[i];

                        if (parameterInfo == null)
                        {
                            continue;
                        }

                        if (parameterInfo.Name == null)
                        {
                            continue;
                        }

                        if (parameterInfo.Name.StartsWith("__"))
                        {
                            continue;
                        }

                        Type type = parameterInfos[i].ParameterType;

                        if (type == null)
                        {
                            continue;
                        }

                        parametersN.Add(type);
                    }

                    parameters = parametersN.ToArray();
                }
                catch (Exception ex2)
                {
                    parameters = nullParameters;
                }

                try
                {
                    original = AccessTools.Method(originalClass, originalMethodName, parameters);
                }
                catch (Exception ex2)
                {
                    original = null;
                }
            }
            catch (Exception ex)
            {
                original = null;
            }

            if (original == null)
            {
                Logger.LogInfo($"AccessTool.Method original {originalMethodName} == null");
                return;
            }

            HarmonyMethod patchedMethod = new HarmonyMethod(patched);
            var prefixMethod = usePrefix ? patchedMethod : null;
            var postfixMethod = usePostfix ? patchedMethod : null;

            harmony.Patch(original,
                prefix: prefixMethod,
                postfix: postfixMethod);
        }
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
        public static bool misc_invItem_Click_TakeButton(object __instance)
        {
            if (!enableThisMod)
            {
                return true;
            }

            return true;
        }
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
