package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class DatasetItemEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<DatasetItemEntity>(DatasetItemTable)

    val id1 by DatasetItemTable.id1
    val projectId by DatasetItemTable.projectId
    var status by DatasetItemTable.status
    var input by DatasetItemTable.input
    var expectedOutput by DatasetItemTable.expectedOutput
    var metadata by DatasetItemTable.metadata
    var sourceTraceId by DatasetItemTable.sourceTraceId
    var sourceObservationId by DatasetItemTable.sourceObservationId
    var datasetId by DatasetItemTable.datasetId
    val dataset by DatasetEntity referencedOn DatasetItemTable
    var createdAt by DatasetItemTable.createdAt
    var updatedAt by DatasetItemTable.updatedAt
    val validFrom by DatasetItemTable.validFrom
    var validTo by DatasetItemTable.validTo
    var isDeleted by DatasetItemTable.isDeleted

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DatasetItemTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
