namespace ContactBook;

public class Contact : IEquatable<Contact>
{
    private string fname;
    private string lname;
    private string phone;
    private string email;

    public Contact(string fname = "", string lname = "", string phone = "", string email = "")
    {
        SetFName(fname);
        SetLName(lname);
        SetEmail(email);
        SetPhone(phone);


    }
        public string getFname()
    {
        
        return fname;
    }

        public string getLname()
    {
        
        return lname;
    }
    public string getPhone()
    {
    return phone;       

    }

        public string getEmail()
    {
        
        return email;
    }
    public void SetFName(string fname)
    {
        this.fname = fname;
    }


    public void SetLName(string lname)
    {
        this.lname = lname;
    }
    public void SetEmail(string email)
    {
        this.email = email;
    }

    public void SetPhone(string phone)
    {
        this.phone = phone;
    }
    public override string ToString()
    {
        return $"Contact[fname={fname}, lname={lname}, phone={phone}, email={email}]";
    }

    public bool Equals(Contact? other)
    {
        if (other is null) return false;
        return fname == other.fname && lname == other.lname && phone == other.phone && email == other.email;
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as Contact);
    }

    public static bool operator ==(Contact? x, Contact? y)
    {
        if (x is null) return y is null;
        return x.Equals(y);
    }

    public static bool operator !=(Contact? x, Contact? y)
    {
        return !(x == y);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(fname, lname, phone, email);
    }
}
