package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DatasetEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DatasetEntity>(DatasetTable)

    var projectId by DatasetTable.projectId
    var name by DatasetTable.name
    var slug by DatasetTable.slug
    var columnTypes by DatasetTable.columnTypes
    var createdAt by DatasetTable.createdAt
    var updatedAt by DatasetTable.updatedAt
    var archivedAt by DatasetTable.archivedAt
    var mapping by DatasetTable.mapping
    var useS3 by DatasetTable.useS3
    var s3RecordCount by DatasetTable.s3RecordCount
    var contentLayout by DatasetTable.contentLayout
    var status by DatasetTable.status
    var statusError by DatasetTable.statusError
    var stagingKey by DatasetTable.stagingKey
    var sourceStoredObjectId by DatasetTable.sourceStoredObjectId
    var uploadFilename by DatasetTable.uploadFilename
    var rowCount by DatasetTable.rowCount
    var sizeBytes by DatasetTable.sizeBytes
    var chunkCount by DatasetTable.chunkCount
    var chunkOffsets by DatasetTable.chunkOffsets

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DatasetTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
