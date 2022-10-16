using UnityEditor;
using UnityEngine;

namespace XomracTools
{
    

    [CustomPropertyDrawer(typeof(CurveData))]
    public class CurveDataEditor : PropertyDrawer
    {
        private const float buttonSize= 45F;
        private bool foldout = false;
        private float standardLabelWidth = EditorGUIUtility.labelWidth;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty xCurveProp = property.FindPropertyRelative("xCurve");
            SerializedProperty yCurveProp = property.FindPropertyRelative("yCurve");
            SerializedProperty zCurveProp = property.FindPropertyRelative("zCurve");
            SerializedProperty xMultiplierProp = property.FindPropertyRelative("xMultiplier");
            SerializedProperty yMultiplierProp = property.FindPropertyRelative("yMultiplier");
            SerializedProperty zMultiplierProp = property.FindPropertyRelative("zMultiplier");

            
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.GetControlRect (true, 16f, EditorStyles.foldout);
            Rect foldRect = GUILayoutUtility.GetLastRect ();
            if (Event.current.type == EventType.MouseUp && foldRect.Contains (Event.current.mousePosition)) {
                foldout = !foldout;
                GUI.changed = true;
                Event.current.Use();
            }
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, label);
            if (foldout)
            {
                EditorGUIUtility.labelWidth = 60;
                EditorGUILayout.BeginHorizontal();
                xCurveProp.animationCurveValue = EditorGUILayout.CurveField(xCurveProp.animationCurveValue,Color.red,new Rect(0,-1,1,2));
                xMultiplierProp.floatValue = EditorGUILayout.FloatField("Multiplier",xMultiplierProp.floatValue);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                yCurveProp.animationCurveValue = EditorGUILayout.CurveField(yCurveProp.animationCurveValue,Color.green,new Rect(0,-1,1,2));
                yMultiplierProp.floatValue = EditorGUILayout.FloatField("Multiplier",yMultiplierProp.floatValue);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                zCurveProp.animationCurveValue = EditorGUILayout.CurveField(zCurveProp.animationCurveValue,Color.blue,new Rect(0,-1,1,2));
                zMultiplierProp.floatValue = EditorGUILayout.FloatField("Multiplier",zMultiplierProp.floatValue);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.EndFoldoutHeaderGroup();
            property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndChangeCheck();
            
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }

}