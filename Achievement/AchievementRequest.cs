namespace AchievementAPI
{
    public class AchievementRequest
    {
        public string achievementName { get; }
        public AchievementType achievementType { get; }
        public long gamerScore { get; }
        public Guid UUID { get; }
        public AchievementRequest(string achievementName, AchievementType achievementType
            , long gamerScore, Guid UUID)
        {
            this.achievementName = achievementName;
            this.achievementType = achievementType;
            this.gamerScore = gamerScore;
            this.UUID = UUID;
        }
        public static AchievementRequest fromGetBody(AchievementGetBody body)
        {
            String achievementName = body.achievementName;
            AchievementType achievementType = (AchievementType)body.achievementType;
            long gamerScore = body.gamerScore;
            if(gamerScore == 0)
            {
                if(achievementType == AchievementType.XboxOneRare)
                {
                    gamerScore = GamerScoreGenerator.Instance.GetRareGamerScore;
                }
                else
                {
                    gamerScore = GamerScoreGenerator.Instance.GetGamerScore;
                }
            }
            Guid uuid = Guid.NewGuid();
            return new AchievementRequest(achievementName, achievementType, gamerScore, uuid);
        }
    }
}
