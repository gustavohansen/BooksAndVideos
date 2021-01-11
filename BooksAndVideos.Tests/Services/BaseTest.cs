using System.Collections.Generic;
using BooksAndVideos.App.Entities;

namespace BooksAndVideos.Tests.Services
{
    public class BaseTest
    {
        protected Order GetOrderWithMembership()
        {
            return new Order
            {
                Customer = new Customer { Id = 1 },
                Items = new List<OrderLine> { new OrderLine {
                        Id = 1,
                        Count = 1,
                        Product = new Membership
                        {
                            Id = 1,
                            Price = 100,
                            MembershipType = MembershipType.BookClubMembership
                        }
                    }
                }
            };
        }

        protected Order GetOrderWithPhysicalProduct()
        {
            return new Order
            {
                Customer = new Customer { Id = 1 },
                Items = new List<OrderLine> { new OrderLine {
                        Id = 2,
                        Count = 1,
                        Product = new PhysicalProduct
                        {
                            Id = 1,
                            Price = 100
                        }
                    }
                }
            };
        }

        protected Order GetOrderWithPhysicalProductAndMembership()
        {
            return new Order
            {
                Customer = new Customer { Id = 1 },
                Items = new List<OrderLine> { new OrderLine {
                        Id = 2,
                        Count = 1,
                        Product = new PhysicalProduct
                        {
                            Id = 1,
                            Price = 100
                        }
                    },
                    new OrderLine {
                        Id = 2,
                        Count = 1,
                        Product = new Membership
                        {
                            Id = 1,
                            Price = 100,
                            MembershipType = MembershipType.BookClubMembership
                        }
                    }
                }
            };
        }
    }
}