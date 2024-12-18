using System;
using System.Collections.Generic;


namespace Real_Estate_Management
{
    

    class Program
    {
        static void Main(string[] args)
        {
            
            char selection;
            char seller_selection;
            char buyer_selection;
            bool exitMenu = false;
            bool subexitMenu = false;

            User x = new User("default", "default123", 0);
            Seller s = new Seller();
            Buyer b = new Buyer();
            

            do{
                x.display_menu();
                selection = x.get_selection();
                switch(selection){
                case '1':
                    x.show_ui(UserType.SELLER);
                    do{
                        s.display_menu();
                        seller_selection = s.get_selection();
                        switch(seller_selection){
                        case '1':
                        s.sell_outside();
                        break; 
                        case '2':
                        s.buy_outside(b); 
                        break;
                        case '3':
                        s.display_sell_list_outside(b);
                        break;
                        case '4':
                        s.display_rent_list_outside(b);
                        break;
                        case '5':
                        s.remove_product_sell_list();
                        break;
                        case '6':
                        s.remove_product_rent_list();
                        break;
                        case '7':
                        s.quit_handle();
                        subexitMenu = true;
                        break;
                        default:
                        s.handle_unknown();
                        break; 
                        }

                    } while(!subexitMenu);
                    break;
                
                case '2':
                    x.show_ui(UserType.BUYER);
                    do{
                        b.display_menu();
                        buyer_selection = b.get_selection();
                        switch(buyer_selection){
                        case '1':
                        b.sell_outside();
                        break; 
                        case '2':
                        b.buy_outside(s); 
                        break;
                        case '3':
                        b.display_sell_list_outside(s);
                        break;
                        case '4':
                        b.display_rent_list_outside(s);
                        break;
                        case '5':
                        b.remove_product_sell_list();
                        break;
                        case '6':
                        b.remove_product_rent_list();
                        break;
                        case '7':
                        b.quit_handle();
                        subexitMenu = true;
                        break;
                        default:
                        b.handle_unknown();
                        break; 
                        }
                    }while (!subexitMenu);
                    break;
                case'3':
                x.quit_handle();
                exitMenu = true;
                break;
                default:
                x.handle_unknown();
                break;
                }                        
            } while (!exitMenu);
        }
    }
}
