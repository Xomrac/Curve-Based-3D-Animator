using System;
using System.Linq;
using UnityEngine;

namespace XomracTools
{

    [Serializable]
    public class CurveData
    {
        public AnimationCurve xCurve;
        public float xMultiplier = 1;
        public AnimationCurve yCurve;
        public float yMultiplier = 1;
        public AnimationCurve zCurve;
        public float zMultiplier = 1;

        public Vector3 Multipliers => new Vector3(xMultiplier, yMultiplier, zMultiplier);

        public Vector3 EvaluateCurves(float time) => new Vector3(xCurve.Evaluate(time), yCurve.Evaluate(time), zCurve.Evaluate(time));

        public void NormalizeCurves()
        {
            if (xCurve.keys.Last().time != 1)
            {
                xCurve.AddKey(1, 0);
            }
            if (yCurve.keys.Last().time != 1)
            {
                yCurve.AddKey(1, 0);
            }
            if (zCurve.keys.Last().time != 1)
            {
                zCurve.AddKey(1, 0);
            }
        }

    }

    [CreateAssetMenu(fileName = "CustomAnimation_", menuName = "Custom Animations/Animation")]
    public class CustomAnimation : ScriptableObject
    {

        public delegate void RepaintAction();
        public event RepaintAction WantRepaint;
        [SerializeField] private bool usePosition = false;
        public bool UsePosition => usePosition;

        [SerializeField] private bool useRotation = false;
        public bool UseRotation => useRotation;

        [SerializeField] private bool useScale = false;
        public bool UseScale => useScale;

        [SerializeField] private float animationTime = 1;
        public float AnimationTime => animationTime;

        [SerializeField] private CurveData positionCurves;
        public CurveData PositionCurves => positionCurves;

        [SerializeField] private CurveData rotationCurves;
        public CurveData RotationCurves => rotationCurves;

        [SerializeField] private CurveData scaleCurves;
        public CurveData ScaleCurves => scaleCurves;

        private void Repaint()
        {
                WantRepaint?.Invoke();
            
        }
        private void OnValidate()
        {
            positionCurves.NormalizeCurves();
            rotationCurves.NormalizeCurves();
            scaleCurves.NormalizeCurves();
            Repaint();
        }

    }

}