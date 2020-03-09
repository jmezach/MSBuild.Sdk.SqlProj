using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace MSBuild.Sdk.SqlProj.BuildDacpac.Tests
{
    [TestClass]
    public class PackageBuilderTest
    {
        [TestMethod]
        [ValidPropertiesTestData]
        public void WithProperty_Valid(PropertyInfo property, string value, object expected)
        {
            // Arrange
            var packageBuilder = new PackageBuilder();

            // Act
            packageBuilder.SetProperty(property.Name, value);

            // Assert
            property.GetValue(packageBuilder.Options).ShouldBe(expected);
        }

        [TestMethod]
        public void WithProperty_UnknownProperty()
        {
            // Arrange
            var packageBuilder = new PackageBuilder();

            // Act & Assert
            Should.Throw<ArgumentException>(() => packageBuilder.SetProperty("MyUnknownProperty", "MyValue"));
        }

        [TestMethod]
        public void WithProperty_InvalidValue()
        {
            // Arrange
            var packageBuilder = new PackageBuilder();

            // Act
            Should.Throw<ArgumentException>(() => packageBuilder.SetProperty("QueryStoreIntervalLength", "MyFancyText"));
        }

        [TestMethod]
        public void UsingVersion()
        {
            // Arrange
            var packageBuilder = new PackageBuilder();

            // Act
            packageBuilder.UsingVersion(SqlServerVersion.Sql150);

            // Assert
            packageBuilder.Model.Version.ShouldBe(SqlServerVersion.Sql150);
        }

        class ValidPropertiesTestDataAttribute : Attribute, ITestDataSource
        {
            public IEnumerable<object[]> GetData(MethodInfo methodInfo)
            {
                var optionsType = typeof(TSqlModelOptions);
                return new List<object[]> {
                    new object[] { optionsType.GetProperty("QueryStoreIntervalLength"), "1", 1 },
                    new object[] { optionsType.GetProperty("QueryStoreFlushInterval"), "2", 2 },
                    new object[] { optionsType.GetProperty("QueryStoreDesiredState"), "ReadWrite", QueryStoreDesiredState.ReadWrite },
                    new object[] { optionsType.GetProperty("QueryStoreCaptureMode"), "Auto", QueryStoreCaptureMode.Auto },
                    new object[] { optionsType.GetProperty("ParameterizationOption"), "Forced", ParameterizationOption.Forced },
                    new object[] { optionsType.GetProperty("PageVerifyMode"), "TornPageDetection", PageVerifyMode.TornPageDetection },
                    new object[] { optionsType.GetProperty("QueryStoreMaxStorageSize"), "3", 3 },
                    new object[] { optionsType.GetProperty("NumericRoundAbortOn"), "True", true },
                    new object[] { optionsType.GetProperty("NestedTriggersOn"), "False", false },
                    new object[] { optionsType.GetProperty("HonorBrokerPriority"), "True", true },
                    new object[] { optionsType.GetProperty("FullTextEnabled"), "False", false },
                    new object[] { optionsType.GetProperty("FileStreamDirectoryName"), "Test", "Test" },
                    new object[] { optionsType.GetProperty("DbScopedConfigQueryOptimizerHotfixesSecondary"), "True", true },
                    new object[] { optionsType.GetProperty("DbScopedConfigQueryOptimizerHotfixes"), "False", false },
                    new object[] { optionsType.GetProperty("NonTransactedFileStreamAccess"), "ReadOnly", NonTransactedFileStreamAccess.ReadOnly },
                    new object[] { optionsType.GetProperty("DbScopedConfigParameterSniffingSecondary"), "True", true },
                    new object[] { optionsType.GetProperty("QueryStoreMaxPlansPerQuery"), "10", 10 },
                    new object[] { optionsType.GetProperty("QuotedIdentifierOn"), "False", false },
                    new object[] { optionsType.GetProperty("VardecimalStorageFormatOn"), "True", true },
                    new object[] { optionsType.GetProperty("TwoDigitYearCutoff"), "10", 10 },
                    new object[] { optionsType.GetProperty("Trustworthy"), "False", false },
                    new object[] { optionsType.GetProperty("TransformNoiseWords"), "True", true },
                    new object[] { optionsType.GetProperty("TornPageProtectionOn"), "False", false },
                    new object[] { optionsType.GetProperty("TargetRecoveryTimeUnit"), "Hours", TimeUnit.Hours },
                    new object[] { optionsType.GetProperty("QueryStoreStaleQueryThreshold"), "42", 42 },
                    new object[] { optionsType.GetProperty("TargetRecoveryTimePeriod"), "11", 11 },
                    new object[] { optionsType.GetProperty("ServiceBrokerOption"), "ErrorBrokerConversations", ServiceBrokerOption.ErrorBrokerConversations },
                    new object[] { optionsType.GetProperty("RecursiveTriggersOn"), "True", true } ,
                    new object[] { optionsType.GetProperty("DelayedDurabilityMode"), "Forced", DelayedDurabilityMode.Forced },
                    new object[] { optionsType.GetProperty("RecoveryMode"), "BulkLogged", RecoveryMode.BulkLogged },
                    new object[] { optionsType.GetProperty("ReadOnly"), "False", false },
                    new object[] { optionsType.GetProperty("SupplementalLoggingOn"), "True", true },
                    new object[] { optionsType.GetProperty("DbScopedConfigParameterSniffing"), "False", false },
                    new object[] { optionsType.GetProperty("DbScopedConfigMaxDOPSecondary"), "12", 12 },
                    new object[] { optionsType.GetProperty("DbScopedConfigMaxDOP"), "13", 13 },
                    new object[] { optionsType.GetProperty("AutoShrink"), "True", true },
                    new object[] { optionsType.GetProperty("AutoCreateStatisticsIncremental"), "False", false },
                    new object[] { optionsType.GetProperty("AutoCreateStatistics"), "True", true },
                    new object[] { optionsType.GetProperty("AutoClose"), "False", false },
                    new object[] { optionsType.GetProperty("ArithAbortOn"), "True", true },
                    new object[] { optionsType.GetProperty("AnsiWarningsOn"), "False", false },
                    new object[] { optionsType.GetProperty("AutoUpdateStatistics"), "True", true },
                    new object[] { optionsType.GetProperty("AnsiPaddingOn"), "False", false },
                    new object[] { optionsType.GetProperty("AnsiNullDefaultOn"), "True", true },
                    new object[] { optionsType.GetProperty("MemoryOptimizedElevateToSnapshot"), "False", false },
                    new object[] { optionsType.GetProperty("TransactionIsolationReadCommittedSnapshot"), "True", true },
                    new object[] { optionsType.GetProperty("AllowSnapshotIsolation"), "False", false },
                    new object[] { optionsType.GetProperty("Collation"), "Test", "Test" },
                    new object[] { optionsType.GetProperty("AnsiNullsOn"), "True", true },
                    new object[] { optionsType.GetProperty("AutoUpdateStatisticsAsync"), "False", false },
                    new object[] { optionsType.GetProperty("CatalogCollation"), "Latin1_General_100_CI_AS_KS_WS_SC", CatalogCollation.Latin1_General_100_CI_AS_KS_WS_SC },
                    new object[] { optionsType.GetProperty("ChangeTrackingAutoCleanup"), "True", true },
                    new object[] { optionsType.GetProperty("DbScopedConfigLegacyCardinalityEstimationSecondary"), "False", false },
                    new object[] { optionsType.GetProperty("DbScopedConfigLegacyCardinalityEstimation"), "True", true },
                    new object[] { optionsType.GetProperty("DBChainingOn"), "False", false },
                    new object[] { optionsType.GetProperty("DefaultLanguage"), "Test", "Test" },
                    new object[] { optionsType.GetProperty("DefaultFullTextLanguage"), "Test", "Test" },
                    new object[] { optionsType.GetProperty("DateCorrelationOptimizationOn"), "True", true },
                    new object[] { optionsType.GetProperty("DatabaseStateOffline"), "False", false },
                    new object[] { optionsType.GetProperty("CursorDefaultGlobalScope"), "True", true },
                    new object[] { optionsType.GetProperty("CursorCloseOnCommit"), "False", false },
                    new object[] { optionsType.GetProperty("Containment"), "Partial", Containment.Partial },
                    new object[] { optionsType.GetProperty("ConcatNullYieldsNull"), "True", true },
                    new object[] { optionsType.GetProperty("CompatibilityLevel"), "9", 9 },
                    new object[] { optionsType.GetProperty("ChangeTrackingRetentionUnit"), "Days", TimeUnit.Days },
                    new object[] { optionsType.GetProperty("ChangeTrackingRetentionPeriod"), "8", 8 },
                    new object[] { optionsType.GetProperty("ChangeTrackingEnabled"), "False", false },
                    new object[] { optionsType.GetProperty("UserAccessOption"), "Restricted", UserAccessOption.Restricted },
                    new object[] { optionsType.GetProperty("WithEncryption"), "True", true },
                };
            }

            public string GetDisplayName(MethodInfo methodInfo, object[] data)
            {
                if (data != null)
                {
                    var result = $"{methodInfo.Name} ({string.Join(",", data)})";
                    return result;
                }

                return null;
            }
        }
    }
}