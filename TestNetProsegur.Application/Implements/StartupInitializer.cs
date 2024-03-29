﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNetProsegur.Application.Interfaces;
using TestNetProsegur.Core.Entities;
using TestNetProsegur.Core.Repositories;

namespace TestNetProsegur.Application.Implements
{
    public class StartupInitializer : IStartupInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<Product> _productoRepository;
        private readonly IRepository<MenuItem> _menuItemRepository;

        public StartupInitializer(UserManager<IdentityUser> userManager, 
            IRepository<Product> productoRepository, 
            IRepository<MenuItem> menuItemRepository)
        {
            _userManager = userManager;
            _productoRepository = productoRepository;
            _menuItemRepository = menuItemRepository;
        }

        public async Task Initialize()
        {
            var users = GetUsers();
            foreach (var user in users)
            {
                await _userManager.CreateAsync(user, "12345aB@");
                
                if (user.UserName.Equals("administrator@outlook.com"))
                {
                    await _userManager.AddToRoleAsync(user, "ADMINISTRATOR");
                }
                else if (user.UserName.Equals("supervisor@outlook.com"))
                {
                    await _userManager.AddToRoleAsync(user, "SUPERVISOR");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "EMPLOYEED");
                }
            }

            var products = GetProducts();
            foreach (var product in products)
            {
                await _productoRepository.Add(product);
            }
            await _productoRepository.SaveChangesAsync();

            var menuItems = GetMenuItems();
            foreach (var menuItem in menuItems)
            {
                await _menuItemRepository.Add(menuItem);
            }
            await _menuItemRepository.SaveChangesAsync();
        }

        private List<IdentityUser> GetUsers()
        {
            return new List<IdentityUser>
            {
                new IdentityUser
                {
                    UserName = "administrator@outlook.com",
                    Email = "administrator@outlook.com"
                },
                new IdentityUser
                {
                    UserName = "supervisor@outlook.com",
                    Email = "supervisor@outlook.com"
                },
                new IdentityUser
                {
                    UserName = "employeed@outlook.com",
                    Email = "employeed@outlook.com"
                }
            };
        }

        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Cafe",
                    Stock = 20,
                    Unit = "KG"
                },
                new Product
                {
                    Name = "Leche",
                    Stock = 50,
                    Unit = "LT"
                },
                new Product
                {
                    Name = "Azúcar",
                    Stock = 200,
                    Unit = "KG"
                },
            };
        }

        private List<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem
                {
                    Category = "Drinks",
                    Name = "Hot Coffees",
                    Price = 15,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient
                        {
                            IdProduct = 1,
                            Quantity = 0.2M
                        }
                    }
                },
                new MenuItem
                {
                    Category = "Drinks",
                    Name = "Frappuccino",
                    Price = 19,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient
                        {
                            IdProduct = 1,
                            Quantity = 0.200M
                        },
                        new Ingredient
                        {
                            IdProduct = 2,
                            Quantity = 0.100M
                        },
                        new Ingredient
                        {
                            IdProduct = 3,
                            Quantity = 0.300M
                        },
                    }
                },
                new MenuItem
                {
                    Category = "Drinks",
                    Name = "Cold Coffees",
                    Price = 13,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient
                        {
                            IdProduct = 1,
                            Quantity = 0.200M
                        },
                        new Ingredient
                        {
                            IdProduct = 3,
                            Quantity = 0.300M
                        },
                    }
                },
            };
        }
    }
}
