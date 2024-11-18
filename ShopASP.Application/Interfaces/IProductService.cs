using ShopASP.Application.DTO;
using ShopASP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.Interfaces
{
    public interface IProductService : GenericInterface<ProductDTO>
    {
        Task UpdateProduct(ProductDTO productDTO);
    }
}
