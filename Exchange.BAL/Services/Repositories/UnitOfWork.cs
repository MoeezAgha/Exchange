using Exchange.BAL.Services.Contracts;
using Exchange.DAL;
using Exchange.DAL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Exchange.BAL.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _context { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UnitOfWork(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
            Categories = new CategoryRepository(_context);
            ExchangeOffers = new ExchangeOfferRepository(_context);
            Images = new ImageRepository(_context);
            Products = new ProductRepository(_context);
            ProductImages = new ProductImageRepository(_context);
            Tags = new TagRepository(_context);
            NavMenu=  new NavMenuRepository(_context);
            _httpContextAccessor = httpContextAccessor;
       
        }

        public CategoryRepository Categories { get; set; }
        public ExchangeOfferRepository ExchangeOffers { get; set; }
        public ImageRepository Images { get; set; }
        public ProductRepository Products { get; set; }
        public ProductImageRepository ProductImages { get; set; }
        public TagRepository Tags { get; set; }

        public NavMenuRepository NavMenu { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            UpdateAuditFields();
            return await _context.SaveChangesAsync();
        }
        private void UpdateAuditFields()
        {
            var entries = _context.ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditable && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var audit = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    audit.CreatedBy =  audit.CreatedBy ??  GetCurrentUserId() ?? string.Empty;
                    audit.CreatedDate = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    audit.ModifiedBy = GetCurrentUserId() ?? string.Empty;
                    audit.ModifiedDate = DateTime.UtcNow;
                }
            }
        }
        private string GetCurrentUserId()
        {

            return _httpContextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value ?? "System";
        }
        public void Dispose()
        {
            _context.Dispose();
        }



    }

}
