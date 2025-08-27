using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;
using System.Collections.Generic;

[CustomEditor(typeof(HeartRateMonitorSettings))]
public class HeartRateMonitorSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HeartRateMonitorSettings script = (HeartRateMonitorSettings)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Basic Settings", EditorStyles.boldLabel);
        script.selectedPlatform = (HeartRateMonitorSettings.PlatformOption)EditorGUILayout.EnumPopup("Platform", script.selectedPlatform);
        script.selectedStyle = (HeartRateMonitorSettings.StyleOption)EditorGUILayout.EnumPopup("Style", script.selectedStyle);
        if (script.selectedPlatform == HeartRateMonitorSettings.PlatformOption.Quest && script.selectedStyle == HeartRateMonitorSettings.StyleOption.HUD) {
        } else if (script.selectedStyle != HeartRateMonitorSettings.StyleOption.Heart && script.selectedStyle != HeartRateMonitorSettings.StyleOption.HUD) {
            script.selectedColor = (HeartRateMonitorSettings.ColorOption)EditorGUILayout.EnumPopup("Color", script.selectedColor);
        } else {
            script.selectedColorHUD = (HeartRateMonitorSettings.ColorOptionHUD)EditorGUILayout.EnumPopup("Color", script.selectedColorHUD);
        }
        if (script.selectedStyle == HeartRateMonitorSettings.StyleOption.OutlinedText && script.selectedPlatform == HeartRateMonitorSettings.PlatformOption.PC) {
            script.selectedOutlinedTextColor = (HeartRateMonitorSettings.OutlinedTextColorOption)EditorGUILayout.EnumPopup("Outline Color", script.selectedOutlinedTextColor);
        } else if (script.selectedStyle == HeartRateMonitorSettings.StyleOption.OutlinedText && script.selectedPlatform == HeartRateMonitorSettings.PlatformOption.Quest) {
            script.selectedOutlinedTextColorQuest = (HeartRateMonitorSettings.OutlinedTextColorOptionQuest)EditorGUILayout.EnumPopup("Outline Color", script.selectedOutlinedTextColorQuest);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Behavior", EditorStyles.boldLabel);
        script.autoTurnOff = EditorGUILayout.Toggle("Automatically Turn Off at 0", script.autoTurnOff);
        // if (script.selectedPlatform == HeartRateMonitorSettings.PlatformOption.PC && script.selectedStyle == HeartRateMonitorSettings.StyleOption.HUD) {
        //     script.useSound = EditorGUILayout.Toggle("Heartbeat Sound", script.useSound);
        // } else {
        //     script.useSound = false;
        // }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Menu toggles (+1 synced bits for each one selected)", EditorStyles.boldLabel);
        script.toggleOnOff = EditorGUILayout.Toggle("On/off", script.toggleOnOff);
        script.toggleAct = EditorGUILayout.Toggle("Act Dead", script.toggleAct);
        // if (script.useSound) {
        //     script.toggleSound = EditorGUILayout.Toggle("Headbeat Sound Toggle", script.toggleSound);
        // } else {
        //     script.toggleSound = false;
        // }
        if (script.selectedStyle == HeartRateMonitorSettings.StyleOption.HUD) {
            script.toggleHUD = EditorGUILayout.Toggle("Hud (local, +0 bits)", script.toggleHUD);
        } else {
            script.toggleHUD = false;
        }


        EditorGUILayout.Space();
        if (GUILayout.Button("Configure"))
        {

            string selectedPrefabName = "";
            
            selectedPrefabName += script.selectedPlatform + "_";
            selectedPrefabName += script.selectedStyle;

            if (script.selectedStyle == HeartRateMonitorSettings.StyleOption.OutlinedText && script.selectedPlatform == HeartRateMonitorSettings.PlatformOption.PC) {
                selectedPrefabName += "_" + script.selectedOutlinedTextColor;
            } else if (script.selectedStyle == HeartRateMonitorSettings.StyleOption.OutlinedText && script.selectedPlatform == HeartRateMonitorSettings.PlatformOption.Quest) {
                selectedPrefabName += "_" + script.selectedOutlinedTextColorQuest;
            }

            GameObject selectedPrefab = (GameObject)Resources.Load(selectedPrefabName);

            if (selectedPrefab != null) {

                MonoBehaviour thisComponent = (MonoBehaviour)target;
                GameObject instance = Instantiate(selectedPrefab, thisComponent.gameObject.transform.parent);
                instance.name = "Heartrate Monitor";
                
                // set preset path
                string pcPresetPath = "Assets/32294/heartrate_monitor/scripts/presets/";
                string presetPath = pcPresetPath;
                if (script.selectedPlatform != HeartRateMonitorSettings.PlatformOption.PC) {
                    presetPath = "Assets/32294/heartrate_monitor/scripts/presets/quest/";
                }

                // determine what the component values need to be
                // create list
                var presetsToApply = new List<string> {};
                
                // add the number fx layer
                if (script.autoTurnOff) { presetsToApply.Add(pcPresetPath + "number/number_autooff.preset"); }
                else { presetsToApply.Add(pcPresetPath + "number/number.preset"); }
                
                // add the on fx layer if it makes sense
                if (script.selectedStyle == HeartRateMonitorSettings.StyleOption.ScreenHeart || script.selectedStyle == HeartRateMonitorSettings.StyleOption.ScreenSquare) {
                    if (script.autoTurnOff) { presetsToApply.Add(pcPresetPath + "on/on_autooff.preset"); }
                    else { presetsToApply.Add(pcPresetPath + "on/on.preset"); }
                }

                // add the color fx layer
                if (script.selectedStyle == HeartRateMonitorSettings.StyleOption.Heart) {

                    if (script.selectedColorHUD == HeartRateMonitorSettings.ColorOptionHUD.RGB) {
                        if (script.autoTurnOff) { presetsToApply.Add(presetPath + "color/heart_rgb_autooff.preset"); }
                        else { presetsToApply.Add(presetPath + "color/heart_rgb.preset"); }
                    } else {
                        if (script.autoTurnOff) { presetsToApply.Add(presetPath + "color/heart_default_autooff.preset"); }
                        else { presetsToApply.Add(presetPath + "color/heart_default.preset"); }
                    }

                } else if (script.selectedStyle == HeartRateMonitorSettings.StyleOption.HUD) {
                    
                    if (script.selectedColorHUD == HeartRateMonitorSettings.ColorOptionHUD.RGB && script.selectedPlatform == HeartRateMonitorSettings.PlatformOption.PC) {
                        if (script.autoTurnOff) { presetsToApply.Add(presetPath + "color/hud_rgb_autooff.preset"); }
                        else { presetsToApply.Add(presetPath + "color/hud_rgb.preset"); }
                    } else {
                        if (script.autoTurnOff) { presetsToApply.Add(presetPath + "color/hud_default_autooff.preset"); }
                        else { presetsToApply.Add(presetPath + "color/hud_default.preset"); }
                    }

                } else {
                    
                    if (script.selectedColor == HeartRateMonitorSettings.ColorOption.RGB) {
                        if (script.autoTurnOff) { presetsToApply.Add(presetPath + "color/rgb_autooff.preset"); }
                        else { presetsToApply.Add(presetPath + "color/rgb.preset"); }
                    } else if (script.selectedColor == HeartRateMonitorSettings.ColorOption.White) {
                        if (script.autoTurnOff) { presetsToApply.Add(presetPath + "color/white_autooff.preset"); }
                        else { presetsToApply.Add(presetPath + "color/white.preset"); }            
                    } else {
                        if (script.autoTurnOff) { presetsToApply.Add(presetPath + "color/default_autooff.preset"); }
                        else { presetsToApply.Add(presetPath + "color/default.preset"); }
                    }

                }

                // add the on off toggle if neccessary
                if (script.toggleOnOff) { presetsToApply.Add(pcPresetPath + "menu/on_off.preset"); }
                // add the act dead toggle if neccessary
                if (script.toggleAct) { presetsToApply.Add(pcPresetPath + "menu/act_dead.preset"); }
                // add the act HUD toggle if neccessary
                if (script.toggleHUD) { presetsToApply.Add(pcPresetPath + "menu/hud.preset"); }

                // get components
                Component[] components = instance.GetComponents<Component>();
                
                // loop through components
                int count = 0;
                foreach (Component comp in components) {

                    if (comp.GetType().Name == "VRCFury") {
    
                        count += 1;

                        // check if it's the first time to either delete or keep the menu icon override
                        if (count == 1) {
                            
                            if (!script.toggleOnOff && !script.toggleAct && !script.toggleHUD) { DestroyImmediate(comp); }
                            continue;

                        // check if it's the second time to skip the viewpoint constraint on the hud models
                        } else if (count == 2 && script.selectedStyle == HeartRateMonitorSettings.StyleOption.HUD) {

                            continue;

                        }

                        // apply preset from list if avaliable, otherwise delete the extra component
                        if (presetsToApply.Count == 0) {
                            DestroyImmediate(comp);
                        } else {    
                            Preset preset = AssetDatabase.LoadAssetAtPath<Preset>(presetsToApply[0]);
                            preset.ApplyTo(comp);
                            presetsToApply.RemoveAt(0);   
                        }

                    }

                }

            } else {
                
                Debug.LogWarning("Prefab not found.");
                Debug.LogWarning(selectedPrefabName);
            
            }

        }

        EditorUtility.SetDirty(script);
    }
}
