namespace FusionTechBoilerplate.Models
{
    public class UserParameters
    {
        public string Id { get; set; }

        public FindItemByUserIdSpec<Item> SpecByUserId { get; set; }

        public FindUserByIdSpec SpecUserId { get; set; }
    }
}
