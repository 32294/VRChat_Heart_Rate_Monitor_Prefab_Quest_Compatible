using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;
using System.Collections.Generic;

[CustomEditor(typeof(HeartRateSoundObject))]
public class HeartRateSoundObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HeartRateSoundObject script = (HeartRateSoundObject)target;

        EditorGUILayout.LabelField("Menu toggles", EditorStyles.boldLabel);
        script.toggleOnOff = EditorGUILayout.Toggle("Sound On/off (+1 synced bits)", script.toggleOnOff);

        EditorGUILayout.Space();
        if (GUILayout.Button("Get Sound Object"))
        {

            string selectedPrefabName = "Heartbeat";
            
            if (script.toggleOnOff) {
                selectedPrefabName += "_Toggle";
            }

            GameObject selectedPrefab = (GameObject)Resources.Load(selectedPrefabName);

            MonoBehaviour thisComponent = (MonoBehaviour)target;
            GameObject instance = Instantiate(selectedPrefab, thisComponent.gameObject.transform.parent);
            instance.name = "Heartbeat Sound";

        }

        EditorUtility.SetDirty(script);
    }
}
