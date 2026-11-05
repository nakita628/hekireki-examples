package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class McpToolEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, McpToolEntity>(McpToolTable)

    var createdAt by McpToolTable.createdAt
    var updatedAt by McpToolTable.updatedAt
    var name by McpToolTable.name
    var description by McpToolTable.description
    var schema by McpToolTable.schema
    var isEnabled by McpToolTable.isEnabled
    var isWrite by McpToolTable.isWrite
    var connectionId by McpToolTable.connectionId
    var connection by McpConnectionEntity referencedOn McpToolTable.connectionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(McpToolTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
