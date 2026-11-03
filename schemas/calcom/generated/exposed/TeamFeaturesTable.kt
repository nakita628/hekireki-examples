package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TeamFeaturesTable : CompositeIdTable("\"TeamFeatures\"") {
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamFeatures_teamId_fkey\"",
        )
    val featureId =
        reference(
            "featureId",
            FeatureTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamFeatures_featureId_fkey\"",
        )
    val enabled = bool("enabled")
    val assignedAt = pgTimestamp("assignedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val assignedBy = text("assignedBy")
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(teamId, featureId, name = "\"TeamFeatures_pkey\"")

    init {
        addIdColumn(teamId)
        addIdColumn(featureId)
        index("\"TeamFeatures_teamId_featureId_idx\"", false, teamId, featureId)
    }
}
