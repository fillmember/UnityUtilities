using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArtificialRome {
public class TransformJitter : MonoBehaviour {

	[Header("Configuration - Amount")]
	public Vector3 positionUpperBound;
	public Vector3 positionLowerBound;
	public Vector3 rotationUpperBound;
	public Vector3 rotationLowerBound;

	[Header("Configuration - Time")]
	[Range(0,1)] public float probability = 0.33f;

	[Header("Configuration - Damp")]

	public bool easeBackToInitialStates = true;
	[Range(0,1)] public float damping = 0.8f;

	// Internal properties
	private Vector3 initialPosition;
	private Quaternion initialRotation;
	private Vector3 targetPosition;
	private Quaternion targetRotation;

	void Start () {
		initialPosition = transform.position;
		initialRotation = transform.rotation;
		targetPosition = transform.position;
		targetRotation = transform.rotation;
	}

	void Update () {
		// Do we move this frame?
		bool move = probability == 1 ? true : Random.value < probability;
		// Compute candidate position/rotation values of this frame
		// This could be a random new value, or the initial value.
		Vector3 p = initialPosition + (
			move
				? Vector3.Lerp( positionUpperBound , positionLowerBound , Random.value )
				: Vector3.zero
			);
		Quaternion r = initialRotation * (
			move
				? Quaternion.Euler(
					Vector3.Lerp(
						rotationUpperBound, rotationLowerBound , Random.value
					)
				)
				: Quaternion.identity
		);
		// Apply candidate values when:
		// we are moving this frame, OR
		// We should ease back to initial state
		if (move || easeBackToInitialStates) {
			targetPosition = p;
			targetRotation = r;
		}
		// Lerp this transform to the target values
		transform.position = Vector3.Lerp(targetPosition,transform.position,damping);
		transform.rotation = Quaternion.Slerp(targetRotation,transform.rotation,damping);
	}

}

}
