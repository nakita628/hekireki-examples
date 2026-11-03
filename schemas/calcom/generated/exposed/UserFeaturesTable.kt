package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserFeaturesTable : CompositeIdTable("\"UserFeatures\"") {
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserFeatures_userId_fkey\"",
        )
    val featureId =
        reference(
            "featureId",
            FeatureTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserFeatures_featureId_fkey\"",
        )
    val enabled = bool("enabled")
    val assignedAt = pgTimestamp("assignedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val assignedBy = text("assignedBy")
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(userId, featureId, name = "\"UserFeatures_pkey\"")

    init {
        addIdColumn(userId)
        addIdColumn(featureId)
        index("\"UserFeatures_userId_featureId_idx\"", false, userId, featureId)
    }
}
