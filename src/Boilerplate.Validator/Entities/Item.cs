namespace Boilerplate.Models
{
    #region << Using >>

    using AutoMapper;
    using Boilerplate.Domain;

    #endregion

    public class Item : EntityBase
    {
        #region Properties

        public string Value { get; set; }

        public Status Status { get; set; }

        #endregion

        #region Constructors

        public Item()
        {
            Status = Status.New;
        }

        #endregion

        #region Nested Classes

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