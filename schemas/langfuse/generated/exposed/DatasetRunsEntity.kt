package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class DatasetRunsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<DatasetRunsEntity>(DatasetRunsTable)

    val id1 by DatasetRunsTable.id1
    val projectId by DatasetRunsTable.projectId
    var name by DatasetRunsTable.name
    var description by DatasetRunsTable.description
    var metadata by DatasetRunsTable.metadata
    var datasetId by DatasetRunsTable.datasetId
    val dataset by DatasetEntity referencedOn DatasetRunsTable
    var createdAt by DatasetRunsTable.createdAt
    var updatedAt by DatasetRunsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DatasetRunsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
