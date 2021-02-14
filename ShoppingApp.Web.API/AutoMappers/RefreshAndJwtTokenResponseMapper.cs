using AutoMapper;
using ShoppingApp.CQRS.Models.ResponseModels.IdentityResponseModels;
using ShoppingApp.Utils.InternalModels;
using ShoppingApp.Web.API.Contracts.ResponseModels.V1;

namespace ShoppingApp.Web.API.AutoMappers
{
    public class CQRSResponseToAPIResponseMapperProfile : Profile
    {
        public CQRSResponseToAPIResponseMapperProfile()
        {
            CreateMap<ExternalLoginCommandsResponseModel, RefreshAndJwtTokenResponseModel>();

            CreateMap<RefreshTokenCommandResponseModel, RefreshAndJwtTokenResponseModel>();

            CreateMap<LoginAndRegisterCommandsResponseModel, RefreshAndJwtTokenResponseModel>();

            CreateMap<InternalErrorModel, ErrorListResponseModel>();
        }
    }
}
