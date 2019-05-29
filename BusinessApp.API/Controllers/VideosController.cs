using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BusinessApp.API.Data;
using BusinessApp.API.Dtos;
using BusinessApp.API.Helpers;
using VideoModel = BusinessApp.API.Models.Video;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace BusinessApp.API.Controllers
{
    [Authorize]
    [Route("api/businesses/{businessId}/videos")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly IBusinessRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public VideosController(IBusinessRepository repo, IMapper mapper, 
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _repo = repo;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}", Name="GetVideo")]
        public async Task<IActionResult> GetVideo(int id)
        {
            var videoFromRepo = await _repo.GetVideo(id);

            var video = _mapper.Map<VideoForReturnDto>(videoFromRepo);

            return Ok(video);
        }

        [HttpPost]
        public async Task<IActionResult> AddVideoForUser(int businessId,
            [FromForm]VideoForCreationDto videoForCreationDto)
        {
            var businessFromRepo = await _repo.GetBusiness(businessId);

            if (businessFromRepo.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();


            var file = videoForCreationDto.File;

            var uploadResult = new VideoUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new VideoUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            if ( businessFromRepo.Video != null && businessFromRepo.Video.PublicId != null)
            {
                var deleteParams = new DeletionParams(businessFromRepo.Video.PublicId)
                                        {
                                            ResourceType = ResourceType.Video
                                        };

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    _repo.Delete(businessFromRepo.Video);
                }
            }

            videoForCreationDto.Url = uploadResult.Uri.ToString();
            videoForCreationDto.PublicId = uploadResult.PublicId;

            var video = _mapper.Map<VideoModel>(videoForCreationDto);

            businessFromRepo.Video = video;

            if (await _repo.SaveAll())
            {
                var videoToReturn = _mapper.Map<VideoForReturnDto>(video);
                return CreatedAtRoute("GetVideo", new { id = video.Id }, videoToReturn);
            }

            return BadRequest("Cloud not add the video");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int businessId, int id)
        {
            var businessFromRepo = await _repo.GetBusiness(businessId);

            if (businessFromRepo.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var videoFromRepo = await _repo.GetVideo(id);

            if (videoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(videoFromRepo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    _repo.Delete(videoFromRepo);
                }
            }
            
            if (videoFromRepo.PublicId == null)
            {
                _repo.Delete(videoFromRepo);
            }

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete the photo");
        }

    }
}