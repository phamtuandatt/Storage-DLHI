using Newtonsoft.Json;
using Storage.DataProvider;
using Storage.DTOs;
using Storage.Helper;
using Storage.RequestDto.SupplierRequestDto;
using Storage.Response;
using Storage.Response.SupplierResponseDto;
using Storage.Response.UnitResponseDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class SupplierDAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static async Task<DataTable> GetSuppiers()
        {
            //string sql = "SELECT *FROM SUPPLIER";

            //return data.GetData(sql, "cboSupplier");
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_SUPPLIERS}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<SuppliersResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "SUPPLIERS");
            }
        }

        public static async Task<DataTable> GetSuppierTypes()
        {
            //string sql = "SELECT *FROM SUPPLIER_TYPE";

            //return data.GetData(sql, "cboSupplierType");
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_SUPPLIER_TYPES}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<SupplierTypeDto>>(json).ToList();

                return API.ListToDataTable(res, "SUPPLIER_TYPES");
            }
        }

        public static async Task<SupplierDto> GetSupplier(Guid guid)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_SUPPLIER_BY_ID}{guid}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<SuppliersResponseDto>(json);

                SupplierDto dto = new SupplierDto()
                {
                    ID = Guid.Parse(res.Id.ToString()),
                    Code = res.Code.ToString(),
                    NameSupplier = res.NameSuppier.ToString(),
                    NameCompany = res.NameCompanySupplier.ToString(),
                    Address = res.Address.ToString(),
                    Phone = res.Phone.ToString(),
                    Email = res.Email.ToString(),
                    Note = res.Note.ToString(),
                };

                return dto ?? new SupplierDto();
            }
        }

        public static async Task<string> GetCurrentCodeSupplier(string code)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_CURRENT_CODE_SUPPLIER}{code}";
                string json = await client.GetStringAsync(url);

                return json;
            }
        }

        public static async Task<bool> Add(SupplierDto supplier)
        {
            var entity = new SupplierRequestDto()
            {
                Id = supplier.ID,
                Code = supplier.Code,
                NameSuppier = supplier.NameSupplier,
                NameCompanySupplier = supplier.NameCompany,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Note = supplier.Note,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(entity),
                    Encoding.UTF8, "application/json");

            var a = JsonConvert.SerializeObject(entity);

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_SUPPLIERS}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static async Task<bool> Update(SupplierDto supplier)
        {
            var entity = new SupplierRequestDto()
            {
                Id = supplier.ID,
                Code = supplier.Code,
                NameSuppier = supplier.NameSupplier,
                NameCompanySupplier = supplier.NameCompany,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Note = supplier.Note,
            };

            var putData = JsonConvert.SerializeObject(entity);
            StringContent content = new StringContent(putData,
                    Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync($"{API.API_DOMAIN}{API.PUT_SUPPLIERS}{entity.Id}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static async Task<bool> Delete(Guid id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{API.API_DOMAIN}{API.DELETE_SUPPLIER}{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static async Task<bool> AddSupplierType(SupplierTypeDto dto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto),
                                Encoding.UTF8, "application/json");

            var a = JsonConvert.SerializeObject(dto);

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_SUPPLIER_TYPES}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
    }
}
