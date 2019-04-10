using Core.Business.CommandServices.Points;
using Core.Data.Commands;
using DatabaseFactory.Data;
using DatabaseFactory.Data.Contracts;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class CreatePointServiceTests
    {
        [Fact]
        public void CreateWithNullDatabaseWillThrow()
        {
            // Arrange
            IDatabase nullDatabase = null;

            // Act
            Action action = () => new CreatePointServiceStub(nullDatabase);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ExecuteWillPerformDatabaseProcedureToCreatePoint()
        {
            // Arrange
            var databaseMock = new Mock<IDatabase>();
            var serviceStub = new CreatePointServiceStub(databaseMock.Object);
            var command = new CreatePointCommand
            {
                UserId = 0
            };
            var transaction = Mock.Of<IDbTransaction>();
            var procedureMock = new Mock<IDbCommand>();
            var parameter = Mock.Of<IDbDataParameter>();
            var parameterCollectionMock = new Mock<IDataParameterCollection>();

            // Mock
            databaseMock.Setup(mock =>
                mock.TryExecuteTransaction(It.IsAny<Action<IDbTransaction>>())
            ).Callback((Action<IDbTransaction> transactionAction) => {
                transactionAction.Invoke(transaction);
            });

            databaseMock.Setup((mock) =>
                mock.CreateStoredProcCommand(
                    CreatePointServiceStub.StubbedProcedureName,
                    transaction)
            ).Returns(procedureMock.Object);

            databaseMock.Setup((mock) =>
                mock.CreateParameter(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(parameter);

            procedureMock.SetupGet(mock => mock.Parameters)
                .Returns(parameterCollectionMock.Object);

            // Act
            serviceStub.Execute(command);

            // Assert
            databaseMock.Verify(mock =>
                mock.TryExecuteTransaction(It.IsAny<Action<IDbTransaction>>()),
                Times.Once);

            databaseMock.Verify(mock =>
                mock.CreateStoredProcCommand(
                    CreatePointServiceStub.StubbedProcedureName,
                    transaction),
                    Times.Once);

            databaseMock.Verify(mock =>
                mock.CreateParameter("UserId", command.UserId),
                Times.AtLeastOnce);

            parameterCollectionMock.Verify(mock => mock.Add(parameter), Times.Once);

            databaseMock.Verify(mock => mock.Execute(procedureMock.Object), Times.Once);
        }
    }

    public class CreatePointServiceStub : CreatePointService
    {
        public CreatePointServiceStub(IDatabase database) : base(database)
        {
        }

        protected override string ProcedureName => StubbedProcedureName;

        public static string StubbedProcedureName => "Blog.Test";
    }

    public class DatabaseStub : SQLDatabase
    {
        public string ConnectionString => "SomeConnectionString";
        public DatabaseStub()
        {
            this.options.ConnectionString = ConnectionString;
        }
    }
}
