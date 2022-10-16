using UnityEditor;
using UnityEngine;

namespace XomracTools
{

    [CustomEditor(typeof(CustomAnimation))]
    public class CustomAnimationEditor : Editor
    {
        public override bool RequiresConstantRepaint()
        {
            return true;
        }
        private CustomAnimation Target => (CustomAnimation)target;

        private void OnEnable()
        {
            Target.WantRepaint += Repaint;
        }

        private void OnDisable()
        {
            Target.WantRepaint -= Repaint;
        }

        public override void OnInspectorGUI()
        {
            int baseIndent = EditorGUI.indentLevel;
            int smallerIndent = baseIndent - 1;
            int biggerIndent = baseIndent + 1;
            SerializedProperty usePositionProperty = serializedObject.FindProperty("usePosition");
            SerializedProperty useRotationProperty = serializedObject.FindProperty("useRotation");
            SerializedProperty useScaleProperty = serializedObject.FindProperty("useScale");
            SerializedProperty animationTimeProperty = serializedObject.FindProperty("animationTime");
            SerializedProperty positionCurvesProperty = serializedObject.FindProperty("positionCurves");
            SerializedProperty rotationCurvesProperty = serializedObject.FindProperty("rotationCurves");
            SerializedProperty scaleCurvesProperty = serializedObject.FindProperty("scaleCurves");

            animationTimeProperty.floatValue = EditorGUILayout.FloatField(animationTimeProperty.displayName, animationTimeProperty.floatValue);
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginVertical();
            EditorGUI.indentLevel=smallerIndent;
            usePositionProperty.boolValue = EditorGUILayout.BeginToggleGroup("Position", usePositionProperty.boolValue);
            EditorGUILayout.Space(-40);
            EditorGUI.indentLevel=biggerIndent;
            if (usePositionProperty.boolValue)
            {
                EditorGUI.indentLevel=baseIndent;
                EditorGUILayout.PropertyField(positionCurvesProperty);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUI.indentLevel=smallerIndent;
            useRotationProperty.boolValue = EditorGUILayout.BeginToggleGroup("Rotation", useRotationProperty.boolValue);
            EditorGUILayout.Space(-40);
            EditorGUI.indentLevel=biggerIndent;
            if (useRotationProperty.boolValue)
            {
                EditorGUI.indentLevel=baseIndent;
                EditorGUILayout.PropertyField(rotationCurvesProperty);
            }

            EditorGUILayout.EndToggleGroup();
            EditorGUI.indentLevel=smallerIndent;
            useScaleProperty.boolValue = EditorGUILayout.BeginToggleGroup("Scale", useScaleProperty.boolValue);
            EditorGUILayout.Space(-40);
            EditorGUI.indentLevel=biggerIndent;
            if (useScaleProperty.boolValue)
            {
                EditorGUI.indentLevel=baseIndent;
                EditorGUILayout.PropertyField(scaleCurvesProperty);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }

}