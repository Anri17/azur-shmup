namespace AzurProject.Data
{
    public class NormalEnemyData : BaseData
    {
        // basic
        public string Name { get; set; }
        public int Health { get; set; }
        public int Score { get; set; }
        public string Sprite { get; set; }
        
        // items
        public int PowerItemDrop { get; set; }
        public int BigPowerItemDrop { get; set; }
        public int ScoreItemDrop { get; set; }
        public int LifeItemDrop { get; set; }
    }
}
