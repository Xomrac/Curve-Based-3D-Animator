using System;
using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using Unity.EditorCoroutines.Editor;
#endif

namespace XomracTools
{

    public class Custom3DAnimator : MonoBehaviour
    {
        public CustomAnimation customAnimation;
        private Vector3 startPos;
        private Vector3 startRot;
        private Vector3 startScale;

        public void StartAnimation(float animationTime, CurveType curveType, Vector3 multiplier, bool toLoop)
        {
            StartCoroutine(Animate(animationTime, curveType, multiplier, toLoop));
        }

        public void StartAnimation(Vector3 multiplier, bool toLoop)
        {
            StartAnimation(customAnimation.AnimationTime, CurveType.Position, multiplier, toLoop);
            StartAnimation(customAnimation.AnimationTime, CurveType.Rotation, multiplier, toLoop);
            StartAnimation(customAnimation.AnimationTime, CurveType.Scale, multiplier, toLoop);
        }

        public void StartAnimation(CurveType curveType, Vector3 multiplier, bool toLoop)
        {
            StartAnimation(customAnimation.AnimationTime, curveType, multiplier, toLoop);
        }

        public void StartAnimation(float animationTime, CurveType curveType, bool toLoop)
        {
            StartAnimation(animationTime, curveType, Vector3.one, toLoop);
        }

        public void StartAnimation(CurveType curveType, bool toLoop)
        {
            StartAnimation(customAnimation.AnimationTime, curveType, Vector3.one, toLoop);
        }

        public void StartAnimation(bool toLoop)
        {
            StartAnimation(Vector3.one, toLoop);
        }

        public void StartAnimation()
        {
            StartAnimation(Vector3.one, false);
        }

        private Vector3 GetMultipliers(CurveType type)
        {
            switch (type)
            {
                case CurveType.Position:
                    Debug.Log($"Position: {customAnimation.PositionCurves.Multipliers}");
                    return customAnimation.PositionCurves.Multipliers;
                    break;
                case CurveType.Rotation:
                    Debug.Log($"Rotation: {customAnimation.RotationCurves.Multipliers}");
                    return customAnimation.RotationCurves.Multipliers;
                    break;
                case CurveType.Scale:
                    Debug.Log($"Scale: {customAnimation.ScaleCurves.Multipliers}");
                    return customAnimation.ScaleCurves.Multipliers;
                    break;
                
            }
            return Vector3.zero;
        }

        private Vector3 GetIncrement(CurveType type, float sizeDelta, Vector3 multiplier)
        {
            Vector3 curvesValues = type switch
            {
                CurveType.Position => customAnimation.PositionCurves.EvaluateCurves(sizeDelta),
                CurveType.Rotation => customAnimation.RotationCurves.EvaluateCurves(sizeDelta),
                CurveType.Scale => customAnimation.ScaleCurves.EvaluateCurves(sizeDelta),
                _ => Vector3.zero
            };
            return Vector3.Scale(curvesValues,multiplier);
        }

        private IEnumerator Animate(float animationTime, CurveType curveType, Vector3 multiplier, bool toLoop)
        {
             startPos = transform.localPosition;
             startRot = transform.localEulerAngles;
             startScale = transform.localScale;
            float elapsedTime = 0;
            while (elapsedTime < animationTime)
            {
                elapsedTime += Time.deltaTime;
                float sizeDelta = elapsedTime / animationTime;
                switch (curveType)
                {
                    case CurveType.Position:
                        if (customAnimation.UsePosition)
                        {
                            transform.localPosition = startPos + GetIncrement(curveType, sizeDelta, multiplier);
                        }
                        break;
                    case CurveType.Rotation:
                        if (customAnimation.UseRotation)
                        {
                            transform.localEulerAngles = startRot + GetIncrement(curveType, sizeDelta, multiplier);
                        }
                        break;
                    case CurveType.Scale:
                        if (customAnimation.UseScale)
                        {
                            transform.localScale = startScale + GetIncrement(curveType, sizeDelta, multiplier);
                        }
                        break;
                }
                yield return null;
            }
            if (toLoop)
            {
                StartCoroutine(Animate(animationTime, curveType, multiplier, true));
            }
        }

#if UNITY_EDITOR
        private EditorCoroutine editorCoroutine = null;

        public void TestAnimation(CurveType curveType, bool loop)
        {
            if (editorCoroutine == null)
            {
                Transform startTransform = transform;
                editorCoroutine = EditorCoroutineUtility.StartCoroutineOwnerless(AnimateInEditor(curveType, loop, startTransform));
            }
        }

        public void StopPreview()
        {
            if (editorCoroutine != null)
            {
                EditorCoroutineUtility.StopCoroutine(editorCoroutine);
                editorCoroutine = null;
                transform.position = startPos;
                transform.eulerAngles = startRot;
                transform.localScale = startScale;
            }
        }

        private IEnumerator AnimateInEditor(CurveType curveType, bool toLoop, Transform startTransform)
        {
            startPos = startTransform.localPosition;
            startRot = startTransform.localEulerAngles;
            startScale = startTransform.localScale;
            float elapsedTime = 0;
            while (elapsedTime < customAnimation.AnimationTime)
            {
                elapsedTime += Time.deltaTime;
                float sizeDelta = elapsedTime / customAnimation.AnimationTime;
                switch (curveType)
                {
                    case CurveType.Position:
                        if (customAnimation.UsePosition)
                        {
                            transform.localPosition = startPos + GetIncrement(CurveType.Position, sizeDelta, GetMultipliers(CurveType.Position));
                        }
                        break;
                    case CurveType.Rotation:
                        if (customAnimation.UseRotation)
                        {
                            transform.localEulerAngles = startRot + GetIncrement(CurveType.Rotation, sizeDelta, GetMultipliers(CurveType.Rotation));
                        }
                        break;
                    case CurveType.Scale:
                        if (customAnimation.UseScale)
                        {
                            transform.localScale = startScale + GetIncrement(CurveType.Scale, sizeDelta, GetMultipliers(CurveType.Scale));
                        }
                        break;

                    case CurveType.All:
                        if (customAnimation.UsePosition)
                        {
                            transform.localPosition = startPos + GetIncrement(CurveType.Position, sizeDelta, GetMultipliers(CurveType.Position));
                        }
                        if (customAnimation.UseRotation)
                        {
                            transform.localEulerAngles = startRot + GetIncrement(CurveType.Rotation, sizeDelta, GetMultipliers(CurveType.Rotation));
                        }
                        if (customAnimation.UseScale)
                        {
                            Debug.Log(GetMultipliers(CurveType.Scale));
                            Debug.Log(GetMultipliers(CurveType.Rotation));
                            Debug.Log(GetMultipliers(CurveType.Position));
                            transform.localScale = startScale + GetIncrement(CurveType.Scale, sizeDelta, GetMultipliers(CurveType.Scale));
                        }
                        break;
                }
                yield return null;
            }
            transform.position = startPos;
            transform.eulerAngles = startRot;
            transform.localScale = startScale;
            editorCoroutine = toLoop ? EditorCoroutineUtility.StartCoroutineOwnerless(AnimateInEditor(curveType, true, startTransform)) : null;
        }


#endif
    }

}