using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.Dtos.Tokens
{
    public record TokenResponseDto(string Token,string UserName,DateTime ExpiredAt);
}
