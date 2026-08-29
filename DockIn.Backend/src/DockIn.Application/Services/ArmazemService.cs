public class ArmazemService
{
    IArmazemRepository _armazemRepository;

    public ArmazemService(IArmazemRepository armazemRepository)
    {
        _armazemRepository = armazemRepository;
    }


}
