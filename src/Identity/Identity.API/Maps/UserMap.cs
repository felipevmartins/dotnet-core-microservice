using Identity.Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Maps
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.HasKey(x => x.id);
            entityBuilder.ToTable("users");

            entityBuilder.Property(x => x.id).HasColumnName("id");
            entityBuilder.Property(x => x.name).HasColumnName("name");
            entityBuilder.Property(x => x.password).HasColumnName("password");
            entityBuilder.Property(x => x.email).HasColumnName("email");
        }
    }
}
