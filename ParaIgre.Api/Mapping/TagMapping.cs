using System;
using ParaIgre.Api.DTOs;
using ParaIgre.Api.Entities;

namespace ParaIgre.Api.Mapping;

public static class TagMapping
{
    public static TagDTO ToDTO(this Tag tag)
    {
        return new TagDTO(tag.Id, tag.Name);
    } 
}
