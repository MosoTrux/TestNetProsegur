using Microsoft.EntityFrameworkCore;
using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Application.Interfaces;
using TestNetProsegur.Core.Entities;
using TestNetProsegur.Core.Repositories;

namespace TestNetProsegur.Application.Implements
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository<MenuItem> _menuItemRepository;

        public MenuItemService(IRepository<MenuItem> menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<ServiceResponseDto<IEnumerable<MenuItem>>> GetAllAsync()
        {
            var response = new ServiceResponseDto<IEnumerable<MenuItem>>();
            try
            {
                var queryList = await _menuItemRepository.GetAll()
                    .Include(d => d.Ingredients)
                    .ToListAsync().ConfigureAwait(false);

                response.Data = queryList;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo obtener la lista. Exception: {ex.Message}");
            }
            return response;
        }

        public async Task<ServiceResponseDto<MenuItem>> Register(MenuItem entity)
        {
            var response = new ServiceResponseDto<MenuItem>();
            try
            {
                await _menuItemRepository.Add(entity);
                await _menuItemRepository.SaveChangesAsync();
                response.Data = entity;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo registrar el item. Exception: {ex.Message}");
            }
            return response;
        }

        public async Task<ServiceResponseDto<MenuItem>> Update(MenuItem entity)
        {
            var response = new ServiceResponseDto<MenuItem>();
            try
            {
                await _menuItemRepository.Add(entity);
                await _menuItemRepository.SaveChangesAsync();
                response.Data = entity;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo grabar el item. Exception: {ex.Message}");
            }
            return response;
        }
    }
}
