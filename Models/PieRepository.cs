﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace PieShop.Models
{
    public class PieRepository: IPieRepository
    {
        private readonly AppDbContext _appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pie> Pies
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.Include(p => p.PieReviews).FirstOrDefault(p => p.PieId == pieId);
        }

        public void UpdatePie(Pie pie)
        {
            _appDbContext.Pies.Update(pie);
            _appDbContext.SaveChanges();
        }

        public void CreatePie(Pie pie)
        {

           pie.PieId= _appDbContext.Pies.Last().PieId +1;
            _appDbContext.Pies.Add(pie);
            _appDbContext.SaveChanges();
        }

        public void DeletePie(int pieId)
        {
            //Pie p = _appDbContext.Pies.FirstOrDefaultAsync(p => p.PieId == pieId).Result;
              _appDbContext.Pies.Remove(_appDbContext.Pies.FirstOrDefaultAsync(p => p.PieId == pieId).Result);
            _appDbContext.SaveChangesAsync();

        }
    }
}
