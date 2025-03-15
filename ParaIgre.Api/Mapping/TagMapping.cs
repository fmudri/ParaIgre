// Mapping classes contain extension methods to convert between entities and DTOs
// This helps isolate the conversion logic and keeps controllers and endpoints clean


// Import namespaces for DTOs and entities.
using System;
using ParaIgre.Api.DTOs;    // Contains TagDTO.
using ParaIgre.Api.Entities;  // Contains Tag.

namespace ParaIgre.Api.Mapping
{
    // Static class to hold mapping methods for Tag.
    public static class TagMapping
    {
        // Converts a Tag entity to a TagDTO.
        public static TagDTO ToDTO(this Tag tag)
        {
            // Return a new TagDTO with the Tag's Id and Name.
            return new TagDTO(tag.Id, tag.Name);
        } 
    }
}

