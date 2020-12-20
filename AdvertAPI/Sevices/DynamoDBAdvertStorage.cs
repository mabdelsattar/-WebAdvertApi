using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertAPI.Models;
using AutoMapper;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace AdvertAPI.Sevices
{
    public class DynamoDBAdvertStorage : IAdvertStorageService
    {
        private readonly IMapper _mapper;
        public DynamoDBAdvertStorage(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<string> Add(AdvertModel model)
        {
            AdvertDBModel advertDBModel = _mapper.Map<AdvertDBModel>(model);
            advertDBModel.Id = new Guid().ToString();
            advertDBModel.CreationDateTime = DateTime.UtcNow;
            advertDBModel.Status = AdvertStatus.Pending;


            //now we need to store it into the DB 
            using (var client = new AmazonDynamoDBClient())
            {
                //becuase we have a cred profile on appsettings.json
                using (var context = new DynamoDBContext(client))
                {
                    await context.SaveAsync(advertDBModel);
                }
            }
            return advertDBModel.Id;

        }

        public async Task<bool> CheckHealthAsync()
        {
            using (var client = new AmazonDynamoDBClient())
            {
                var tableData = await client.DescribeTableAsync("Adverts");
                // if(tableData.Table.TableStatus  == "Active")
                //Or
                return string.Compare(tableData.Table.TableStatus, "active", true) == 0;
            }

        }

        public async Task Confirm(ConfirmAdvertModel model)
        {
            //now we need to store it into the DB 
            using (var client = new AmazonDynamoDBClient())
            {
                //becuase we have a cred profile on appsettings.json
                using (var context = new DynamoDBContext(client))
                {
                    var record = await context.LoadAsync<AdvertDBModel>(model.Id);
                    if (record == null)
                    {
                        throw new KeyNotFoundException($"a record with ID = { model.Id} was not found.");
                    }
                    if (model.Status == AdvertStatus.Active)
                    {
                        record.Status = AdvertStatus.Active;
                        await context.SaveAsync(record);
                    }
                    else 
                    {
                        await context.DeleteAsync(record);
                    }
                }
            }
        }
    }
}
