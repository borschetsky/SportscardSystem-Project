﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bytes2you.Validation;
using SportscardSystem.Data.Contracts;
using SportscardSystem.DTO;
using SportscardSystem.DTO.Contracts;
using SportscardSystem.Logic.Services.Contracts;
using SportscardSystem.Models;
using System;
using System.Linq;

namespace SportscardSystem.Logic.Services
{
    public class ClientService : IClientService
    {
        private readonly ISportscardSystemDbContext dbContext;
        private readonly IMapper mapper;

        public ClientService(ISportscardSystemDbContext dbContext, IMapper mapper)
        {
            Guard.WhenArgument(dbContext, "DbContext can not be null").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper can not be null").IsNull().Throw();

            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void AddClient(IClientDto clientDto)
        {
            Guard.WhenArgument(clientDto, "ClientDto can not be null").IsNull().Throw();

            var clientToAdd = this.mapper.Map<Client>(clientDto);
            
            this.dbContext.Clients.Add(clientToAdd);
            this.dbContext.SaveChanges();
        }

        //To be implemented
        public void DeleteClient(IClientDto clientDto)
        {
            Guard.WhenArgument(clientDto, "ClientDto can not be null").IsNull().Throw();
            var client = dbContext.Clients.FirstOrDefault(x => x.FirstName == clientDto.FirstName);
            if (client.FirstName == clientDto.FirstName)
            {
                client.IsDeleted = true;
                client.DeletedOn = DateTime.Now;
                dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("There are no such client!");
            }

            
        }

        public IQueryable<IClientDto> GetAllClients()
        {
            var allClients = this.dbContext.Clients.ProjectTo<ClientDto>();
            Guard.WhenArgument(allClients, "AllClients can not be null").IsNull().Throw();

            Console.WriteLine("test");
            return allClients;
        }

        public IQueryable<IClientDto> GetMostActiveClient()
        {
            throw new NotImplementedException();
        }

        public Guid GetCompanyGuidByName(string companyName)
        {
            Guid result;
            try
            {
                result = this.dbContext.Companies.FirstOrDefault(x => x.Name == companyName).Id;

            }
            catch (Exception)
            {

                throw new ArgumentException("No such company exists!");
            }

            return result;
        }
    }
}
