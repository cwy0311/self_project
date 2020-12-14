using System;

namespace esports
{
    public class EsportsGame : IEquatable<EsportsGame>
	{
        public int Id { get; private set; }
        public string NameField { get; }
		private int hitRate;
		public int HitRate
		{
			get { return hitRate; }
			set
			{
				hitRate = Math.Min(GameSettingConst.MAX_GAME_HIT_RATE, Math.Max(1, value));
			}
		}

		public int playerCount { get; private set; }

		public string iconString { get; private set; }




        public EsportsGame(int id, string nameField, int hitRate, int playerCount, string iconString)
        {
            Id = id;
            NameField = nameField;
            HitRate = hitRate;
			this.playerCount = playerCount;
			this.iconString = iconString;
        }


		public static bool operator ==(EsportsGame obj1, EsportsGame obj2)
		{
			if (ReferenceEquals(obj1, obj2))
			{
				return true;
			}
			if (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null))
			{
				return false;
			}
			return obj1.Equals(obj2);
		}

		public static bool operator !=(EsportsGame obj1, EsportsGame obj2)
		{
			return !(obj1 == obj2);
		}

		public bool Equals(EsportsGame other)
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
			return Equals(obj as EsportsGame);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return Id.GetHashCode();
			}
		}
	}
}
