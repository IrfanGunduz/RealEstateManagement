using System;
using System.Collections.Generic;


namespace Real_Estate_Management
{
    public enum UserType{
        SELLER,
        BUYER
    }
    public class User
    {   
        // Private members and attributes
        private string username {get; set;}
        private string password {get; set;}       
        protected decimal budget {get; private set;}

         // Delegated constructors
        public User() : this("USER", "password", 1000000)
        {
        }

        public User(string username, string password, decimal budget)
        {
            this.username = username;
            this.password = password;
            this.budget = budget;
        }

        // Public accessors or methods or attributes
        
        public string GetUsername() => username;
        

        protected void DeductBudget(decimal amount){
            if (amount > budget)
                throw new InvalidOperationException("Insufficent funds. ");
            budget -=amount;
        }

        protected void AddToBudget(decimal amount){
            budget += amount;
        }

        public void TransferBudget(User other, decimal amount){
        if (amount > budget)
            throw new InvalidOperationException("Insufficient funds.");
        DeductBudget(amount);
        other.AddToBudget(amount);
        }

        public virtual void display_menu()
        {
            Console.WriteLine("\n---------------------");
            Console.WriteLine(" 1 - Seller");
            Console.WriteLine(" 2 - Buyer"); 
            Console.WriteLine(" 3 - Quit"); 
            Console.WriteLine("Select your choice: \n ");
        }

        public bool checkPassword(string enteredPassword) {
            return enteredPassword == password;
        }
        public void show_ui(UserType userType){
            string userTypeStr = (userType == UserType.SELLER) ? "seller" : "buyer";
            User user = new User (userTypeStr, userTypeStr + "123", 1000000);

            Console.WriteLine("Please enter your username and password!\n");
            string enteredUsername, enteredPassword;

            while (true) {
                Console.WriteLine("User name: ");
                enteredUsername = Console.ReadLine();
                Console.WriteLine("Password ");
                enteredPassword = Console.ReadLine();

                if(enteredUsername == user.username){
                    if (user.checkPassword(enteredPassword)){
                        if(userType == UserType.SELLER){
                            Console.WriteLine("You have been logged in successfully Seller!");
                            break;
                        }else if (userType == UserType.BUYER){
                            Console.WriteLine("You have been logged in successfully Buyer!");
                            break;
                        }
                    } else {
                        Console.WriteLine("Wrong Password!");
                    }

                } else {
                    Console.WriteLine("Wrong Username!\nEnter Again!");
                }
            }
        }

        public virtual char get_selection(){
            char selection;
            selection = Convert.ToChar(Console.ReadLine());
            Console.WriteLine("\n");
            return selection;
        }

        public void handle_unknown(){
            Console.WriteLine("Unknown selection please try again!");
        }

        public void quit_handle(){
        Console.WriteLine("You have been logged out! ");
        }

    }
}
