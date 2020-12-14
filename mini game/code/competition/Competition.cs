using System;
using System.Collections.Generic;
using UnityEngine;

namespace esports
{
	public class Competition : IEquatable<Competition>
	{
		public int Id { get; private set; }
		public string NameField { get; private set; }
		private int rate;
		public int Rate
		{
			get { return rate; }
			set
			{
				rate = Math.Min(GameSettingConst.MAX_COMPETITION_RATE, Math.Max(1, value));
			}
		}
		private int difficulty;
		public int Difficulty
		{
			get { return difficulty; }
			set
			{
				difficulty = Math.Min(GameSettingConst.MAX_COMPETITION_DIFFICULTY, Math.Max(1, value));
			}
		}
		public EsportsGame Game { get; private set; }
		public List<int> prize;
		public CustomDateTime time;
		public List<Qualification> qualifications;
		public int round;

		public Competition(int id, string nameField, int rate,int difficulty, EsportsGame game, List<int> prize, List<Qualification> qualifications, CustomDateTime time, int round=0)
		{
			Id = id;
			NameField = nameField;
			Rate = rate;
			Difficulty = difficulty;
			Game = game;
			this.prize = prize;
			this.qualifications = qualifications;
			this.time = time;
			this.round = round;
		}


		public int GetPrize(int ranking)
		{
			if (prize == null)
			{
				return 0;
			}
			if (ranking > prize.Count || ranking <= 0)
			{
				return 0;
			}
			return prize[ranking - 1];
		}

		public int GetTeamFame(int ranking)
		{
			switch (ranking)
            {
				case 1:
					return Difficulty * 6;
				case 2:
					return Difficulty * 4;
				case 3:
					return Difficulty * 2;
				default:
					return Difficulty;
            }
		}

		/*public int GetPersonalFame(int ranking)
		{
			if (ranking > 8)
			{
				return 0;
			}
			return Convert.ToInt32(Math.Round(1f / ranking * 10 * (2 * Game.HitRate + Rate) * UnityEngine.Random.Range(0.75f, 1.25f)));
		}*/

		public bool FulfillQualifications(TeamController teamController)
		{
			if (qualifications == null)
			{
				return true;
			}
			foreach (Qualification quali in qualifications)
			{
				if (!QualificationFactory.FulfillQualification(quali,teamController))
				{
					return false;
				}

			}
			return true;
		}

		public static bool operator ==(Competition obj1, Competition obj2)
		{
			if (ReferenceEquals(obj1, obj2))
			{
				return true;
			}
			if (ReferenceEquals(obj1, null))
			{
				return false;
			}
			if (ReferenceEquals(obj2, null))
			{
				return false;
			}

			return obj1.Equals(obj2);
		}

		public static bool operator !=(Competition obj1, Competition obj2)
		{
			return !(obj1 == obj2);
		}

		public bool Equals(Competition other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return Id == other.Id;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Competition);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return Id.GetHashCode();
			}
		}

		//shallow copy game and qualification, as the requirement remain unchanged
		public Competition Clone()
        {
			return new Competition(Id, NameField, rate, Difficulty, Game, prize.Clone<int>(), qualifications, time.DeepClone(), round);
        }
	}
}