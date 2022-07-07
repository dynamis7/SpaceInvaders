using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : ObjectWithLifes
{
    public static Player player;
    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab = default;
    [SerializeField] private Transform bulletSpawnPoint = default;
    [Header("Points")]
    [ConditionalField("showLifes"), SerializeField] private Text pointsText = default;
    private float horizontalSpeed = default;
    private float xLimit = default;
    private Vector2 targetPosition = default;
    private float timeSinceLastShoot = default;
    private float timeLimit = default;
    private float bulletSpeed = default;
    private GameObject newBullet = default;
    private float playerPoints = 0;

	protected override void Awake()
	{
		base.Awake();
        player = this;
    }
      
	private void Start()
    {
        GetData();
        pointsText.text = playerPoints.ToString();
    }

    private void Update()
    {
        PlayerMovement();
        PlayerShooting();
    }
    public void AddPoints(float gainedPoints)
    {
        playerPoints += gainedPoints;
        pointsText.text = playerPoints.ToString();
    }

    public void GameOver()
	{
        if (playerPoints > GameData.PointsRecord) GameData.PointsRecord = playerPoints;
        SceneInstaller.LoadScene(0);
    }
    private void GetData()
    {
        targetPosition = transform.position;
        horizontalSpeed = GameData.PlayerSpeed;
        bulletSpeed = GameData.PlayerBulletSpeed;
        xLimit = GameData.ScreenWidth - 1.5f;
        timeLimit = GameData.PlayerTimeForNextShoot;
    }
    private void PlayerShooting()
	{
        timeSinceLastShoot += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space) && timeSinceLastShoot > timeLimit)
		{
            newBullet = Instantiate(bulletPrefab, bulletSpawnPoint);
            newBullet.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
            timeSinceLastShoot = 0;
        }
	}
    private void PlayerMovement()
	{
        targetPosition.x += Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -xLimit, xLimit);
        transform.position = targetPosition;
    }
}
