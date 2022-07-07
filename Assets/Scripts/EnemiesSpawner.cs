using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
	public delegate void OnDirectionChangedDelegate();
	public event OnDirectionChangedDelegate OnDirectionChanged;

	private float alienMoveTime;
	public float AlienMoveTime { get { return alienMoveTime; } private set { } }
	
	private float alienSpeed;
	public float AlienSpeed { get { return alienSpeed; } private set { } }

	private float alienBulletSpeed;
	public float AlienBulletSpeed { get { return alienBulletSpeed; } private set { } }

	private int aliensXMoveDirection = 1;
	public int AliensXMoveDirection { get { return aliensXMoveDirection; } private set { } }

	private int alienShootProbability;
	public int AlienShootProbability { get { return alienShootProbability; } private set { } }

	private float alienTimeForNextShoot;
	public float AlienTimeForNextShoot { get { return alienTimeForNextShoot; } private set { } }

	[SerializeField] private GameObject alienBullet;
	public GameObject AlienBullet { get { return alienBullet; } private set { } }

	public static EnemiesSpawner Spawner;

	[Header("Aliens spawning")]
	[SerializeField] private GameObject[] aliens = default;
	[SerializeField] private Transform aliensSpawnPoint = default;
	[Header("Ufo spawning")]
	[SerializeField] private GameObject ufoPrefab = default;
	[SerializeField] private Transform ufoLeftSpawnPoint = default;
	[SerializeField] private Transform ufoRightSpawnPoint = default;

	private int aliensPerRow = default;
	private float spaceBetweenAliensX = default;
	private float spaceBetweenAliensY = default;
	private Bullet bullet;
	private int aliensAmount;
	private float ufoInstantiateTime;
	private float time;
	private float newRowMultiplier;

	private void Awake()
	{
		Spawner = this;
		bullet = alienBullet.GetComponent<Bullet>();
	}
	private void Start()
	{
		aliensAmount = 0;
		SetGameDefaultData();
		InstantiateAliens();
	}
	private void Update()
	{
		InstantiateUfo();
	}
	public void AlienKilled()
	{
		aliensAmount--;
		if(aliensAmount <= 0) InstantiateAliens();
	}
	public void ChangeDirection()
	{
		aliensXMoveDirection *= -1;
		ChangeEnemiesData(newRowMultiplier);
		if (OnDirectionChanged != null) OnDirectionChanged.Invoke();
	}

	private void ChangeEnemiesData(float multiplier)
	{
		alienTimeForNextShoot *= multiplier;
		alienMoveTime *= multiplier;
	}

	private void SetGameDefaultData()
	{
		Camera.main.orthographicSize = GameData.ScreenWidth / 2;
		newRowMultiplier = GameData.NewRowMultiplier;
		aliensPerRow = GameData.AliensPerRow;
		spaceBetweenAliensX = GameData.SpaceBetweenAliensX;
		spaceBetweenAliensY = GameData.SpaceBetweenAliensY;
		ufoInstantiateTime = GameData.UfoInstantiateTime;
	}

	private void InstantiateUfo()
	{
		time += Time.deltaTime;
		if(time > ufoInstantiateTime)
		{
			time = 0;
			GameObject ufoObject = Instantiate(ufoPrefab);
			ufoObject.GetComponent<Ufo>().SetSpawnPoints(ufoLeftSpawnPoint, ufoRightSpawnPoint);
		}
	}
	private void InstantiateAliens()
	{
		SetEnemiesStartData();
		for (int i = 0; i < aliens.Length; i++)
		{
			float newYPosition = aliensSpawnPoint.position.y - i * spaceBetweenAliensY + aliens.Length * spaceBetweenAliensY / 2;
			for (int j = 0; j < aliensPerRow; j++)
			{
				Vector2 newPosition = new Vector2(aliensSpawnPoint.position.x + spaceBetweenAliensX * j - aliensPerRow * spaceBetweenAliensX / 2, newYPosition);
				Instantiate(aliens[i], newPosition, Quaternion.identity, aliensSpawnPoint);
				aliensAmount++;
			}
		}
	}

	private void SetEnemiesStartData()
	{
		alienSpeed = GameData.AlienSpeed;
		alienMoveTime = GameData.AlienMoveTime;
		alienBulletSpeed = GameData.AlienBulletSpeed;
		alienShootProbability = GameData.AlienShootProbability;
		alienTimeForNextShoot = GameData.AlienTimeForNextShoot;
		bullet.SetBulletSpeed(GameData.AlienBulletSpeed);
	}
}
