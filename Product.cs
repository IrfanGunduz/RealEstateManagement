using System;
using System.Collections.Generic;

namespace Real_Estate_Management;

public class Product
{
// Private members and attributes
private string product_name;
private decimal price;
private double square_meters;

public string GetProductName() => product_name;
public decimal GetProductPrice() => price;
public double GetProductSquare_meters() => square_meters;

// Delegated constructors

public Product(): this("product_name", 0, 0.0){

}

public Product(string product, decimal price, double square_meters ){
    this.product_name = product;
    this.price = price;
    this.square_meters = square_meters;
}

// Public accessors or methods or attributes


}
