namespace ContactBook;

public class ContactMerger
{

    public static Dictionary <int, List<int>> FindDuplicates(List<Contact> contacts)
    {
        var phoneIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        var emailIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        var ds = new DuplicateSets(contacts.Count);

        for(int i = 0; i < contacts.Count ; i++)
        {
            Contact c = contacts[i];
            string phone = c.getPhone();
            string email = c.getEmail();

            if (!string.IsNullOrWhiteSpace(phone))
            {
                if(phoneIndex.TryGetValue(phone, out int existing)){ds.Union(existing, i);} else { phoneIndex[phone] = i;}
            }
             if (!string.IsNullOrWhiteSpace(email))
            {
                if(emailIndex.TryGetValue(email, out int existing)){ds.Union(existing, i);} else { emailIndex[email] = i;}
            }
        
        }

        var groups = new Dictionary<int, List<int>>();

        for(int i = 0 ; i < contacts.Count; i++)
        {
            int root = ds.FindRoot(i);

            if(!groups.TryGetValue(root, out var list))
            {
                list = new List<int>();
                groups[root] = list;
            }
            list.Add(i);
        }
        return groups;
    }
    private class DuplicateSets
    {
        private int[] parent;

        public DuplicateSets(int n)
        {
            parent = new int[n];
            
            for(int i =0; i< n; i++)
            {
                parent[i] = i;
            }
        }
        public int FindRoot(int i)
        {
            if(parent[i] != i) 
            {
                parent[i] = FindRoot(parent[i]);
            }
            return parent[i];
        }
        public void Union(int a, int b)
        {
            
            int rootA = FindRoot(a);
            int rootB = FindRoot(b);

            if(rootA != rootB)
            {
                parent[rootB] = rootA;
            }
        }
    }

}