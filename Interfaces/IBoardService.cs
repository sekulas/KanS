using KanS.Models;

namespace KanS.Interfaces;

public interface IBoardService {
    Task<int> CreateBoard();

}
