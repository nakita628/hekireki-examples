package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class DatasetEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<DatasetEntity>(DatasetTable)

    val id1 by DatasetTable.id1
    val projectId by DatasetTable.projectId
    var name by DatasetTable.name
    var description by DatasetTable.description
    var metadata by DatasetTable.metadata
    var remoteExperimentUrl by DatasetTable.remoteExperimentUrl
    var remoteExperimentPayload by DatasetTable.remoteExperimentPayload
    var remoteExperimentEnabled by DatasetTable.remoteExperimentEnabled
    var remoteExperimentSecretKey by DatasetTable.remoteExperimentSecretKey
    var remoteExperimentDisplaySecretKey by DatasetTable.remoteExperimentDisplaySecretKey
    var remoteExperimentRequestHeaders by DatasetTable.remoteExperimentRequestHeaders
    var remoteExperimentDisplayHeaders by DatasetTable.remoteExperimentDisplayHeaders
    var inputSchema by DatasetTable.inputSchema
    var expectedOutputSchema by DatasetTable.expectedOutputSchema
    val project by ProjectEntity referencedOn DatasetTable.projectId
    var createdAt by DatasetTable.createdAt
    var updatedAt by DatasetTable.updatedAt
    val datasetItems by DatasetItemEntity referrersOn DatasetItemTable
    val datasetRuns by DatasetRunsEntity referrersOn DatasetRunsTable

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DatasetTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
