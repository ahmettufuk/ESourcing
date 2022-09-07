namespace ESourcing_Sourcing.Settings.DatabaseSettings
{
    public interface ISourcingDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
