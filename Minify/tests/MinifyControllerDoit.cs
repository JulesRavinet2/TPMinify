using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Minify.Controllers;
using Minify.Interfaces;
using Minify.Model;
using Xunit;
using Moq;

namespace Minify.Tests
{
    public class MinifyControllerDoit
    {
        [Fact]
        public void AddData()
        {
            Mock<IRepository> database = new Mock<IRepository>();
            MinifyController minifyController = new MinifyController(database.Object);

            var data = new MinifyData
            {
                _id = null,
                Key = null,
                Url = "https://youtube.com"
            };

            minifyController.Add(data);
            
            database.Verify(f => f.Add(It.IsAny<MinifyData>()));
            database.Verify(f => f.Add(It.Is<MinifyData>(
                e => e.Url == "https://youtube.com")));
            
        }

        [Fact]
        public void GetData()
        {
            Mock<IRepository> database = new Mock<IRepository>();
            MinifyController minifyController = new MinifyController(database.Object);

            List<MinifyData> datas = new List<MinifyData>();
            
            datas.Add(new MinifyData
            {
                _id = null,
                Key = null,
                Url = "https://youtube.com"
            });

            database.Setup(f => f.Get())
                .Returns(datas);
            
            var result = minifyController.Get();
            
            Assert.Equal(datas, result);
        }
        
        [Fact]
        public void DeleteData()
        {
            Mock<IRepository> database = new Mock<IRepository>();
            MinifyController minifyController = new MinifyController(database.Object);

            string id = "695d843a-4fd4-4c49-9c45-298b295046b2";
            
            minifyController.Delete(id);
            
            database.Verify(f => f.Delete(It.IsAny<string>()));
            database.Verify(f => f.Delete(It.Is<string>(
                e => id == "695d843a-4fd4-4c49-9c45-298b295046b2")));
        }
    }
}