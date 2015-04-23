using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Roslyn.Visug.NewCSharpFeatures.ReadOnlyAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Category
    {
        public IEnumerable<Product> Products { get; }

        public Category()
        {
            this.Products = new List<Product>();
        }
    }

    class Product
    {
        public IEnumerable<Category> Categories { get; set; }

        public Product()
        {
            this.Categories = new List<Category>();
        }
    }
}
