using System;
using UnityEngine;

namespace esports
{
	public class SimpleBattleStatus
	{
		private int maxHealth;
		public int MaxHealth
		{
			get { return maxHealth; }
			private set { maxHealth = Math.Max(0, value); }
		}
		private int currentHealth;
		public int CurrentHealth
		{
			get { return currentHealth; }
			private set { currentHealth = Math.Min(MaxHealth,Math.Max(0, value)); }
		}
		private int maxStrength;
		public int MaxStrength
		{
			get { return maxStrength; }
			private set { maxStrength = Math.Max(0, value); }
		}
		private int maxdefense;
		public int MaxDefense
		{
			get { return maxdefense; }
			private set { maxdefense = Math.Max(0, value); }
		}

		private int currentStrength;
		public int CurrentStrength
		{
			get { return currentStrength; }
			set { currentStrength = Math.Min(MaxStrength, Math.Max(0, value)); }
		}

		private int currentDefense;
		public int CurrentDefense
		{
			get { return currentDefense; }
			set { currentDefense = Math.Min(MaxDefense, Math.Max(0, value)); }
		}

		public SimpleBattleStatus(int health, int strength, int defense)
		{
			MaxHealth = health;
			MaxStrength = strength;
			MaxDefense = defense;
			MaximizeStatus();
		}

		public void MaximizeStatus()
        {
			CurrentHealth = MaxHealth;
			CurrentStrength = MaxStrength;
			CurrentDefense = MaxDefense;
        }

		public bool IsDead()
		{
			return CurrentHealth <= 0;
		}

		public void Attack(SimpleBattleStatus sufferSide, bool isAttack)
        {
			sufferSide.currentHealth -= CountDmg(sufferSide, isAttack);

		}

		private int CountDmg(SimpleBattleStatus sufferSide, bool isAttack)
		{
			if (isAttack)
			{
				return Math.Max(currentStrength - sufferSide.currentDefense, Mathf.RoundToInt(currentStrength * (1 - sufferSide.currentDefense / (100f + sufferSide.currentDefense))));
			}
			else
			{
				return Math.Max(1, (Math.Max(1, currentStrength - sufferSide.currentDefense) + Mathf.RoundToInt(currentStrength * (1 - sufferSide.currentDefense / (100f + sufferSide.currentDefense)))) / 2);
			}
		}

		public SimpleBattleStatus DeepClone()
        {
			return new SimpleBattleStatus(MaxHealth, MaxStrength, MaxDefense);
        }
	}
}
