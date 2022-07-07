public static class GameData
{
	//screen data - camera values
	public const float ScreenWidth = 10;
	//player data
	public static float PointsRecord = 0;
	public const float PlayerSpeed = 4;
	public const float PlayerBulletSpeed = 8;
	public const float PlayerTimeForNextShoot = 0.8f;
	//alien data
	public const float AlienSpeed = 1;
	public const float AlienBulletSpeed = -3;
	public const float AlienMoveTime = 2;
	public const float AlienTimeForNextShoot = 4;
	public const int AlienShootProbability = 1;
	public const int AliensPerRow = 11;
	public const float SpaceBetweenAliensX = 1;
	public const float SpaceBetweenAliensY = 0.5f;
	public const float NewRowMultiplier = 0.9f;
	//ufo data
	public const float UfoSpeed = 3.4f;
	public const float UfoInstantiateTime = 6f;
}
