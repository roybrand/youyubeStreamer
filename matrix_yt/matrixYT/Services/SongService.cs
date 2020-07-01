using System;
using System.Collections.Generic;
using System.Linq;
using matrixYT.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface ISongService
    {
        IEnumerable<Song> GetAll();
        Song GetById(int id);
        Song Create(Song song);
        void Update(Song song);
        void Delete(int id);
    }

    public class SongService : ISongService
    {
        private DataContext _context;

        public SongService(DataContext context)
        {
            _context = context;
        }

        
        public IEnumerable<Song> GetAll()
        {
            return _context.Songs;
        }

        public Song GetById(int id)
        {
            return _context.Songs.Find(id);
        }

        public Song Create(Song song)
        {
            // validation            

            if (_context.Songs.Any(x => x.Name == song.Name))
                throw new AppException("song name \"" + song.Name + "\" is already taken");         

            _context.Songs.Add(song);
            _context.SaveChanges();

            return song;
        }

        public void Update(Song songParam)
        {
            var song = _context.Songs.Find(songParam.Id);

            if (song == null)
                throw new AppException("Song not found");

            // update songName if it has changed
            if (!string.IsNullOrWhiteSpace(songParam.Name) && songParam.Name != song.Name)
            {
                // throw error if the new song is already taken
                if (_context.Songs.Any(x => x.Name == songParam.Name))
                    throw new AppException("SongName " + songParam.Name + " is already taken");

                song.Name = songParam.Name;
            }

            // update song properties if provided
            if (!string.IsNullOrWhiteSpace(songParam.Url))
                song.Url = songParam.Url;

            if (!string.IsNullOrWhiteSpace(songParam.Name))
                song.Name = songParam.Name;           

            _context.Songs.Update(song);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var song = _context.Songs.Find(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
                _context.SaveChanges();
            }
        }     
    }
}