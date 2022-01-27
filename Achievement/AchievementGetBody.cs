namespace AchievementAPI
{
    public class AchievementGetBody
    {
        public string achievementName { get; set; }
        public int achievementType { get; set; }
        public long gamerScore { get; set; }
        public bool valid
        {
            get
            {
                return ((achievementName != null && achievementName.Length > 0)
                    && (achievementType > 0)
                    && gamerScore >= 0);
            }
        }
    }
}
