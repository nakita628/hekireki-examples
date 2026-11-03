package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object FeatureTable : IdTable<String>("\"Feature\"") {
    override val id = text("slug").entityId()
    val enabled = bool("enabled").default(false)
    val description = text("description").nullable()
    val type = pgEnum("type", "\"FeatureType\"", FeatureType.entries, FeatureType::dbName).nullable().default(FeatureType.RELEASE)
    val stale = bool("stale").nullable().default(false)
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    val createdAt =
        pgTimestamp("createdAt", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt =
        pgTimestamp("updatedAt", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = integer("updatedBy").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Feature_pkey\"")

    init {
        uniqueIndex("\"Feature_slug_key\"", id)
        index("\"Feature_enabled_idx\"", false, enabled)
        index("\"Feature_stale_idx\"", false, stale)
    }
}
