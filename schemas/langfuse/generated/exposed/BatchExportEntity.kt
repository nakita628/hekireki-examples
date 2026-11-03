package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BatchExportEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BatchExportEntity>(BatchExportTable)

    var createdAt by BatchExportTable.createdAt
    var updatedAt by BatchExportTable.updatedAt
    var projectId by BatchExportTable.projectId
    var project by ProjectEntity referencedOn BatchExportTable.projectId
    var userId by BatchExportTable.userId
    var finishedAt by BatchExportTable.finishedAt
    var expiresAt by BatchExportTable.expiresAt
    var name by BatchExportTable.name
    var status by BatchExportTable.status
    var query by BatchExportTable.query
    var format by BatchExportTable.format
    var url by BatchExportTable.url
    var log by BatchExportTable.log

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BatchExportTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
