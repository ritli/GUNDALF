using UnityEngine;
using System.Collections;
using UnityEditor;

//[CustomEditor(typeof(Item))]
public class ItemEditor : Editor{
    
    SerializedProperty m_itemType;
    SerializedProperty m_sprite;
    SerializedProperty m_EquipOnPickup;
    SerializedProperty m_stats;
    SerializedProperty m_gunstats;

     void OnEnable()
    {
        m_itemType = serializedObject.FindProperty("m_itemType");

        m_stats = serializedObject.FindProperty("m_stats");
        m_gunstats = serializedObject.FindProperty("m_gunStats");

        m_sprite = serializedObject.FindProperty("m_sprite");
        m_EquipOnPickup = serializedObject.FindProperty("m_EquipOnPickup");
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_sprite);

        EditorGUILayout.PropertyField(m_itemType);

        GUIContent content = new GUIContent();
        content.text = "Wat";


      

        switch ((ItemType)m_itemType.intValue)
        {
            case ItemType.gun:



                EditorGUILayout.PropertyField(m_gunstats);

                break;
            case ItemType.armor:
                break;
            case ItemType.trinket:
                break;
            default:
                break;
        }
        EditorGUILayout.PropertyField(m_EquipOnPickup);

        serializedObject.ApplyModifiedProperties();
    }
}
