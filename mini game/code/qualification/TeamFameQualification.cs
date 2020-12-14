namespace esports
{
	public class TeamFameQualification<TeamController, EsportsGame> : Qualification<esports.TeamController, esports.EsportsGame>
	{
		public TeamFameQualification(float theshold, esports.EsportsGame auditTarget) : base(theshold, auditTarget)
		{ }
		public override bool Fulfill(esports.TeamController organization)
		{
			//if (organization.esportsGameFame == null || !organization.esportsGameFame.ContainsKey(auditTarget))
			//{
			//	return false;
			//}
			return organization.GetEsportsGameFame(auditTarget) >= theshold;
		}

		public override Qualification DeepClone()
		{
			return new TeamFameQualification<TeamController, EsportsGame>(theshold, auditTarget);

		}
	}
}