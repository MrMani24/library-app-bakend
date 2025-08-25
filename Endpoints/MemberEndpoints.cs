using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_app_bakend.DbContextes;
using library_app_bakend.DTOs.Common;
using library_app_bakend.DTOs.Members;
using library_app_bakend.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace library_app_bakend.Endpoints
{
    public static class MemberEndpoints
    {
        public static void MapMemberEndpoins(this WebApplication app)
        {
            app.MapGet("members/list", async ([FromServices] LibraryDB db) =>
            {
                var result = await db.Members.Select(m => new MemberListDto
                {
                    Id = m.Guid,
                    Firstname = m.Firstname,
                    Lastname = m.Lastname,
                    Wealth = m.Wealth
                }).ToListAsync();
                return result;
            });
            app.MapPost("member/create", async ([FromServices] LibraryDB db, [FromBody] MemberAddDto memberAddDto) =>
            {
                var MEmber = new Member
                {
                    Gender = memberAddDto.Gender,
                    Firstname = memberAddDto.Firstname,
                    Lastname = memberAddDto.Lastname,
                    Wealth = memberAddDto.Wealth,
                    Username = memberAddDto.Username,
                    Password = memberAddDto.Password
                };
                await db.Members.AddAsync(MEmber);
                await db.SaveChangesAsync();
                return new CommandResultDto
                {
                    Successfull = true,
                    Massage = "Member Added!"
                };
            });
            app.MapPut("member/update/{guid}", async ([FromRoute] string guid, [FromBody] MemberUpdateDto memberUpdateDto, [FromServices] LibraryDB db) =>
            {

                var s = await db.Members.FirstOrDefaultAsync(m => m.Guid == guid);
                if (s == null)
                {
                    return new CommandResultDto
                    {
                        Successfull = false,
                        Massage = "Member Not Fonud!"
                    };
                }
                s.Firstname = memberUpdateDto.Firstname;
                s.Lastname = memberUpdateDto.Lastname;
                s.Wealth = memberUpdateDto.Wealth;
                await db.SaveChangesAsync();
                return new CommandResultDto
                {
                    Successfull = true,
                    Massage = "Mamber Update!"
                };
            });
            app.MapDelete("member/remove/{guid}", async ([FromRoute] string guid, [FromServices] LibraryDB db) =>
            {
                var member = await db.Members.FirstOrDefaultAsync(m => m.Guid == guid);
                if (member == null)
                {
                    return new CommandResultDto
                    {
                        Successfull = false,
                        Massage = "Not Found!"
                    };
                }
                db.Members.Remove(member);
                await db.SaveChangesAsync();
                return new CommandResultDto
                {
                    Successfull = true,
                    Massage = "Member Removed."
                };
            });
        }
    }
}