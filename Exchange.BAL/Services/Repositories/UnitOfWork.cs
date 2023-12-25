using Exchange.BAL.Services.Contracts;
using Exchange.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.BAL.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _context { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
            Categories = new CategoryRepository(_context);
            ExchangeOffers = new ExchangeOfferRepository(_context);
            Images = new ImageRepository(_context);
            Products = new ProductRepository(_context);
            ProductImages = new ProductImageRepository(_context);
            Tags = new TagRepository(_context);
        }

        public CategoryRepository Categories { get; set; }
        public ExchangeOfferRepository ExchangeOffers { get; set; }
        public ImageRepository Images { get; set; }
        public ProductRepository Products { get; set; }
        public ProductImageRepository ProductImages { get; set; }
        public TagRepository Tags { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    
        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
