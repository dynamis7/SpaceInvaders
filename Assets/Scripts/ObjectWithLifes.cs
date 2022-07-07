using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ObjectWithLifes : MonoBehaviour
{
	[Header("Life")]
	[SerializeField] private int maxLifes = default;
	[SerializeField] private bool showLifes = default;
	[ConditionalField("showLifes"), SerializeField] private Text lifesText = default;
	[SerializeField] protected UnityEvent onLifeLoseEvent = default; 
	[SerializeField] protected UnityEvent onDeadEvent = default;

	private int lifes = default;

	protected virtual void Awake()
	{
		lifes = maxLifes;
		if (showLifes) lifesText.text = lifes.ToString();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.GetComponent<Bullet>())
		{
			Destroy(other.gameObject);
			lifes--;
			if(showLifes) lifesText.text = lifes.ToString();
			onLifeLoseEvent.Invoke();
			CheckIfIsDead();
		}
	}

	private void CheckIfIsDead()
	{
		if (lifes <= 0)
		{
			onDeadEvent.Invoke();
			if (showLifes) lifesText.text = "0";
		}
	}

	public virtual void Destroy(float destroyTime = 0)
	{
		Destroy(gameObject, destroyTime);
	}
}
