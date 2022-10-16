using UnityEditor;
using UnityEngine;

namespace XomracTools
{

	[CustomEditor(typeof(Custom3DAnimator))]
	public class Custom3DAnimatorEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			var script = (Custom3DAnimator)target;
			if (GUILayout.Button("Test Animation"))
			{
				script.TestAnimation(CurveType.All, true);
			}
			if (GUILayout.Button("Stop Animation"))
			{
				script.StopPreview();
			}

			serializedObject.ApplyModifiedProperties();
		}

	}

}

