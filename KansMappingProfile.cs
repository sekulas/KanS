﻿using AutoMapper;
using KanS.Entities;
using KanS.Models;

namespace KanS;

public class KansMappingProfile : Profile {
    public KansMappingProfile() {

        CreateMap<UserRegisterDto, UserLoginDto>();

    }
}
