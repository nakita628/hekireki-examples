package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class FontsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, FontsEntity>(FontsTable)

    var name by FontsTable.name
    var displayName by FontsTable.displayName
    var url by FontsTable.url
    var fileData by FontsTable.fileData
    var isLocal by FontsTable.isLocal
    var isSystem by FontsTable.isSystem
    var weights by FontsTable.weights
    var category by FontsTable.category
    var sortOrder by FontsTable.sortOrder
    var createdAt by FontsTable.createdAt
    var updatedAt by FontsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FontsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
