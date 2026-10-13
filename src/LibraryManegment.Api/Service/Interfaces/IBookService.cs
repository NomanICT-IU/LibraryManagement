using LibraryManegment.Api.Dtos.BookDtos;

namespace LibraryManegment.Api.Service.Interface;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync();

    Task<BookDto> GetByTitleAsync(string title);
    Task<bool> AddQuantityAsync(AddQuantityDto dto);

    Task<BookDto> CreateAsync(CreateBookDto dto);

    Task<bool> UpdateAsync(int id, UpdateBookDto dto);

    Task<bool> DeleteAsync(int id);
}
