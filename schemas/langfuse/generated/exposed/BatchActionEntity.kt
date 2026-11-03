package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BatchActionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BatchActionEntity>(BatchActionTable)

    var createdAt by BatchActionTable.createdAt
    var updatedAt by BatchActionTable.updatedAt
    var projectId by BatchActionTable.projectId
    var project by ProjectEntity referencedOn BatchActionTable.projectId
    var userId by BatchActionTable.userId
    var actionType by BatchActionTable.actionType
    var tableName by BatchActionTable.tableName1
    var status by BatchActionTable.status
    var finishedAt by BatchActionTable.finishedAt
    var query by BatchActionTable.query
    var config by BatchActionTable.config
    var totalCount by BatchActionTable.totalCount
    var processedCount by BatchActionTable.processedCount
    var failedCount by BatchActionTable.failedCount
    var log by BatchActionTable.log

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BatchActionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
