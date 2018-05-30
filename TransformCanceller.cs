using UnityEngine;

public class TransformCanceller : MonoBehaviour {

	public Vector3 positionCancelMultiplier = new Vector3(-1f,-1f,-1f);

	public Transform target;
	public Transform positionCanceller;
	public Transform rotationCanceller;

	// Use this for initialization
	void Start () {
		if ( target == null || positionCanceller == null || rotationCanceller == null) MakeCancellerParents();
	}

	// Update is called once per frame
	void Update () {

		rotationCanceller.localRotation = Quaternion.Inverse( target.localRotation );
		positionCanceller.localPosition = Vector3.Scale( positionCanceller.InverseTransformPoint( target.position ) , positionCancelMultiplier );

	}

	public Vector3 GetOriginalPosition () {

		if (positionCanceller != null) {

			return positionCanceller.parent.position + target.localPosition;

		}

		return target.position;

	}

	public Quaternion GetOriginalRotation () {

		if (positionCanceller != null) {

			return positionCanceller.parent.rotation * target.localRotation;

		}

		return target.rotation;


	}

	public void MakeCancellerParents() {

		target = transform;

		GameObject gobj_positionCanceller = new GameObject();
		gobj_positionCanceller.name = "Canceller (Position) For: " + target.gameObject.name;

		GameObject gobj_rotationCanceller = new GameObject();
		gobj_rotationCanceller.name = "Canceller (Rotation) For: " + target.gameObject.name;

		positionCanceller = gobj_positionCanceller.transform;
		rotationCanceller = gobj_rotationCanceller.transform;

		//
		// original_parent
		//   positionCanceller
		//     rotationCanceller
		//       target
		//
		positionCanceller.SetParent( target.parent , false );
		rotationCanceller.SetParent( positionCanceller , false );
		target.SetParent( rotationCanceller , false );

	}

	public void RemoveCancellerParents() {

		target.SetParent( positionCanceller.parent );

		DestroyImmediate( positionCanceller.gameObject );

		positionCanceller = null;
		rotationCanceller = null;

	}

}
