namespace ContactBook;

public class ContactBook
{
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


public ContactBook(List<Contact> contacts = null)
    {
        allContacts = (contacts == null) ? new List<Contact>(): contacts;

    }
public void Start()
    {
        
        ShowWelcomeScreen();

        string input;
        do
        {
            ShowContacts();

            do
            {
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
        if(allContacts.Count <= 0){ 

        Console.WriteLine("No contacts found.");
    }
        else
        {

            for (int i = 0; i < allContacts.Count;i++){

                Console.WriteLine($"{i} {allContacts[i]}");
            }
        }
    }
        
        

    private void ShowInputOptions()
    {
        
    }

    private string GetInput()
    {
        return "";        
    }

    private bool isValidInput(string input)
    {
        return true;
    }

    private void ProcessInput(string input)
    {
        
    }

    private bool ConfirmExit()
    {
        return true;
    }

    private void showExitScreen()
    {
        
    }
    private void PressEnterContinue()
    {
        Console.Write("Press Enter to continue.");
        while(Console.ReadKey(true).Key != ConsoleKey.Enter){}
    }
}