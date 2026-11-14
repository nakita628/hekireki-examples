package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnalyticsTable : IdTable<String>("\"Analytics\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val key = text("key")
    val value = jsonb<String>("value", { it }, { it }).nullable()
    val numericValue = double("numericValue").nullable()
    val stringValue = text("stringValue").nullable()
    val boolValue = bool("boolValue").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Analytics_pkey\"")

    init {
        index("\"Analytics_createdAt_key_projectId_numericValue_idx\"", false, createdAt, key, projectId, numericValue)
        index("\"Analytics_projectId_idx\"", false, projectId)
    }
}
