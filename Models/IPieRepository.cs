﻿using System.Collections.Generic;

namespace PieShop.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> Pies { get; }
        IEnumerable<Pie> PiesOfTheWeek { get; }

        Pie GetPieById(int pieId);

        void CreatePie(Pie pie);

        void UpdatePie(Pie pie);

        void DeletePie(int pieId);
    }
}