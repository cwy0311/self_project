using System;

namespace esports
{
    public class QualificationFactory
    {
        public static Qualification GetQualification(float theshold, int auditTarget)
        {
            return new TeamCompetitionQualification<TeamController>(theshold, auditTarget);
        }

        public static Qualification GetQualification(float theshold, EsportsGame auditTarget)
        {
            return new TeamFameQualification<TeamController, EsportsGame>(theshold, auditTarget);
        }

        //for competition requirement
        public static bool FulfillQualification(Qualification quali, TeamController teamController)
        {
            if (quali == null)
            {
                return true;
            }
            if (quali is TeamFameQualification<TeamController, EsportsGame> && (quali as TeamFameQualification<TeamController, EsportsGame> != null))
            {
                return (quali as TeamFameQualification<TeamController, EsportsGame>).Fulfill(teamController);
            }
            else if (quali is TeamCompetitionQualification<TeamController> && quali is TeamCompetitionQualification<TeamController>)
            {
                return (quali as TeamCompetitionQualification<TeamController>).Fulfill(teamController);
            }
            return false;
        }
        public static bool FulfillQualification(Qualification quali, Player player)
        {
            if (quali == null || player==null)
            {
                return true;
            }
            
            return true;
        }

    }
}