using ShopASP.Application.DTO;
using ShopASP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.Interface
{
    public interface IProductService : GenericInterface<ProductDTO>
    {
        Task Update(ProductDTO productDTO);
    }
}
