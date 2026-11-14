package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TriggerSentTable : IdTable<String>("\"TriggerSent\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val triggerId = text("triggerId")
    val traceId = text("traceId").nullable()
    val customGraphId = text("customGraphId").nullable()
    val projectId = text("projectId")
    val resolvedAt = pgTimestamp("resolvedAt", 3).nullable()
    val openIncidentKey = text("openIncidentKey").nullable().uniqueIndex("\"TriggerSent_openIncidentKey_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TriggerSent_pkey\"")

    init {
        index("\"TriggerSent_triggerId_idx\"", false, triggerId)
        index("\"TriggerSent_projectId_idx\"", false, projectId)
        index("\"TriggerSent_customGraphId_idx\"", false, customGraphId)
        index("\"TriggerSent_resolvedAt_idx\"", false, resolvedAt)
        index("\"TriggerSent_projectId_triggerId_createdAt_id_idx\"", false, projectId, triggerId, createdAt, id)
        uniqueIndex("\"TriggerSent_triggerId_traceId_key\"", triggerId, traceId)
    }
}
