using AutoMapper;
using SpotifyApi.Domain.EntityModels;
using SpotifyApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Dtos
{
    public class ProfileConfiguration : Profile
    {
        public ProfileConfiguration()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();
             

            CreateMap<Album, AlbumDto>()
               .ForMember(d => d.Tracks, o => o.MapFrom( a => a.Tracks))
               .ReverseMap();

            CreateMap<Track, TrackDto>()
               .ForMember(d => d.Artists, o => o.MapFrom( a => a.Artists ))
               .ReverseMap();

            CreateMap<Artist, ArtistDto>()
               .ReverseMap();

            CreateMap<PlaylistAlbum, PlaylistAlbumDto>()
                .ForMember(d => d.Tracks, o => o.MapFrom(a => a.Tracks))
                .ReverseMap();

            CreateMap<PlaylistArtist, PlaylistArtistDto>()
               .ForMember(d => d.Tracks, o => o.MapFrom(a => a.Tracks))
              .ReverseMap();

            CreateMap<PlaylistTrack, PlaylistTrackDto>()
              .ReverseMap();
        }
    }
}
