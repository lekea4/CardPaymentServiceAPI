using AutoMapper;
using CardPaymentServiceAPI.DatabaseConnection.DBContext;
using CardPaymentServiceAPI.Models;
using CardPaymentServiceAPI.Models.DTOs;
using CardPaymentServiceAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardPaymentServiceAPI.Services
{
    public class FintechService : IFintechService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly CardPaymentServiceDbContext _context;
        private readonly IMapper _mapper;

        public FintechService(CardPaymentServiceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> AddFintech(FintechsDto fintechs)
        {
            Response response = new Response();
            try
            {
                var newFintech = _mapper.Map<Fintechs>(fintechs);
                newFintech.DateCreated = DateTime.Now;
                newFintech.CreationReference = Guid.NewGuid().ToString();
                newFintech.IsActive = true;
                var addFintech = await _context.AddAsync<Fintechs>(newFintech);
                await _context.SaveChangesAsync();
                response.ResponseMessage = "Success";
                response.ResponseCode = "00";
            }
            catch (Exception ex)
            {

                Logger.Error($"Error| Failed to Add Fintech with error details : \n {ex} \n");
                response.ResponseMessage = "Failed";
                response.ResponseCode = "02";
                
            }
            return response;

        }

        public Task<Response> DeleteFintech(FintechsDto fintechs)
        {
            throw new System.NotImplementedException();
        }


        public async Task<Responses<IEnumerable<FintechsDto>>> GetAllFintechs()
        {
          
            try
            {
                var fintechs = await _context.Fintechs.ToListAsync();
                var response = _mapper.Map<IEnumerable<FintechsDto>>(fintechs);
                return Responses<IEnumerable<FintechsDto>>.Success("Success", "00", response, 200);

            }
            catch (Exception ex)
            {

                Logger.Info($"Error getting fintechs with details:\n  {ex}\n");
                return Responses<IEnumerable<FintechsDto>>.Fail("Success", "00", 404);

            }

        }
    }
}
