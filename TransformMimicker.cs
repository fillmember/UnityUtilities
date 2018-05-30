using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMimicker : MonoBehaviour {

	public Transform target;

	public bool useRelative = true;

	public bool usePosition = true;

	public Vector3 multiplier = Vector3.one;
	public Vector3 offset = Vector3.zero;

	public bool useRotation = false;

	public Vector3 rMultiplier = Vector3.one;
	public Vector3 rOffset = Vector3.zero;

	public enum LifeCycle {
		Update , LateUpdate
	}

	public LifeCycle useLifeCycle = LifeCycle.Update;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

		if (useLifeCycle == LifeCycle.Update) { Mimic(); }

	}

	// LateUpdate
	void LateUpdate () {

		if (useLifeCycle == LifeCycle.LateUpdate) { Mimic(); }

	}

	void Mimic() {
		if (target == null) { return ; }

		if (usePosition) {

			if (useRelative) {

				transform.localPosition = VectorTransform( target.localPosition , multiplier , offset );

			} else {

				transform.position = VectorTransform( target.position , multiplier , offset );

			}

		}

		if (useRotation) {

			if (useRelative) {

				// transform.localRotation = Quaternion.Euler( Vector3.Scale( target.localRotation.eulerAngles , rMultiplier ) + rOffset );
				transform.localRotation = Quaternion.Euler( VectorTransform( target.localRotation.eulerAngles , rMultiplier , rOffset ) );

			} else {

				// transform.rotation = Quaternion.Euler( Vector3.Scale( target.rotation.eulerAngles , rMultiplier ) + rOffset );
				// transform.rotation = Quaternion.Euler( Vector3.Scale( target.rotation.eulerAngles , rMultiplier ) ) * Quaternion.Euler( rOffset );
				transform.rotation = Quaternion.Euler( VectorTransform( target.rotation.eulerAngles , rMultiplier , rOffset ) );

			}

		}
	}

	static public Vector3 VectorTransform( Vector3 _target , Vector3 _multi , Vector3 _offset ) {

		return Vector3.Scale( _target , _multi ) + _offset;

	}

}
