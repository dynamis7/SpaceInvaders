using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ObjectWithLifes
{
	[ConditionalField("hasPoints"), Header("Points"), SerializeField] private float points = default;
	[SerializeField] private bool hasPoints = default;

	protected override void Awake()
	{
		base.Awake();
		if (hasPoints) onDeadEvent.AddListener(delegate { Player.player.AddPoints(points); });
	}
}
