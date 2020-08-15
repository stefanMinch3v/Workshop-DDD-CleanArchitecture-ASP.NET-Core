namespace CarRentalSystem.Web.Features
{
    using Application;
    using Application.Features.CarAds.Commands.Create;
    using Application.Features.CarAds.Queries.Search;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;

    public class CarAdsController : ApiController
    {
        private readonly IOptions<ApplicationSettings> settings;

        public CarAdsController(IOptions<ApplicationSettings> settings)
            => this.settings = settings;

        [HttpGet]
        [Route(nameof(GetSettings))]
        public object GetSettings() => this.settings;

        [HttpGet]
        public async Task<ActionResult<SearchCarAdsOutputModel>> Search([FromQuery] SearchCarAdsQuery query)
            => await this.Send(query);

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateCarAdOutputModel>> Create(CreateCarAdCommand command)
            => await this.Send(command);
    }
}
