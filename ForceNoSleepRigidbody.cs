using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceNoSleepRigidbody : MonoBehaviour {

	Rigidbody rb;

	void Start () {

		rb = GetComponent<Rigidbody>();

	}

	void Update () {

		if ((rb != null) && rb.IsSleeping()) {
			rb.WakeUp();
		}

	}
}
