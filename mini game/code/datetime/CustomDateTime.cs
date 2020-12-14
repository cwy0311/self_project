using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace esports
{

	[Serializable]
	public class CustomDateTime : IEquatable<CustomDateTime>, IComparable
	{
		private int year;
		public int Year
		{
			get { return year; }
			set { year = value; }
		}

		private int month;
		public int Month
		{
			get { return month; }
			private set
			{
				if (value < 0)
				{
					month = (12 + value % 12) % 12;
				}
				else
				{
					month = value % 12;
				}
			}
		}

		private int week;
		public int Week
		{
			get { return week; }
			private set
			{
				if (value < 0)
				{
					week = (4 + value % 4) % 4;
				}
				else
				{
					week = value % 4;
				}
			}
		}

		private int weekday;
		public int Weekday
		{
			get { return weekday; }
			private set
			{
				if (value < 0)
				{
					weekday = (7 + value % 7) % 7;
				}
				else
				{
					weekday = value % 7;
				}
			}
		}

		public CustomDateTime(int year, int month, int week, int weekday)
		{
			Year = year;
			Month = month;
			Week = week;
			Weekday = weekday;
		}


		public string GetWeekdayString()
		{
			switch (weekday)
			{
				case (0):
					return "Sun";
				case (1):
					return "Mon";
				case (2):
					return "Tue";
				case (3):
					return "Wed";
				case (4):
					return "Thu";
				case (5):
					return "Fri";
				case (6):
				default:
					return "Sat";
			}
		}

		public void AddYear(int year)
		{
			Year += year;
		}

		public void AddMonth(int monthCount)
		{
			if (monthCount < 0)
			{
				AddYear((monthCount - 11 + Month) / 12);
			}
			else
			{
				AddYear((monthCount + Month) / 12);
			}
			Month += monthCount;
		}

		public void AddWeek(int weekCount)
		{
			if (weekCount < 0)
			{
				AddMonth((weekCount - 3 + Week) / 4);
			}
			else
			{
				AddMonth((weekCount + Week) / 4);
			}
			Week += weekCount;
		}

		public void AddWeekday(int weekdayCount)
		{
			if (weekdayCount < 0)
			{
				AddWeek((weekdayCount - 6 + Weekday) / 7);
			}
			else
			{
				AddWeek((weekdayCount + Weekday) / 7);
			}
			Weekday += weekdayCount;
		}

		public int TotalDaysWithoutYear()
		{
			return Month * 28 + Week * 7 + Weekday;
		}

		public int TotalDays()
		{
			return Year * 336 + Month * 28 + Week * 7 + Weekday;
		}


		/// <summary>
		/// return how many days will reach the specific month and day.
		/// eg: new CustomDateTime(3,11,3,4).CountTotalDays2OtherDateTime(new CustomDateTime(1,0,0,0)) return 3,
		///	since 3 days later, (3,11,3,4) becomes (4,0,0,0), which is same day,week and month with another
		/// </summary>
		public int CountTotalDays2OtherDateTime(CustomDateTime otherDateTime)
		{
			if (otherDateTime == null)
			{
				return -1;
			}
			int currentTotalDays = TotalDaysWithoutYear();
			int targetTotalDays = otherDateTime.TotalDaysWithoutYear();
			if (currentTotalDays <= targetTotalDays)
			{
				return targetTotalDays - currentTotalDays;
			}
			else
			{ //currentTotalDays>targetTotalDays , across next year
				return targetTotalDays - currentTotalDays + 336;
			}

		}





		public static bool operator ==(CustomDateTime obj1, CustomDateTime obj2)
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

		public static bool operator !=(CustomDateTime obj1, CustomDateTime obj2)
		{
			if (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null))
			{
				return false;
			}
			return !(obj1 == obj2);
		}

		public static bool operator >(CustomDateTime obj1, CustomDateTime obj2)
		{
			if (ReferenceEquals(obj1, null))
			{
				return false;
			}
			if (ReferenceEquals(obj2, null))
			{
				return true;
			}
			return obj1.TotalDays() > obj2.TotalDays();
		}

		public static bool operator <(CustomDateTime obj1, CustomDateTime obj2)
		{
			return !(obj1 >= obj2);
		}

		public static bool operator >=(CustomDateTime obj1, CustomDateTime obj2)
		{
			return (obj1 > obj2 || obj1 == obj2);
		}

		public static bool operator <=(CustomDateTime obj1, CustomDateTime obj2)
		{
			return !(obj1 > obj2);
		}


		public bool Equals(CustomDateTime other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return TotalDays() == other.TotalDays();
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as CustomDateTime);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return TotalDays().GetHashCode();
			}
		}
		public int CompareTo(object obj)
		{
			if (obj == null) return 1;
			CustomDateTime other = obj as CustomDateTime;
			if (ReferenceEquals(other, null))
				throw new ArgumentException("Object is not a CustomDateTime");
			else
				return TotalDays().CompareTo(other.TotalDays());
		}
		public CustomDateTime DeepClone()
		{
			return new CustomDateTime(year, Month, week, weekday);
		}
	}
}