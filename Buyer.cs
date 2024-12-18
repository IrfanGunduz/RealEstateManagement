using System;
using System.Collections.Generic;


namespace Real_Estate_Management;

public class Buyer : User
{
 // Private members and attributes

private List<Product> SellingProducts { get; set; } = new List<Product>();
private List<Product> RentProducts {get; set;} = new List<Product>();
private List<Product> OwnedProducts {get; set;} = new List<Product>();

public List<Product> GetOwnedProducts() => OwnedProducts;
public List<Product> GetSellListingProducts() => SellingProducts;
public List<Product> GetRentProducts() => RentProducts;

 // Constructors
public Buyer():this ("buyer", "password", 10000){

}

public Buyer(string username, string password, decimal budget)
    : base(username, password, budget) {}
    

// Public accessors or methods or attributes


public void AddProductToSell(Product product){
    if(OwnedProducts.Contains(product)){
        OwnedProducts.Remove(product);
        SellingProducts.Add(product);
        Console.WriteLine($"{GetUsername()} listed {product.GetProductName()} name of the product have been listed to sale");
    } else {
        Console.WriteLine($"You do not own {product.GetProductName()} to sell. ");
    }   
}

public void AddProductToRent(Product product){
    if(OwnedProducts.Contains(product)){
        OwnedProducts.Remove(product);
        RentProducts.Add(product);
        Console.WriteLine($"{GetUsername()} listed {product.GetProductName()} name of the product have been listed to rent");
    } else {
        Console.WriteLine($"You do not own {product.GetProductName()} to rent. ");
    }
    
}

public void BuyProduct(Product product, Seller seller){
        if (seller == null || !seller.GetSellListingProducts().Contains(product)){
            Console.WriteLine($"The product {product.GetProductName()} is not available in {seller?.GetUsername()}'s list.");
            return;
        }

        if (budget < product.GetProductPrice()){
            Console.WriteLine("Insufficient budget.");
            return;
        }

        if (seller == (User)this){
            Console.WriteLine("You cannot buy your own product. ");
            return;
        }

        TransferBudget(seller, product.GetProductPrice());
        seller.RemoveProduct(product);
        OwnedProducts.Add(product);

        Console.WriteLine($"{GetUsername()} bought {product.GetProductName()} from {seller.GetUsername()}.");
    }

public void RentProduct(Product product, Seller seller){
        if (seller == null || !seller.GetRentProducts().Contains(product)){
            Console.WriteLine($"The product {product.GetProductName()} is not available in {seller?.GetUsername()}'s list.");
            return;
        }

        if (budget < product.GetProductPrice()){
            Console.WriteLine("Insufficient budget.");
            return;
        }

        if (seller == (User)this){
            Console.WriteLine("You cannot rent your own product. ");
            return;
        }

        TransferBudget(seller, product.GetProductPrice());
        seller.RemoveProduct(product);
        OwnedProducts.Add(product);

        Console.WriteLine($"{GetUsername()} rented {product.GetProductName()} from {seller.GetUsername()}.");
    }


    public void SellProduct(Product product, User seller){
    if(!SellingProducts.Contains(product)){
        Console.WriteLine($"Product {product.GetProductName()} is not in your selling list.");
        return;
    }

    if (seller == this){
        Console.WriteLine("You cannot buy your own product.");
        return;
    }
    
    TransferBudget(seller, product.GetProductPrice());
    SellingProducts.Remove(product);
    Console.WriteLine($"{GetUsername()} sold {product.GetProductName()} to {seller.GetUsername()}.");
    
}

    public void ListOwnedProducts(){
        Console.WriteLine($"{GetUsername()}'s owned products:");
        foreach (var product in OwnedProducts)
        {
            Console.WriteLine($"- {product.GetProductName()} (${product.GetProductPrice()})");
        }
    }

    public void ListProductsForSale(List<Product> products) {
    foreach (var product in products) {
        Console.WriteLine($"- {product.GetProductName()} (${product.GetProductPrice()}) - {product.GetProductSquare_meters()} m²");
    }
}

    public Product FindProductByName(List<Product> products, string name){
        return products.FirstOrDefault(p => p.GetProductName().Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public void RemoveProduct(Product product){
    if (SellingProducts.Contains(product)){
        SellingProducts.Remove(product);
        Console.WriteLine($"{GetUsername()} removed {product.GetProductName()} from the selling list.");
    }
    else if (RentProducts.Contains(product)){
        RentProducts.Remove(product);
        Console.WriteLine($"{GetUsername()} removed {product.GetProductName()} from the rent list.");
    }else{
        Console.WriteLine($"Product {product.GetProductName()} is not in your selling or renting list.");
    }     
}

public void sell_outside(){
    Console.WriteLine("Do you want to Sell Or Rent?");
    string selection = Console.ReadLine();
    string selection_up = selection.ToUpper();
        if (selection_up == "SELL"){
            Console.WriteLine("Enter an Home/Lot/Office name to add to the sell list:  ");
            string product_name = Console.ReadLine();
            Console.WriteLine("Enter price: ");
            decimal product_price = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("ENTER square meters ");
            double product_square_meters = Convert.ToDouble(Console.ReadLine());
            Product product1 = new Product(product_name, product_price, product_square_meters);
            OwnedProducts.Add(product1);
            AddProductToSell(product1);

        }else if (selection_up == "RENT"){
            Console.WriteLine("Enter an Home/Lot/Office name to add to the rent list:  ");
            string product_name = Console.ReadLine();
            Console.WriteLine("Enter price: ");
            decimal product_price = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("ENTER square meters ");
            double product_square_meters = Convert.ToDouble(Console.ReadLine());
            Product product2 = new Product(product_name, product_price, product_square_meters);
            OwnedProducts.Add(product2);
            AddProductToRent(product2);           
        }
    }
public void buy_outside(Seller s){
   s.ListProductsForSale(s.GetSellListingProducts());
   Console.WriteLine($"Please Enter the product name you want it to buy from {s.GetUsername()}");
   string productname = Console.ReadLine();
   string productnameup = productname.ToUpper();
   Product p = s.FindProductByName(s.GetSellListingProducts(), productnameup);
   if(p !=null){
    BuyProduct(p, s);
   } else{
    Console.WriteLine($"{productnameup}Not found or already sold.");
   }

}

public void display_sell_list_outside(Seller s){
    Console.WriteLine("Seller selling list: \n");
    ListProductsForSale(GetSellListingProducts());
    Console.WriteLine("Buyer selling list: \n");
    s.ListProductsForSale(s.GetSellListingProducts());

}

public void display_rent_list_outside(Seller s){
    Console.WriteLine("Seller renting list: ");
    ListProductsForSale(GetRentProducts());
    Console.WriteLine("Buyer renting list: ");
    s.ListProductsForSale(s.GetRentProducts());
}

public void remove_product_sell_list(){
Console.WriteLine($"Please Enter the product name you want it to remove {GetSellListingProducts()}");
   string productname = Console.ReadLine();
   string productnameup = productname.ToUpper();
   Product p = FindProductByName(GetSellListingProducts(), productnameup);
   if (p != null)
    {
        RemoveProduct(p);
        Console.WriteLine("Product removed successfully.");
    }
    else
    {
        Console.WriteLine("Product not found in the sell list.");
    }
}

public void remove_product_rent_list(){
    Console.WriteLine($"Please Enter the product name you want it to remove {GetRentProducts()}");
    string productname = Console.ReadLine();
    string productnameup = productname.ToUpper();
    Product p = FindProductByName(GetRentProducts(), productnameup);
    if (p != null)
    {
        RemoveProduct(p);
        Console.WriteLine("Product removed successfully.");
    }
    else
    {
        Console.WriteLine("Product not found in the rent list.");
    }
}


public override void display_menu(){
    Console.WriteLine("\n---------------------");
    Console.WriteLine(" 1 - Sell");
    Console.WriteLine(" 2 - Buy");
    Console.WriteLine(" 3 - Display list of on sale" );
    Console.WriteLine(" 4 - Display list of on rent");
    Console.WriteLine(" 5 - Remove product from sell list");
    Console.WriteLine(" 6 - Remove product from rent list");
    Console.WriteLine(" 7 - Quit");
    Console.WriteLine("Select your choice: \n ");
    }

public override char get_selection(){
    char seller_selection = Convert.ToChar(Console.ReadLine());
    Console.WriteLine("\n");
    return seller_selection;
}


}
