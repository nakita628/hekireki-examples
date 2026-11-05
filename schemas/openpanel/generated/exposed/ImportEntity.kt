package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class ImportEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, ImportEntity>(ImportTable)

    var projectId by ImportTable.projectId
    var project by ProjectEntity referencedOn ImportTable.projectId
    var jobId by ImportTable.jobId
    var status by ImportTable.status
    var statusMessage by ImportTable.statusMessage
    var errorMessage by ImportTable.errorMessage
    var config by ImportTable.config
    var totalEvents by ImportTable.totalEvents
    var processedEvents by ImportTable.processedEvents
    var currentStep by ImportTable.currentStep
    var currentBatch by ImportTable.currentBatch
    var createdAt by ImportTable.createdAt
    var completedAt by ImportTable.completedAt
    var updatedAt by ImportTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ImportTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
