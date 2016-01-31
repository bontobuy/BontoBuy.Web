﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    public interface IModelRepo
    {
        IEnumerable<ModelViewModel> Retrieve();
        ModelViewModel Get(int id);
        ModelViewModel Create(ModelViewModel item);
        ModelViewModel Update(int id, ModelViewModel item);
        void Remove(int id);

        //IEnumerable<ModelViewModel> GetModelsByBrand(int brand);
    }
}