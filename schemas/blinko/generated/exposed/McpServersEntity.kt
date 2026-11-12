package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class McpServersEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, McpServersEntity>(McpServersTable)

    var name by McpServersTable.name
    var description by McpServersTable.description
    var type by McpServersTable.type
    var command by McpServersTable.command
    var args by McpServersTable.args
    var url by McpServersTable.url
    var env by McpServersTable.env
    var headers by McpServersTable.headers
    var isEnabled by McpServersTable.isEnabled
    var createdAt by McpServersTable.createdAt
    var updatedAt by McpServersTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(McpServersTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
