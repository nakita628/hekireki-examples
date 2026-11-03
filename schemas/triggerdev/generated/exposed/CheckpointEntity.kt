package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CheckpointEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CheckpointEntity>(CheckpointTable)

    var friendlyId by CheckpointTable.friendlyId
    var type by CheckpointTable.type
    var location by CheckpointTable.location
    var imageRef by CheckpointTable.imageRef
    var reason by CheckpointTable.reason
    var metadata by CheckpointTable.metadata
    val events by CheckpointRestoreEventEntity referrersOn CheckpointRestoreEventTable.checkpointId
    var run by TaskRunEntity referencedOn CheckpointTable.runId
    var runId by CheckpointTable.runId
    var attempt by TaskRunAttemptEntity referencedOn CheckpointTable.attemptId
    var attemptId by CheckpointTable.attemptId
    var attemptNumber by CheckpointTable.attemptNumber
    var project by ProjectEntity referencedOn CheckpointTable.projectId
    var projectId by CheckpointTable.projectId
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn CheckpointTable.runtimeEnvironmentId
    var runtimeEnvironmentId by CheckpointTable.runtimeEnvironmentId
    var createdAt by CheckpointTable.createdAt
    var updatedAt by CheckpointTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CheckpointTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
