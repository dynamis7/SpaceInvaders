using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlienEdgeCollider : MonoBehaviour
{
	[SerializeField] private UnityEvent onAlienEnterEvent = default;
	private bool alienEnteredEdge = false;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<Alien>()) onAlienEnterEvent.Invoke();
	}
	public void ChangeDirection()
	{
		alienEnteredEdge = true;
		StartCoroutine("Check");
	}
	private IEnumerator Check()
	{
		yield return new WaitForEndOfFrame();
		if (alienEnteredEdge)
		{
			EnemiesSpawner.Spawner.ChangeDirection();
			alienEnteredEdge = false;
		}
	}
}
