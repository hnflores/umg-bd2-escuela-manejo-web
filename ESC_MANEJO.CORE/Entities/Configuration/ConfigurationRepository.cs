namespace ESC_MANEJO.CORE.Entities.Configuration
{
    public class ConfigurationRepository
    {
        public AdminRepositoryModel Admin { get; set; }        
    }

    public class AdminRepositoryModel
    {
        public string Url { get; set; }
        public string KeyAuth { get; set; }
        public string MethodLogin { get; set; }
       
    }
   
}
