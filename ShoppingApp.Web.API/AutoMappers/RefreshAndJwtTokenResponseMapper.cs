using System;
using System.Collections.Generic;
using AutoMapper;
using ShoppingApp.CQRS.Models.ResponseModels;
using ShoppingApp.Utils.InternalModels;
using ShoppingApp.Web.API.Contracts.ResponseModels.V1;

namespace ShoppingApp.Web.API.AutoMappers
{
    public class RefreshAndJwtTokenResponseMapper : Profile
    {
        public RefreshAndJwtTokenResponseMapper()
        {
            CreateMap<ExternalLoginCommandsResponseModel, RefreshAndJwtTokenResponseModel>();

            CreateMap<RefreshTokenCommandResponseModel, RefreshAndJwtTokenResponseModel>();

            CreateMap<LoginAndRegisterCommandsResponseModel, RefreshAndJwtTokenResponseModel>();

            CreateMap<InternalErrorModel, ErrorListResponseModel>();
        }
    }
}
