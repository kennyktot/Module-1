using System;
using System.Collections.Generic;

public class DictionaryRepository<TKey, TValue> where TKey : IComparable<TKey>
{
    private Dictionary<TKey, TValue> _items;

    
    public DictionaryRepository()
    {
        _items = new Dictionary<TKey, TValue>();
    }

    
    public void Add(TKey id, TValue item)
    {
        if (_items.ContainsKey(id))
        {
            throw new ArgumentException($"Item with key {id} already exists.");
        }
        _items.Add(id, item);
    }

   
    public TValue Get(TKey id)
    {
        if (!_items.ContainsKey(id))
        {
            throw new KeyNotFoundException($"Item with key {id} not found.");
        }
        return _items[id];
    }

    
    public void Update(TKey id, TValue newItem)
    {
        if (!_items.ContainsKey(id))
        {
            throw new KeyNotFoundException($"Item with key {id} not found.");
        }
        _items[id] = newItem;
    }

    public void Delete(TKey id)
    {
        if (!_items.ContainsKey(id))
        {
            throw new KeyNotFoundException($"Item with key {id} not found.");
        }
        _items.Remove(id);
    }

    
    public Dictionary<TKey, TValue> GetAll()
    {
        return new Dictionary<TKey, TValue>(_items);
    }
}

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }

    public Product(int productId, string productName)
    {
        ProductId = productId;
        ProductName = productName;
    }
}

public class Program
{
    public static void Main()
    {
        var productRepository = new DictionaryRepository<int, Product>();

        bool continueRunning = true;
        while (continueRunning)
        {
            Console.Clear();
            Console.WriteLine("Product Repository Management");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Get Product by ID");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Delete Product");
            Console.WriteLine("5. View All Products");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddProduct(productRepository);
                    break;
                case "2":
                    GetProductById(productRepository);
                    break;
                case "3":
                    UpdateProduct(productRepository);
                    break;
                case "4":
                    DeleteProduct(productRepository);
                    break;
                case "5":
                    ViewAllProducts(productRepository);
                    break;
                case "6":
                    continueRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            if (continueRunning)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }

    
    public static void AddProduct(DictionaryRepository<int, Product> repository)
    {
        try
        {
            Console.Write("\nEnter Product ID: ");
            int productId = int.Parse(Console.ReadLine());
            Console.Write("Enter Product Name: ");
            string productName = Console.ReadLine();

            var product = new Product(productId, productName);
            repository.Add(productId, product);
            Console.WriteLine("Product added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    
    public static void GetProductById(DictionaryRepository<int, Product> repository)
    {
        try
        {
            Console.Write("\nEnter Product ID to retrieve: ");
            int productId = int.Parse(Console.ReadLine());
            var product = repository.Get(productId);
            Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.ProductName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    
    public static void UpdateProduct(DictionaryRepository<int, Product> repository)
    {
        try
        {
            Console.Write("\nEnter Product ID to update: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter new Product Name: ");
            string newProductName = Console.ReadLine();

            var updatedProduct = new Product(productId, newProductName);
            repository.Update(productId, updatedProduct);
            Console.WriteLine("Product updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    
    public static void DeleteProduct(DictionaryRepository<int, Product> repository)
    {
        try
        {
            Console.Write("\nEnter Product ID to delete: ");
            int productId = int.Parse(Console.ReadLine());
            repository.Delete(productId);
            Console.WriteLine("Product deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    
    public static void ViewAllProducts(DictionaryRepository<int, Product> repository)
    {
        var products = repository.GetAll();
        if (products.Count == 0)
        {
            Console.WriteLine("\nNo products available.");
        }
        else
        {
            Console.WriteLine("\nProducts in Repository:");
            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.Key}, Name: {product.Value.ProductName}");
            }
        }
    }
}
