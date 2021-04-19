
namespace FusionTechBoilerplate.Models
{
    #region << Using >>

    using AutoMapper;
    using FusionTechBoilerplate.Domain;
    using JetBrains.Annotations;

    #endregion

    public class Item : EntityBase,
        IUserProperty
    {
        #region Properties

        public new int Id { get; set; }

        public string Value { get; set; }

        public Status Status { get; set; }

        public User User { get; set; }        

        #endregion

        #region Constructors

        public Item()
        {
            Status = Status.New;
        }

        #endregion

        #region Nested Classes

        [UsedImplicitly]
        public class ItemProfile : Profile
        {
            #region Constructors

            public ItemProfile()
            {
                CreateMap<Item, ItemDto>().ReverseMap();
            }

            #endregion
        }

        #endregion
    }
}