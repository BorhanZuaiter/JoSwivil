using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.AuctionService
{
    public class AuctionService : IAuctionService
    {
        private readonly VODContext _db;

        public AuctionService(VODContext db)
        {
            _db = db;
        }
        public List<ItemDto> GetHomeAuctions()
        {
            var List = _db.Items
                .Where(r => r.IsDeleted == false)
                .OrderBy(x => Guid.NewGuid())
                .Select(a => new ItemDto
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
                })
                .Take(10)
                .ToList();
            return List;

        }

        public List<ItemDto> GetAuctions()
        {
            var List = _db.Items.OrderByDescending(x => x.Id)
                .Where(r => r.IsDeleted == false).Select(a => new ItemDto
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
                }).ToList();

            return List;
        }
        public ItemDto GetById(int id)
        {
            var res = _db.Items.Where(r => r.IsDeleted == false && r.Id == id).Select(a => new ItemDto
            {
                Id = a.Id,
                Name = a.Name,
                Sellername = a.Seller.Name,
                Sellerimage = a.Seller.ProfileImageUrl,
                PrivewImageUrl = a.PrivewImageUrl,
                DetailsImageUrl = a.DetailsImageUrl,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                CurrentBiddingPrice = a.CurrentBiddingPrice,
                SKU = a.SKU,
                Description = a.Description,
                Brand = a.Brand,
                Tags = a.Tags,
                Dimensions = a.Dimensions,
                Weight = a.Weight,
                SellerId = a.SellerId,
                CategoryId = a.CategoryId,
                Categoryname = a.Category.Name,


            }).FirstOrDefault();
            return res;
        }
        public bool UpdateBid(int itemId, double newBid)
        {
            var item = _db.Items.FirstOrDefault(i => i.Id == itemId && !i.IsDeleted);
            if (item == null || newBid <= item.CurrentBiddingPrice)
            {
                return false; // Bid should be higher than the current bid
            }

            item.CurrentBiddingPrice = newBid;
            _db.SaveChanges();
            return true;
        }

    }
}
