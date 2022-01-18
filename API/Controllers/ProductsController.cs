using API.Core.Entities;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {

        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductType> typeRepo, IGenericRepository<ProductBrand> brandRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _productTypeRepo = typeRepo;
            _productBrandRepo = brandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("products")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string lang = "en")
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var productList = await _productRepo.ListAsync(spec);
            MappingProfiles.Lang = lang;
            var products = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(productList);

            return Ok(products);
        }
        [HttpGet]
        [Route("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _productBrandRepo.ListAllAsync();
            return Ok(productBrands);
        }
        [HttpGet]
        [Route("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productTypeRepo.ListAllAsync();
            return Ok(productTypes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id, string lang = "en")
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var productEntity = await _productRepo.GetEntityWithSpec(spec);
            MappingProfiles.Lang = lang;

            if (productEntity == null) return NotFound(new ApiResponse(404));

            var product = _mapper.Map<Product, ProductToReturnDto>(productEntity);

            return Ok(product);
        }
    }
}