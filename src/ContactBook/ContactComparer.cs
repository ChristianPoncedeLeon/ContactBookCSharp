namespace ContactBook;

public class ContactComparer : IComparer<Contact>
{

    public enum SortType
    {
        
        FName,
        LName,
        Phone,
        Email
    };

    private SortType sortType;
    public ContactComparer(SortType sortType)
    {
        SetSortType(sortType);
    }
    public SortType GetSortType()
    {
        return sortType;
    }
    
    public void SetSortType(SortType sortType)
    {
        this.sortType = sortType;
    }
        public int Compare(Contact? x, Contact? y)
    {
        int r = 0;
        switch (sortType)
        {
            case SortType.LName: return
                (r = string.Compare(x?.getLname(), y?.getLname())) != 0 ? r:
                (r = string.Compare(x?.getFname(), y?.getFname())) != 0 ? r:
                (r = string.Compare(x?.getPhone(), y?.getPhone())) != 0 ? r:
                string.Compare(x?.getEmail(), y?.getEmail());
            
            case SortType.Phone:return
                (r = string.Compare(x?.getPhone(), y?.getPhone())) != 0 ? r:
                (r = string.Compare(x?.getFname(), y?.getFname())) != 0 ? r:
                (r = string.Compare(x?.getLname(), y?.getLname())) != 0 ? r:
                string.Compare(x?.getEmail(), y?.getEmail());

            case SortType.Email:return
                (r = string.Compare(x?.getEmail(), y?.getEmail())) != 0 ? r:
                (r = string.Compare(x?.getFname(), y?.getFname())) != 0 ? r:
                (r = string.Compare(x?.getLname(), y?.getLname())) != 0 ? r:
                string.Compare(x?.getPhone(), y?.getPhone());


            case SortType.FName:
            default:return
                (r = string.Compare(x?.getFname(), y?.getFname())) != 0 ? r:
                (r = string.Compare(x?.getLname(), y?.getLname())) != 0 ? r:
                (r = string.Compare(x?.getPhone(), y?.getPhone())) != 0 ? r:
                string.Compare(x?.getEmail(), y?.getEmail());
            break;
        }
    }
}