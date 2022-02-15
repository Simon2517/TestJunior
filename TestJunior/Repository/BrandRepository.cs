﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace TestJunior.Repository
{

    public class BrandRepository : IRepository<Brand>
    {
        private DatabaseContext _ctx;

        public BrandRepository(DatabaseContext ctx)
        {
            _ctx = ctx;

        }

        public async Task<int> deleteAsync(int id)
        {
            int result = 0;
            IDbContextTransaction transaction = _ctx.Database.BeginTransaction();
            try
            {
                var brand = _ctx.Brand.FirstOrDefault(x => x.Id == id);
                brand.isDeleted = true;

                await _ctx.InfoRequestReply
                        .Where(reply => reply.InfoRequest.Product.BrandId == id)
                            .UpdateFromQueryAsync(x => new InfoRequestReply { isDeleted = true });

                await _ctx.InfoRequest
                        .Where(info => info.Product.ProductId == id)
                        .UpdateFromQueryAsync(x => new InfoRequest { isDeleted = true });
                
                await _ctx.ProductCategories
                        .Where(pc => pc.Product.BrandId == id)
                        .UpdateFromQueryAsync(x => new ProductCategories { isDeleted = true });

                await _ctx.Product
                        .Where(p => p.BrandId == id)
                        .UpdateFromQueryAsync(x =>new Product { isDeleted=true});

                result = brand.Id;
                _ctx.Update(brand);
                _ctx.SaveChanges();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return result;
        }

        public IQueryable<Brand> GetAll()
        {
            return _ctx.Brand.AsQueryable();

        }

        public IQueryable<Brand> GetById(int id)
        {
            return _ctx.Brand.Where(b => b.Id == id);
        }
        public int add(Brand brand)
        {
            IDbContextTransaction transaction = _ctx.Database.BeginTransaction();
            try
            {
                _ctx.Brand.Add(brand);
                _ctx.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
                return brand.Id;
            

        }

        public int update(Brand brand)
        {
            IDbContextTransaction transaction = _ctx.Database.BeginTransaction();
            try
            {
                _ctx.Brand.Update(brand);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return brand.Id;
        }
    }
}
