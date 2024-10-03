namespace Data.Models.Listas
{
    public class ListaPaginadaComTotal<T> : ListaPaginada<T>
    {
        public double Total { get; set; }
    }
}
