using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {

	public enum MoveType {

		CircularPath , EightPath

	}

	public MoveType moveType;

	public float speed = 1f;

	public bool useRelative = true;
	public Vector3 origin = Vector3.zero;
	public Vector3 multiplier = Vector3.one;

	public bool useX = true;
	public bool useY = false;
	public bool useZ = true;

	public bool alignToMovement = true;

	private Vector3 _lastPosition;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		float t = Time.time * speed;
		float sint = Mathf.Sin(t);
		float cost = Mathf.Cos(t);
		Vector3 pos;

		_lastPosition = transform.position;

		switch (moveType) {
			case MoveType.CircularPath:
				pos = origin + Vector3.Scale( multiplier , new Vector3( cost , sint , sint ) );
				SetPosition( pos );
				break;
			case MoveType.EightPath:
				pos = origin + Vector3.Scale( multiplier , new Vector3( sint , sint , sint * cost ) );
				SetPosition( pos );
				break;
		}
		if (alignToMovement) AlignToMovement();

	}

	void SetPosition(Vector3 pos) {
		Vector3 _pos;
		if (useRelative) {
			_pos = transform.localPosition;
			if (useX) { _pos.x = pos.x; }
			if (useY) { _pos.y = pos.y; }
			if (useZ) { _pos.z = pos.z; }
			transform.localPosition = _pos;
		} else {
			_pos = transform.position;
			if (useX) { _pos.x = pos.x; }
			if (useY) { _pos.y = pos.y; }
			if (useZ) { _pos.z = pos.z; }
			transform.position = _pos;
		}
	}

	void AlignToMovement() {
		Vector3 delta = _lastPosition - transform.position;
		transform.forward = Vector3.RotateTowards( transform.forward , delta.normalized , 0.1f , 0.1f );
	}

}
