package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SchemaOperationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SchemaOperationEntity>(SchemaOperationTable)

    var type by SchemaOperationTable.type
    var status by SchemaOperationTable.status
    var phase by SchemaOperationTable.phase
    var resourceType by SchemaOperationTable.resourceType
    var resourceId by SchemaOperationTable.resourceId
    var baseId by SchemaOperationTable.baseId
    var tableId by SchemaOperationTable.tableId
    var idempotencyKey by SchemaOperationTable.idempotencyKey
    var payload by SchemaOperationTable.payload
    var result by SchemaOperationTable.result
    var attempts by SchemaOperationTable.attempts
    var maxAttempts by SchemaOperationTable.maxAttempts
    var nextRunAt by SchemaOperationTable.nextRunAt
    var lockedAt by SchemaOperationTable.lockedAt
    var lockedBy by SchemaOperationTable.lockedBy
    var lastError by SchemaOperationTable.lastError
    var createdTime by SchemaOperationTable.createdTime
    var createdBy by SchemaOperationTable.createdBy
    var lastModifiedTime by SchemaOperationTable.lastModifiedTime
    var lastModifiedBy by SchemaOperationTable.lastModifiedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SchemaOperationTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
