using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEdgeCollider : MonoBehaviour
{
	private bool alienEnteredEdge = false;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<Alien>()) alienEnteredEdge = true;
		StartCoroutine("Check");
	}
	private IEnumerator Check()
	{
		yield return new WaitForEndOfFrame();
		if (alienEnteredEdge)
		{
			GameManager.Manager.ChangeDirection();
			alienEnteredEdge = false;
		}
	}
}
