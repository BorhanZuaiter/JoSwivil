using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.SellerService
{
    public class SellerService : ISellerService
    {
        private readonly VODContext _db;

        public SellerService(VODContext db)
        {
            _db = db;
        }
        public List<SellerDto> GetCategory()
        {
            var List = _db.Sellers.OrderByDescending(x => x.Id)
                .Where(r => r.IsDeleted == false).Select(a => new SellerDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    PhoneNumber = a.PhoneNumber,
                    Address = a.Address,
                    CoverImageUrl = a.CoverImageUrl,
                    ProfileImageUrl = a.ProfileImageUrl,
                }).ToList();

            return List;
        }
        public SellerDto GetById(int id)
        {
            var res = _db.Sellers.Where(r => r.IsDeleted == false && r.Id == id).Select(a => new SellerDto
            {
                Id = a.Id,
                Name = a.Name,
                PhoneNumber = a.PhoneNumber,
                Address = a.Address,
                CoverImageUrl = a.CoverImageUrl,
                ProfileImageUrl = a.ProfileImageUrl,
                IdImageUrl = a.IdImageUrl,
                Bio = a.Bio,
                Email = a.Email,
                Items = a.Items.Select(s => new ItemDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Sellername = s.Seller.Name,
                    Sellerimage = s.Seller.ProfileImageUrl,
                    PrivewImageUrl = s.PrivewImageUrl,
                    CurrentBiddingPrice = s.CurrentBiddingPrice,
                    SKU = s.SKU,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                }).ToList()

            }).FirstOrDefault();
            return res;
        }
        public List<ItemDto> GetAuctions(int SellerId)
        {
            var List = _db.Items.OrderByDescending(x => x.Id)
                .Where(r => r.IsDeleted == false && r.SellerId == SellerId).Select(a => new ItemDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Sellername = a.Seller.Name,
                    Sellerimage = a.Seller.ProfileImageUrl,
                    PrivewImageUrl = a.PrivewImageUrl,
                    CurrentBiddingPrice = a.CurrentBiddingPrice,
                    SKU = a.SKU,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    CategoryId = a.CategoryId,
                }).ToList();
            return List;
        }
    }
}
