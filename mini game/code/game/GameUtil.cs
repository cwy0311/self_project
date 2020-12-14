public static class GameUtil
{
	public static string Money2String(int amount)
	{
		if (amount < 100000)
		{
			return (amount).ToString();
		}
		else if (amount < 10000000)
		{
			return (amount / 1000).ToString() + "K";
		}
		else
		{
			return (amount / 1000000).ToString() + "M";
		}


	}
}
