package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object ScimSyncStateTable : IdTable<String>("\"ScimSyncState\"") {
    override val id = text("id").entityId()
    val connectionId = text("connectionId")
    val organizationId = text("organizationId")
    val state = text("state")
    val lastPushedAt = pgTimestamp("lastPushedAt", 3).nullable()
    val lastFailure = jsonb<String>("lastFailure", { it }, { it }).nullable()
    val deadLetters = jsonb<String>("deadLetters", { it }, { it }).default("[]")
    val revokedCause = text("revokedCause").nullable()
    val occurredAt = pgTimestamp("occurredAt", 3)
    val lastEventId = text("lastEventId")
    val acceptedAt = pgTimestamp("acceptedAt", 3)
    val projectionVersion = text("projectionVersion")
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"ScimSyncState_pkey\"")

    init {
        index("\"ScimSyncState_organizationId_idx\"", false, organizationId)
        index("\"ScimSyncState_state_idx\"", false, state)
        index("\"ScimSyncState_connectionId_idx\"", false, connectionId)
    }
}
