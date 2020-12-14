using System.Collections.Generic;

namespace esports
{
	public class TeamCompetitionQualification<TeamController> : Qualification<esports.TeamController, int>
	{
		public TeamCompetitionQualification(float theshold, int auditTarget) : base(theshold,auditTarget)
		{}
		public override bool Fulfill(esports.TeamController organization)
		{
			List<Achievement> competitionAchievement = organization.GetAllAchievement();
			foreach (Achievement ach in competitionAchievement)
            {
				if (ach is SingleCompetitionAchievement)
                {
					SingleCompetitionAchievement sca = (SingleCompetitionAchievement)ach;
					if (sca.competitionId== auditTarget)
                    {
						//theshold=ranking, less value implies higher ranking 
						if (sca.GetCurrentRank() <= theshold && sca.GetCurrentRank()>0)
                        {
							return true;
                        }
						else
                        {
							return false;
                        }
                    }

				}
            }
			//no target found
			return false;
		}

        public override Qualification DeepClone()
        {
			return new TeamCompetitionQualification<TeamController>(theshold, auditTarget);

		}
    }
}