using Moq;
using SklepRTV.MVC.Controllers;
using SklepRTV.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
namespace SklepRTV.Tests
{
    public class CategoryControllerTests
    {
        private readonly Mock<IApplicationDbContext> _mockContext;
        private readonly CategoryController _controller;
        public CategoryControllerTests()
        {
            _mockContext = new Mock<IApplicationDbContext>();
            var categories = new List<Category>
 {
 new Category { Id = 1, Name = "Electronics" },
 new Category { Id = 2, Name = "Home Appliances" }
 }.AsQueryable();
            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m =>
           m.Provider).Returns(categories.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m =>
           m.Expression).Returns(categories.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m =>
           m.ElementType).Returns(categories.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m =>
           m.GetEnumerator()).Returns(categories.GetEnumerator());
            _mockContext.Setup(c => c.Categories).Returns(mockSet.Object);
            _controller = new CategoryController(_mockContext.Object);
        }
        [Fact]
        public void Index_ReturnsViewResult_WithAListOfCategories()
        {
            // Act
            var result = _controller.Index();
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model =
           Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
        [Fact]
        public void Details_ReturnsViewResult_WithCategory()
        {
            // Act
            var result = _controller.Details(1);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model =
           Assert.IsAssignableFrom<Category>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }
        [Fact]
        public void Details_ReturnsNotFound_ForInvalidId()
        {
            // Act
            var result = _controller.Details(99);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}