using System.Net.Http.Json;
using LaConcordia.DTO;
using LaConcordia.Interface;

namespace LaConcordia.Repository
{
    public class EmpresaRepository : IEmpresa
    {
        private readonly HttpClient _httpClient;

        public EmpresaRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EmpresaDTO>> GetEmpresaInfoAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<EmpresaDTO>>("api/Empresa/GetEmpresaInfoAll");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener las Empresas.", ex);
            }
        }

        public async Task<EmpresaDTO> GetEmpresaByRuc(string ruc)
        {
            return await _httpClient.GetFromJsonAsync<EmpresaDTO>($"api/Empresa/GetEmpresaByRuc/{ruc}");
        }

        public async Task InsertEmpresa(EmpresaDTO New)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Empresa/InsertEmpresa", New);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar InsertEmpresa: {errorContent}");
            }
        }

        public async Task UpdateEmpresa(EmpresaDTO UpdItem)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Empresa/UpdateEmpresa", UpdItem);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar Empresa: {errorContent}");
            }
        }

        public async Task DeleteEmpresaByRuc(string ruc)
        {
            var response = await _httpClient.DeleteAsync($"api/Empresa/DeleteEmpresaByRuc/ {ruc}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar Departamento: {errorContent}");
            }
        }
    }
}
