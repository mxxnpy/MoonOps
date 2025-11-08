using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace MoonOps.Api.Controllers;

[ApiExplorerSettings(GroupName = "Root")]
[Route("api")]
public class ApiController : BaseController
{
    private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionProvider;

    public ApiController(IApiDescriptionGroupCollectionProvider apiDescriptionProvider) => 
        _apiDescriptionProvider = apiDescriptionProvider;

    [HttpGet("")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
 public IActionResult GetApiInfo()
    {
        var groups = _apiDescriptionProvider.ApiDescriptionGroups.Items
     .Where(group => !string.IsNullOrEmpty(group.GroupName))
     .GroupBy(group => group.GroupName)
    .ToDictionary(
     g => g.Key,
        g => g.SelectMany(group => group.Items)
         .Select(api => new
  {
        Method = api.HttpMethod,
          Path = api.RelativePath
      }).ToList()
       );

     return Ok(new
        {
   ApiName = "MoonOps API",
     Version = "1.0.0",
      Timestamp = DateTime.UtcNow,
       Author = "mxxnpy",
            Contact = "contato@moonops.dev",
        AvailableVersions = groups.Keys.ToList(),
            Endpoints = groups
        });
    }
}