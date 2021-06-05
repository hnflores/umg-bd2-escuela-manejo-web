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
        public string MethodGetDrivers { get; set; }
        public string MethodGetVehicles { get; set; }
        public string MethodGetVehicle { get; set; }
        public string MethodAddVehicle { get; set; }
        public string MethodUpdateVehicle { get; set; }
        public string MethodDeleteVehicle { get; set; }
        public string MethodGetCustomers { get; set; }
        public string MethodGetGetCustomerById { get; set; }
        public string MethodAddContract { get; set; }
        public string MethodGetContracts { get; set; }
        public string MethodDeleteContract { get; set; }
        public string MethodGetContract { get; set; }
        public string MethodUpdateContract { get; set; }        
    }

}
