using AutoMapper;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using StoreMVC.Client.Models;
using StoreMVC.Client.Models.Response;
using StoreMVC.Client.Services;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace StoreMVC.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserHttpClient _userHttpClient;
        private readonly IMapper _mapper;

        public HomeController(IUserHttpClient userHttpClient, IMapper mapper)
        {
           _userHttpClient = userHttpClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = await _userHttpClient.GetClient();

            var response = await httpClient.GetAsync("api/Users/GetUserList").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var usersString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var userResponseList = JsonConvert.DeserializeObject<UserResponseList>(usersString);

                List<UserResponse> urlist = userResponseList.RespondList;

                List<UserViewModel> userViewModelList = _mapper.Map<List<UserViewModel>>(urlist);

                return View(userViewModelList);
            }
            else if(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            throw new Exception($"Problem with fetching data from the API: {response.ReasonPhrase}");
        }
        [Authorize(Policy = "CanCreateAndModifyData")]
        public async Task<IActionResult> Privacy()
        {
            var client = new HttpClient();
            var metaDataResponse = await client.GetDiscoveryDocumentAsync("https://localhost:7288");

            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var response = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = metaDataResponse.UserInfoEndpoint,
                Token = accessToken
            });

            if (response.IsError)
            {
                throw new Exception("Problem while fetching data from the UserInfo endpoint", response.Exception);
            }

            var addressClaim = response.Claims.FirstOrDefault(c => c.Type.Equals("address"));

            User.AddIdentity(
                new ClaimsIdentity(
                    new List<Claim> 
                    { 
                        new Claim(addressClaim.Type.ToString(), addressClaim.Value.ToString())
                    }
                    )
                );
           
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}