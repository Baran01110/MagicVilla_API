using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto> {
                new VillaDto{ID =1, Name = "Pool View", Sqft=100, Occupancy=4},
                new VillaDto{ID =2, Name = "Beach View", Sqft=300, Occupancy=3}
            };
    }
}
