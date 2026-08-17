using System.Linq.Expressions;
using System.Security.Cryptography;

namespace ContactBook;

public class ContactBook
{


public const string YES = "Y";
public const string NO = "N";


public readonly string[] YES_NO = new string[] {YES,NO};
public const string NEXT_PAGE = "+";
public const string PREV_PAGE = "-";
public const string GOTO_PAGE = "G";
public const string PAGE_SIZE = "S";
public const string CREATE_CONTACT = "C";
public const string REVIEW_CONTACT = "R";
public const string UPDATE_CONTACT = "U";
public const string DELETE_CONTACT = "D";
public const string FIND_CONTACT = "F";
public const string ORDER_CONTACT = "O";
public const string DEDUPLICATE_CONTACT = "M";
public const string EXIT = "X";
    

public readonly String []COMMANDS = new string[]
{
NEXT_PAGE,
PREV_PAGE,
GOTO_PAGE,
PAGE_SIZE,
CREATE_CONTACT,
REVIEW_CONTACT,
UPDATE_CONTACT,
DELETE_CONTACT,
FIND_CONTACT,
ORDER_CONTACT,
DEDUPLICATE_CONTACT,
EXIT   
};

private List<Contact> allContacts;
private List<Contact> filteredContacts;
private int page;
private int size;
private bool _shouldExit = false;
private bool isExit;


public ContactBook(List<Contact> contacts = null)
    {
        allContacts = (contacts == null) ? new List<Contact>(): contacts;
        filteredContacts = allContacts;
        page = 1;
        size = 10;
        isExit = false;

    }
public void Start()
    {
        
        ShowWelcomeScreen();

        string input;
        do
        {
            

            do
            {
                Console.Clear();
                ShowContacts();
                ShowInputOptions();
                input = GetInput();
            }
            while(!isValidInput(input));

            ProcessInput(input);
        }
        while(!ConfirmExit());

        showExitScreen();
    }

    private void ShowWelcomeScreen()
    {
        Console.WriteLine("Welcome to Christian's Contact Book!");
        PressEnterContinue();
    }

    private void ShowContacts()
    {
        ShowContacts(filteredContacts, page, size);
    }
    private void ShowContacts(List<Contact> contacts, int page, int size)
    {
        if(filteredContacts.Count <= 0){ 

        Console.WriteLine("No contacts found.");
    }
        else
        {

            int indexCol = Math.Max("#".Length, filteredContacts.Count.ToString().Length);
            int fnameCol = Math.Max("First Name".Length, filteredContacts.Max(c => c.getFname()?.Length ?? 0));
            int lnameCol = Math.Max("Last Name".Length, filteredContacts.Max(c => c.getLname()?.Length ?? 0));
            int phoneCol = Math.Max("Phone".Length, filteredContacts.Max(c => c.getPhone()?.Length ?? 0));
            int emailCol = Math.Max("Email".Length, filteredContacts.Max(c => c.getEmail()?.Length ?? 0));

                Console.WriteLine(" "
                + "{0, "+ -indexCol +"} " 
                + "{1, "+ -fnameCol +"} "
                + "{2, " + -lnameCol + "} "
                + "{3, " + -phoneCol + "} "
                + "{4, " + -emailCol + "} ",
            "#","First Name", "Last Name", "Phone","Email");

            Console.WriteLine(new string('─', indexCol+2+fnameCol+2+lnameCol+2+phoneCol+2+emailCol));

            int n = contacts.Count;
            int pageCount = PageCount(contacts, size);
            int s =Math.Clamp((page - 1) *size,0,n);
            int e =Math.Clamp(s + size, 0, n);
            for (int i = s; i < e; i++){
                
                Contact c = filteredContacts[i];
                Console.WriteLine(" "
                + "{0, "+ -indexCol +"} " 
                + "{1, "+ -fnameCol +"} "
                + "{2, " + -lnameCol + "} "
                + "{3, " + -phoneCol + "} "
                + "{4, " + -emailCol + "} ",
                (i + 1), c.getFname(), c.getLname(), c.getPhone(), c.getEmail());
            }
            for(int i = 0; i < size - e + s; i++)
            {
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine($"Page {page} of {pageCount} ({s + 1}-{e} of {n})");
        }
    }
        
        

    private void ShowInputOptions()
    {
        Console.WriteLine($"\n[{NEXT_PAGE}] Next  [{PREV_PAGE}] Prev  [{GOTO_PAGE}] Go to page  [{PAGE_SIZE}] Page size");
        Console.WriteLine($"[{CREATE_CONTACT}] Create  [{REVIEW_CONTACT}] Review  [{UPDATE_CONTACT}] Update  [{DELETE_CONTACT}] Delete");
        Console.WriteLine($"[{FIND_CONTACT}] Find  [{ORDER_CONTACT}] Order  [{DEDUPLICATE_CONTACT}] Deduplicate  [{EXIT}] Exit");
        Console.Write("\nEnter command: ");
    }

    private string GetInput()
    {
        return Console.ReadLine()!.ToUpper();
    }

    private bool isValidInput(string input)
    {
        if (!COMMANDS.Contains(input))
        {
            Console.WriteLine("ERROR: Invalid input. Please try again");
            return false;

        }
        else
        {
            return true;
        }
    }

    private void ProcessInput(string input)
    {
        switch(input)
        {
    case NEXT_PAGE: NextPage(); break;
    case PREV_PAGE: PrevPage(); break;
    case GOTO_PAGE: GotoPage(); break;
    case PAGE_SIZE: PageSize(); break;
    case CREATE_CONTACT: CreateContact(); break;
    case REVIEW_CONTACT: ReviewContact(); break;
    case UPDATE_CONTACT: UpdateContact(); break;
    case DELETE_CONTACT: DeleteContact(); break;
    case FIND_CONTACT: FindContact(); break;
    case ORDER_CONTACT: OrderContact(); break;
    case DEDUPLICATE_CONTACT: DeduplicateContact(); break;
    case EXIT: Exit(); break;
    default: break;

        }
    }

    private bool ConfirmExit()
    {
        return (isExit) ? Confirm("Do you want to exit?",NO) : false;
    }

    private void showExitScreen()
    {
        Console.Clear();
        Console.WriteLine("Thank you for using Christian's Contact Book!");
        
    }
    private void PressEnterContinue()
    {
        Console.Write("Press Enter to continue.");
        Console.ReadLine();
    }

    private void NextPage()
    {
        NextPage(filteredContacts,ref page, size);
        
    }

    private void NextPage(List<Contact> contacts,ref int page, int size)
    {
        page = Math.Clamp(page + 1, 1, PageCount(contacts, size));
    }

   private void PrevPage()
    {
        PrevPage(filteredContacts,ref page, size);
    }
    private void PrevPage(List<Contact> contacts,ref int page, int size)
    {
        page = Math.Clamp(page - 1, 1, PageCount(contacts, size));
    }
    
    private void GotoPage(){
    GotoPage (filteredContacts, ref page, size);
    }
    private void GotoPage(List<Contact> contacts, ref int page, int size)
    {
     page = GetInt("Enter page", 1, PageCount(contacts, size));
    }

    private void PageSize()
    {
       PageSize(ref page, ref size);
    }
    private void PageSize(ref int page, ref int size)
    {
        int max = Console.WindowHeight - 10;
        size = GetInt("Enter page size", 1, max);
        page = 1;
    }

    private void CreateContact()
    {
       Console.Clear();
       Console.WriteLine(new string ('#', 80));
       Console.WriteLine(" Create Contact ");
       Console.WriteLine(new string ('#', 80));
       Console.WriteLine();
       Console.Write("Enter first name: ");
       string fname = Console.ReadLine()!;
       Console.Write("Enter last name: ");
       string lname = Console.ReadLine()!;
       Console.Write("Enter phone number: ");
       string phone = Console.ReadLine()!;
       Console.Write("Enter email: ");
       string email = Console.ReadLine()!;

        Console.WriteLine();
    
       if(Confirm("Do you want to create this contact?", YES))
        {
            Contact c = new Contact(fname, lname, phone, email);
            filteredContacts.Add(c);
            page = PageCount(filteredContacts, size);

            Console.WriteLine("Operation successfull: Contact created");
        }
        else{
            
            Console.WriteLine("Operation cancelled: Contact not created");
        
        }
        Console.WriteLine();
        PressEnterContinue();
    }

    private void ReviewContact(){ 
 
    int index = GetInt("Enter index", 1, filteredContacts.Count) - 1;

    Console.Clear();

       Console.WriteLine(new string ('#', 80));
       Console.WriteLine("Review Contact ");
       Console.WriteLine(new string ('#', 80));
       Console.WriteLine();

    ReviewContact(index);
    Console.WriteLine();
    PressEnterContinue();
    }
    private void ReviewContact(int index)
    {
        Contact c = filteredContacts[index];
    
       Console.WriteLine($"First Name: {c.getFname()}");
       Console.WriteLine($" Last Name: {c.getLname()}");
       Console.WriteLine($"     Phone: {c.getPhone()}");
       Console.WriteLine($"     Email: {c.getEmail()}");
       
        Console.WriteLine();
    }

    private void UpdateContact(){ 
 
    int index = GetInt("Enter index", 1, filteredContacts.Count) - 1;

    Console.Clear();

    Console.WriteLine(new string ('#', 80));
    Console.WriteLine("Update Contact ");
    Console.WriteLine(new string ('#', 80));
    Console.WriteLine();

    UpdateContact(index);
    Console.WriteLine();
    PressEnterContinue();
    }
    private void UpdateContact(int index)
    {
        Contact c = filteredContacts[index];

        

        string fname = c.getFname();
        string lname = c.getLname();
        string phone = c.getPhone();
        string email = c.getEmail();
        
        ReviewContact(index);
    
       if(Confirm("Do you want to edit the first name?", NO))
        {
            Console.Write("Enter first name:");
            fname = Console.ReadLine()!;

        }
        
       if(Confirm("Do you want to edit the last name?", NO))
        {
            Console.Write("Enter last name:");
            lname = Console.ReadLine()!;

        }
        
       if(Confirm("Do you want to edit the phone number?", NO))
        {
            Console.Write("Enter phone number:");
            phone = Console.ReadLine()!;

        }
        
       if(Confirm("Do you want to edit the email?", NO))
        {
            Console.Write("Enter email:");
            email = Console.ReadLine()!;

             Console.WriteLine();
        }
        if(Confirm("Do you want to update this contact?", NO))
        {
            c.SetFName(fname);
            c.SetLName(lname);
            c.SetPhone(phone);
            c.SetEmail(email);

            Console.WriteLine("Operation successfull: Contact updated");
        }
        else{
            
            Console.WriteLine("Operation cancelled: Contact not updated");
        
        }
       
    }

    private void DeleteContact()
    {
        int index = GetInt("Enter index", 1, filteredContacts.Count) - 1;

    Console.Clear();

    Console.WriteLine(new string ('#', 80));
    Console.WriteLine("Delete Contact ");
    Console.WriteLine(new string ('#', 80));
    Console.WriteLine();

    DeleteContact(index);
    
    Console.WriteLine();
    PressEnterContinue();
    }
    
    private void DeleteContact(int index)
    {
        Contact c = filteredContacts[index];

        
        ReviewContact(index);
        
        Console.WriteLine();

        if(Confirm("Do you want to delete this contact?", NO))
        {
            filteredContacts.Remove(c);

            Console.WriteLine("Operation successfull: Contact deleted");
        }
        else{
            
            Console.WriteLine("Operation cancelled: Contact not deleted");
        
        }
    }
       

    private void FindContact()
    {
        Console.Write("Enter search term (Clear): ");
        string searchTerm = Console.ReadLine()!.ToLower();
Console.WriteLine();

        if(Confirm("Do you want to search contacts?", YES))
        {
           filteredContacts = allContacts.FindAll(c => (c.getFname()+c.getLname()+ c.getPhone()+c.getEmail()).ToLower().Contains(searchTerm));
            page = 1;

            Console.WriteLine("Operation successfull: Contacts searched");
        }
        else{
            
            Console.WriteLine("Operation cancelled: Contacts not searched");
        
        }
        PressEnterContinue();
    }

    private void OrderContact()
    {
     Console.WriteLine("Order Contact");
    }

    private void DeduplicateContact()
    {
      Console.WriteLine("Deduplicate Contact");
    }

    private void Exit()
    {
        isExit = true;
    }

    private int GetInt(string prompt, int min, int max)
    {
         string options = $"[{min}-{max}]";

        Console.Write(prompt + $"[{options}] ");
        string answer = Console.ReadLine()!;
        
        int value;
        while (!int.TryParse(answer, out value) || value < min || value > max)
        {
            Console.WriteLine("Error: Invalid option. Please try again.");
            Console.Write(prompt + $"[{options}] ");
            answer = Console.ReadLine()!;
        }

        return value;
        
    }
    private string GetOption(string prompt, string[] validOptions, string defaultOption)
    {
        string options = string.Join('/', validOptions);
        Console.Write(prompt + $"[{options}]({defaultOption}) ");
        string option = Console.ReadLine()!.ToUpper();

        if (string.IsNullOrWhiteSpace(option)) {option = defaultOption;}

        while (!validOptions.Contains(option))
        {
            
            Console.WriteLine("ERROR: Invalid option. Please try again.");
            Console.Write(prompt + $"[{options}]({defaultOption}) ");
            option = Console.ReadLine()!.ToUpper();
        
            if (string.IsNullOrWhiteSpace(option)) {option = defaultOption;}
        }
    
    return option;
    
    }

    private bool Confirm(string prompt, string defaultOption)
    {
        return GetOption(prompt, YES_NO, defaultOption) == YES;

    }
    private static int PageCount(List <Contact> contacts,int size)
    {
        return(int) Math.Max(1, Math.Ceiling(contacts.Count/(double) size));

    }
}