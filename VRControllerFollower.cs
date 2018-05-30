using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRControllerFollower : MonoBehaviour {

	public Transform controller;
	public Transform eye;

	public Transform origin;

	public bool usePosition = true;

	private Vector3 _vec;
	private Vector3 _vec2;

	public Vector3 multiplier = Vector3.one;
	public Vector3 offset = Vector3.zero;

	public bool useRotation = false;
	public Vector3 rMultiplier = Vector3.one;
	public Vector3 rOffset = Vector3.zero;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		// 0 : origin's rotation matches eye's rotation
		// 1 : Get Vector From Controller ~ Eye
		// 2 : Transform the Vector to origin's local space
		// 3 : transform.position = origin.position + ( that_vector + offset ) * multiplier

		// 0
		Quaternion q = origin.rotation;
		Vector3 qe = q.eulerAngles;
		qe.x = eye.rotation.eulerAngles.x * -1f;
		qe.z = eye.rotation.eulerAngles.z * -1f;
		q = Quaternion.Euler( qe );
		origin.rotation = q;

		if (usePosition) {

			// 1
			_vec = controller.localPosition - eye.localPosition;

			// 2
			_vec2 = eye.InverseTransformDirection( _vec ) + offset;

			// 3
			transform.position = origin.position +
				origin.forward * _vec2.z * multiplier.z +
				origin.right * _vec2.x * multiplier.x +
				origin.up * _vec2.y * multiplier.y;

		}

	}

	void LateUpdate () {

		if (useRotation) {

			Quaternion rq = transform.rotation;
			Vector3 rqe = rq.eulerAngles;
			rqe.x = controller.rotation.x * rMultiplier.x + rOffset.x;
			rqe.y = controller.rotation.y * rMultiplier.y + rOffset.y;
			rqe.z = controller.rotation.z * rMultiplier.z + rOffset.z;
			rq = Quaternion.Euler( rqe );
			transform.rotation = rq;

			// transform.forward = controller.forward;

		}

	}
}
