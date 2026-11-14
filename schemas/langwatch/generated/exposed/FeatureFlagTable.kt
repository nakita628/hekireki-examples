package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object FeatureFlagTable : IdTable<String>("\"FeatureFlag\"") {
    override val id = text("key").entityId()
    val enabled = bool("enabled")
    val rules = jsonb<String>("rules", { it }, { it }).nullable()
    val lastEditedBy = text("lastEditedBy").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"FeatureFlag_pkey\"")
}
