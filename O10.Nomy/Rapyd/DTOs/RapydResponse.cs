namespace O10.Nomy.Rapyd.DTOs
{
    public class RapydResponse<T>
    {
        public ResponseStatusDTO Status { get; set; }
        public T? Data { get; set; }
    }
}
