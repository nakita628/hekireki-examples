package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DatasetItemMediaEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DatasetItemMediaEntity>(DatasetItemMediaTable)

    var projectId by DatasetItemMediaTable.projectId
    var createdAt by DatasetItemMediaTable.createdAt
    var updatedAt by DatasetItemMediaTable.updatedAt
    var mediaId by DatasetItemMediaTable.mediaId
    var datasetId by DatasetItemMediaTable.datasetId
    var datasetItemId by DatasetItemMediaTable.datasetItemId
    var datasetItemValidFrom by DatasetItemMediaTable.datasetItemValidFrom
    var field by DatasetItemMediaTable.field
    var jsonPath by DatasetItemMediaTable.jsonPath
    var referenceString by DatasetItemMediaTable.referenceString

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DatasetItemMediaTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
