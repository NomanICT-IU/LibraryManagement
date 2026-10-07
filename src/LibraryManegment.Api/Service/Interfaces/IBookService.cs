using LibraryManegment.Api.Dtos.BookDtos;

namespace LibraryManegment.Api.Service.Interface;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync();

    Task<BookDto> GetByIdAsync(int id);

    Task<BookDto> CreateAsync(CreateBookDto dto);

    Task<bool> UpdateAsync(int id, UpdateBookDto dto);

    Task<bool> DeleteAsync(int id);
}
