using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using matrixYT.Entities;
using WebApi.Models.Users;
using WebApi.Models.Songs;

namespace matrixYT.Apis
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SongsController : ControllerBase
    {
        private ISongService _songService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public SongsController(
            ISongService songService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _songService = songService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

       
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]SongModel model)
        {
            // map model to entity
            var song = _mapper.Map<Song>(model);

            try
            {
                // create song
                _songService.Create(song);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var songs = _songService.GetAll();
            var model = _mapper.Map<IList<SongModel>>(songs);           
            
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var song = _songService.GetById(id);
            var model = _mapper.Map<SongModel>(song);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UpdateModel model)
        {
            // map model to entity and set id
            var song = _mapper.Map<Song>(model);
            song.Id = id;

            try
            {
                // update song 
                _songService.Update(song);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _songService.Delete(id);
            return Ok();
        }
    }
}
