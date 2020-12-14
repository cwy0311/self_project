namespace esports
{
	public abstract class Qualification {
		public float theshold;
		public abstract Qualification DeepClone();
	}

	public abstract class Qualification<K, V>: Qualification
	{
		public V auditTarget;
		public Qualification(float theshold, V auditTarget)
		{
			this.theshold = theshold;
			this.auditTarget = auditTarget;
		}
		public abstract bool Fulfill(K organization);
	}
}