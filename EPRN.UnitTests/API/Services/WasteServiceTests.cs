using AutoMapper;
using EPRN.Common.Dtos;
using EPRN.Common.Enums;
using EPRN.Portal.ViewModels;
using EPRN.UnitTests.Helpers;
using Humanizer;
using Microsoft.Extensions.Options;
using Moq;
using Waste.API.Configuration;
using Waste.API.Models;
using Waste.API.Repositories.Interfaces;
using Waste.API.Services;
using Waste.API.Services.Interfaces;

namespace EPRN.UnitTests.API.Services
{
    [TestClass]
    public class WasteServiceTests
    {
        private IWasteService _wasteService;
        private Mock<IMapper> _mockMapper;
        private Mock<IRepository> _mockRepository;
        private Mock<IOptions<AppConfigSettings>> _mockConfigSettings;

        [TestInitialize]
        public void Init()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<IRepository>();
            _mockConfigSettings = new Mock<IOptions<AppConfigSettings>>();

            var config = new AppConfigSettings
            {
                DeductionAmount = 100
            };

            _mockConfigSettings.Setup(m => m.Value).Returns(config);

            _wasteService = new WasteService(
                _mockMapper.Object,
                _mockRepository.Object,
                _mockConfigSettings.Object);
        }

        #region WasteType tests

        [TestMethod]
        public async Task WasteTypes_Returned_InOrder()
        {
            // Arrange
            var data = new List<WasteType>
            {
                new WasteType
                {
                    Id = 9,
                    Name = "Zoo"
                },
                new WasteType
                {
                    Id = 3,
                    Name = "Alphabetty Spaghetti"
                },
                new WasteType
                {
                    Id = 6,
                    Name = "Middle of the road"
                }
            };

            _mockRepository.Setup(c => c.List<WasteType>()).Returns(data);

            // Act
            var wasteTypes = await _wasteService.GetWasteTypes();

            // Assert
            _mockRepository.Verify(r => r.List<WasteType>(), Times.Once()); // test we called the expected function on the repo
            _mockMapper.Verify(m =>
                m.Map<List<WasteTypeDto>>(
                    It.Is<List<WasteType>>(p =>
                        TestHelper.CompareOrderedList(p, data, wt => wt.Name.ToString()))),
                    Times.Once()); // test that we called Map with the expected ordered list
        }

        [TestMethod]
        public async Task GetWasteType_ValidId_WasteTypeObject()
        {
            // Arrange
            var data = new List<WasteType>
            {
                new WasteType
                {
                    Id = 9,
                    Name = "Zoo"
                },
                new WasteType
                {
                    Id = 3,
                    Name = "Alphabetty Spaghetti"
                },
                new WasteType
                {
                    Id = 6,
                    Name = "Middle of the road"
                }
            };
            _mockRepository.Setup(r => r.GetById<WasteType>(It.Is<int>(p => p == data[1].Id))).ReturnsAsync(data[1]);

            // Act
            var wasteTypeDto = await _wasteService.GetWasteType(data[1].Id);

            // Assert
            _mockRepository.Verify(r => r.GetById<WasteType>(data[1].Id), Times.Once());
            _mockMapper.Verify(m => m.Map<WasteTypeDto>(It.Is<WasteType>(p => p == data[1])), Times.Exactly(1));
        }

        [TestMethod]
        public async Task GetWasteType_InValidId_NullObject()
        {
            // Arrange
            var data = new List<WasteType>
            {
                new WasteType
                {
                    Id = 9,
                    Name = "Zoo"
                },
                new WasteType
                {
                    Id = 3,
                    Name = "Alphabetty Spaghetti"
                },
                new WasteType
                {
                    Id = 6,
                    Name = "Middle of the road"
                }
            };
            _mockRepository.Setup(r => r.GetById<WasteType>(It.Is<int>(p => p == 0))).ReturnsAsync(data[1]);

            // Act
            var wasteTypeDto = await _wasteService.GetWasteType(data[1].Id);

            // Assert
            _mockRepository.Verify(r => r.GetById<WasteType>(data[1].Id), Times.Once());
            _mockMapper.Verify(m => m.Map<WasteTypeDto>(It.Is<WasteType>(p => p == data[1])), Times.Exactly(0));
        }

        [TestMethod]
        public async Task SaveWasteType_Succeeds_With_ValidIds()
        {
            // arrange
            int journeyId = 5;
            int wasteTypeId = 45;

            var wasteJourney = new WasteJourney
            {
            };

            _mockRepository.Setup(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId))).ReturnsAsync(wasteJourney);

            // act
            await _wasteService.SaveWasteType(journeyId, wasteTypeId);

            // assert
            _mockRepository.Verify(r => r.Update(It.Is<WasteJourney>(wj => wj == wasteJourney && wj.WasteTypeId == wasteTypeId)), Times.Once);
        }

        [TestMethod]
        public async Task SaveWasteSubType_Succeeds_With_ValidIds()
        {
            // arrange
            int journeyId = 5;
            int wasteSubTypeId = 45;

            var wasteJourney = new WasteJourney
            {
            };

            _mockRepository.Setup(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId))).ReturnsAsync(wasteJourney);

            // act
            await _wasteService.SaveWasteSubType(journeyId, wasteSubTypeId);

            // assert
            _mockRepository.Verify(r => r.Update(It.Is<WasteJourney>(wj => wj == wasteJourney && wj.WasteTypeId == wasteSubTypeId)), Times.Once);
        }

        [TestMethod]
        public async Task TestGetWasteType_Succeeeds_With_Valid_Id()
        {
            // arrange
            var journeyId = 8;
            var expectedWasteType = "testWasteType";
            var wasteJourney = new WasteJourney
            {
                Id = journeyId,
                WasteTypeId = 45,
                WasteType = new WasteType()
                {
                    Name = expectedWasteType
                }
            };

            _mockRepository.Setup(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId))).ReturnsAsync(wasteJourney);

            // act
            var result = await _wasteService.GetWasteTypeName(journeyId);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string));
            Assert.AreEqual(result, expectedWasteType);
            _mockRepository.Verify(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId)), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task TestGetWasteType_Fails_With_InValid_JourneyRecord()
        {
            // Arrange
            var journeyId = 8;
            var wasteJourney = new WasteJourney { };

            _mockRepository.Setup(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId))).ReturnsAsync(wasteJourney);

            // Act
            var result = await _wasteService.GetWasteTypeName(journeyId);

            //Assert
            Assert.IsNotNull(wasteJourney);
            _mockRepository.Verify(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId)), Times.Never());

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task TestGetWasteType_Fails_With_InValid_WasteTypeId()
        {
            // arrange
            var journeyId = 8;
            var expectedWasteType = "testWasteType";
            var wasteJourney = new WasteJourney
            {
                Id = journeyId,
                WasteTypeId = null,
                WasteType = new WasteType()
                {
                    Name = expectedWasteType
                }
            };

            _mockRepository.Setup(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId))).ReturnsAsync(wasteJourney);

            // Act
            var result = await _wasteService.GetWasteTypeName(journeyId);

            //Assert
            Assert.IsNotNull(wasteJourney.WasteTypeId);
            _mockRepository.Verify(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId)), Times.Never());

        }

        #endregion


        [TestMethod]
        public async Task SaveSelectedMonth_Succeeds_With_ValidIds_ReprocessedIt()
        {
            // arrange
            int journeyId = 8;
            int selectedMonth = 11;
            var wasteJourney = new WasteJourney { };

            wasteJourney.DoneWaste = DoneWaste.ReprocessedIt.ToString();

            _mockRepository.Setup(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId))).ReturnsAsync(wasteJourney);

            // act
            await _wasteService.SaveSelectedMonth(journeyId, selectedMonth);

            // assert
            _mockRepository.Verify(r => r.Update(It.Is<WasteJourney>(wj => wj == wasteJourney && wj.MonthReceived == selectedMonth)), Times.Once);
        }

        [TestMethod]
        public async Task SaveSelectedMonth_Succeeds_With_ValidIds_SentItOn()
        {
            // arrange
            int journeyId = 9;
            int selectedMonth = 1;
            var wasteJourney = new WasteJourney { };

            wasteJourney.DoneWaste = DoneWaste.SentItOn.ToString();

            _mockRepository.Setup(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId))).ReturnsAsync(wasteJourney);

            // act
            await _wasteService.SaveSelectedMonth(journeyId, selectedMonth);

            // assert
            _mockRepository.Verify(r => r.Update(It.Is<WasteJourney>(wj => wj == wasteJourney && wj.MonthSent == selectedMonth)), Times.Once);
        }

        [TestMethod]
        public async Task SaveTonnage_WithValidParameters_Succeeds()
        {
            // arrange
            var journeyId = 5;
            var tonnage = 56.3;
            _mockRepository.Setup(r => r.GetById<WasteJourney>(journeyId)).ReturnsAsync(new WasteJourney
            {
                Id = journeyId,
            });

            // act
            await _wasteService.SaveTonnage(journeyId, tonnage);

            // assert
            _mockRepository.Verify(r => r.Update(It.Is<WasteJourney>(p => p.Id == journeyId && p.Tonnes == tonnage)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task SaveTonnage_WithInvalidJourneyId_ThrowsArgumentNullException()
        {
            // arrange
            var journeyId = 5;
            var tonnage = 56.3;

            // act
            await _wasteService.SaveTonnage(journeyId, tonnage);

            // assert
            _mockRepository.Verify(r => r.Update(It.IsAny<WasteJourney>()), Times.Never);
        }

        [TestMethod]
        public async Task GetWhatHaveYouDoneWaste_Succeeeds_With_Valid_Id_ReprocessedIt()
        {
            // arrange
            var journeyId = 8;
            var expectedWhatHaveYouDoneWaste = DoneWaste.ReprocessedIt;
            var wasteJourney = new WasteJourney
            {
                Id = journeyId,
                DoneWaste = expectedWhatHaveYouDoneWaste.ToString()
            };

            _mockRepository.Setup(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId))).ReturnsAsync(wasteJourney);

            // act
            var result = await _wasteService.GetWhatHaveYouDoneWaste(journeyId);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DoneWaste));
            Assert.AreEqual(result, expectedWhatHaveYouDoneWaste);
            _mockRepository.Verify(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId)), Times.Once());
        }

        [TestMethod]
        public async Task GetWhatHaveYouDoneWaste_Succeeeds_With_Valid_Id_SentItOn()
        {
            // arrange
            var journeyId = 8;
            var expectedWhatHaveYouDoneWaste = DoneWaste.SentItOn;
            var wasteJourney = new WasteJourney
            {
                Id = journeyId,
                DoneWaste = expectedWhatHaveYouDoneWaste.ToString()
            };

            _mockRepository.Setup(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId))).ReturnsAsync(wasteJourney);

            // act
            var result = await _wasteService.GetWhatHaveYouDoneWaste(journeyId);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DoneWaste));
            Assert.AreEqual(result, expectedWhatHaveYouDoneWaste);
            _mockRepository.Verify(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId)), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task TestGetWhatHaveYouyDoneWaste_Fails_With_InValid_JourneyRecord()
        {
            // Arrange
            var journeyId = 8;
            var wasteJourney = new WasteJourney { };

            _mockRepository.Setup(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId))).ReturnsAsync(wasteJourney);

            // Act
            var result = await _wasteService.GetWhatHaveYouDoneWaste(journeyId);

            //Assert
            Assert.IsNotNull(wasteJourney);
            _mockRepository.Verify(r => r.GetById<WasteJourney>(It.Is<int>(p => p == journeyId)), Times.Never());

        }
    }
}
