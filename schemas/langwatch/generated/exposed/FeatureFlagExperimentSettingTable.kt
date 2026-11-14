package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object FeatureFlagExperimentSettingTable : CompositeIdTable("\"FeatureFlagExperimentSetting\"") {
    val flagKey = text("flagKey").entityId()
    val subjectType =
        pgEnum(
            "subjectType",
            "\"FeatureFlagExperimentSubjectType\"",
            FeatureFlagExperimentSubjectType.entries,
            FeatureFlagExperimentSubjectType::dbName,
        ).entityId()
    val subjectId = text("subjectId").entityId()
    val enabled = bool("enabled")
    val changedByUserId = text("changedByUserId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(flagKey, subjectType, subjectId, name = "\"FeatureFlagExperimentSetting_pkey\"")

    init {
        index("\"FeatureFlagExperimentSetting_subjectType_subjectId_idx\"", false, subjectType, subjectId)
    }
}
