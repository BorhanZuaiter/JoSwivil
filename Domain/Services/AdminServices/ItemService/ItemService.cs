using Domain.DTO.AdminDtos;
using Domain.Entities;
using Domain.Helpers;

namespace Domain.Services.AdminServices.ItemService
{
    public class ItemService : IItemService
    {
        private readonly VODContext _db;
        private readonly StorageConnection _storageConnection;

        public ItemService(VODContext db, StorageConnection storageConnection)
        {
            _db = db;
            _storageConnection = storageConnection;
        }
        public QueryResult<ItemDto> GetList(SearchCriteria model)
        {
            var res = _db.Items
                .WhereIf(!string.IsNullOrEmpty(model.Search), r => r.Name.Contains(model.Search))
                .Where(r => r.IsDeleted == false)
                .OrderByDescending(r => r.Id)
                .Select(r => new ItemDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Price = r.Price,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                }).ToQueryResult(model.PageNumber, model.PageSize);
            return res;
        }
        public async Task<bool> Create(ItemDto input, string userId)
        {
            var obj = new Item();
            obj.Name = input.Name;
            obj.Description = input.Description;
            obj.Brand = input.Brand;
            obj.Price = input.Price;
            obj.SKU = input.SKU;
            obj.Tags = input.Tags;
            obj.Weight = input.Weight;
            obj.Dimensions = input.Dimensions;
            obj.NumberOfBids = input.NumberOfBids;
            obj.StartingPrice = input.StartingPrice;
            obj.CurrentBiddingPrice = input.CurrentBiddingPrice;
            obj.CategoryId = input.CategoryId;
            obj.SellerId = input.SellerId;
            obj.StartDate = input.StartDate;
            obj.EndDate = input.EndDate;

            if (input.PrivewImage is not null)
                obj.PrivewImageUrl = await _storageConnection.SaveOnStorage(input.PrivewImage, "Items");

            if (input.DetailsImage is not null)
                obj.DetailsImageUrl = await _storageConnection.SaveOnStorage(input.DetailsImage, "Items");


            obj.CreatedBy = userId;
            obj.CreatedOn = DateTime.Now;
            _db.Items.Add(obj);
            return _db.SaveChanges() > 0;
        }
        public ItemDto GetById(int Id)
        {
            var obj = _db.Items.Where(r => r.Id == Id).Select(a => new ItemDto
            {
                Id = a.Id,
                Name = a.Name,
                NumberOfBids = a.NumberOfBids,
                Price = a.Price,
                StartingPrice = a.StartingPrice,
                CurrentBiddingPrice = a.CurrentBiddingPrice,
                Description = a.Description,
                SKU = a.SKU,
                Tags = a.Tags,
                Weight = a.Weight,
                Dimensions = a.Dimensions,
                Brand = a.Brand,
                CategoryId = a.CategoryId,
                SellerId = a.SellerId,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                DetailsImageUrl = a.DetailsImageUrl,
                PrivewImageUrl = a.PrivewImageUrl,

            }).FirstOrDefault();
            return obj;
        }
        public async Task<bool> Edit(ItemDto input, string userId)
        {
            var obj = _db.Items.Where(r => r.Id == input.Id).FirstOrDefault();
            obj.Name = input.Name;
            obj.NumberOfBids = input.NumberOfBids;
            obj.Price = input.Price;
            obj.StartingPrice = input.StartingPrice;
            obj.CurrentBiddingPrice = input.CurrentBiddingPrice;
            obj.Description = input.Description;
            obj.SKU = input.SKU;
            obj.Tags = input.Tags;
            obj.Weight = input.Weight;
            obj.Dimensions = input.Dimensions;
            obj.Brand = input.Brand;
            obj.CategoryId = input.CategoryId;
            obj.SellerId = input.SellerId;
            obj.StartDate = input.StartDate;
            obj.EndDate = input.EndDate;

            if (input.PrivewImage is not null)
                obj.PrivewImageUrl = await _storageConnection.SaveOnStorage(input.PrivewImage, "Items");

            if (input.DetailsImage is not null)
                obj.DetailsImageUrl = await _storageConnection.SaveOnStorage(input.DetailsImage, "Items");

            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Items.Update(obj);
            return _db.SaveChanges() > 0;
        }
        public bool Delete(int Id, string userId)
        {
            var obj = _db.Items.Where(r => r.Id == Id).FirstOrDefault();
            obj.IsDeleted = true;
            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Items.Update(obj);
            return _db.SaveChanges() > 0;
        }
    }
}
