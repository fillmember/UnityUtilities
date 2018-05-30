

using UnityEditor;
using UnityEngine;

namespace ArtificialRome {

  [CustomEditor( typeof(TransformMimicker) ), CanEditMultipleObjects]
  public class TransformMimickerEditor : Editor {

    public override void OnInspectorGUI() {

      DrawDefaultInspector();

      if ( GUILayout.Button("Current State To Initial Transform") ) {

        foreach ( TransformMimicker mi in targets ) {

          Transform miTarget = mi.target;
          Transform miTransform = mi.gameObject.transform;

          mi.offset = mi.gameObject.transform.position -
            TransformMimicker.VectorTransform(
              mi.useRelative ? mi.target.localPosition : mi.target.position ,
              mi.multiplier , Vector3.zero
            )
          ;
          mi.rOffset = mi.gameObject.transform.rotation.eulerAngles -
            TransformMimicker.VectorTransform(
              mi.useRelative ? mi.target.localRotation.eulerAngles : mi.target.rotation.eulerAngles ,
              mi.rMultiplier , Vector3.zero
            )
          ;
        }

      }

    }

  }

}
