using API.Core.Entities;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
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
        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFilterForCountSpecification(productParams);

            var totalItems = await _productRepo.CountAsync(countSpec);

            var productList = await _productRepo.ListAsync(spec);
            MappingProfiles.Lang = productParams.Lang;
            var products = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(productList);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, products));
        }

        [Cached(600)]
        [HttpGet]
        [Route("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrandToReturnDto>>> GetProductBrands(string lang = "en")
        {
            var productBrandsList = await _productBrandRepo.ListAllAsync();
            MappingProfiles.Lang = lang;
            var productBrands = _mapper.Map<IReadOnlyList<ProductBrand>, IReadOnlyList<ProductBrandToReturnDto>>(productBrandsList);

            return Ok(productBrands);
        }

        [Cached(600)]
        [HttpGet]
        [Route("types")]
        public async Task<ActionResult<IReadOnlyList<ProductTypeToReturnDto>>> GetProductTypes(string lang = "en")
        {
            var productTypesList = await _productTypeRepo.ListAllAsync();
            MappingProfiles.Lang = lang;
            var productBrands = _mapper.Map<IReadOnlyList<ProductType>, IReadOnlyList<ProductTypeToReturnDto>>(productTypesList);
            return Ok(productBrands);
        }

        [Cached(600)]

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