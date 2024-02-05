namespace Store.Models
{
    public class PagingInfo
    {
        public int TotalProducts { get; set; }
        public int ProductsPerPage {  get; set; }
        public int CurrentPage {  get; set; }
        public int TotalPages
            => (int)Math.Ceiling((decimal)TotalProducts / ProductsPerPage);
        public int StartPage {
            get
            {
                if(CurrentPage == TotalPages) return CurrentPage - 10;
                if (CurrentPage == 1) return CurrentPage;
                return CurrentPage - 1;
            }
        }

    }
}
