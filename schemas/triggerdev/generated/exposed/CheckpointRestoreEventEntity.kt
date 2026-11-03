package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CheckpointRestoreEventEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CheckpointRestoreEventEntity>(CheckpointRestoreEventTable)

    var type by CheckpointRestoreEventTable.type
    var reason by CheckpointRestoreEventTable.reason
    var metadata by CheckpointRestoreEventTable.metadata
    var checkpoint by CheckpointEntity referencedOn CheckpointRestoreEventTable.checkpointId
    var checkpointId by CheckpointRestoreEventTable.checkpointId
    var run by TaskRunEntity referencedOn CheckpointRestoreEventTable.runId
    var runId by CheckpointRestoreEventTable.runId
    var attempt by TaskRunAttemptEntity referencedOn CheckpointRestoreEventTable.attemptId
    var attemptId by CheckpointRestoreEventTable.attemptId
    var project by ProjectEntity referencedOn CheckpointRestoreEventTable.projectId
    var projectId by CheckpointRestoreEventTable.projectId
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn CheckpointRestoreEventTable.runtimeEnvironmentId
    var runtimeEnvironmentId by CheckpointRestoreEventTable.runtimeEnvironmentId
    val taskRunDependency by TaskRunDependencyEntity optionalBackReferencedOn TaskRunDependencyTable.checkpointEventId
    val batchTaskRunDependency by BatchTaskRunEntity optionalBackReferencedOn BatchTaskRunTable.checkpointEventId
    var createdAt by CheckpointRestoreEventTable.createdAt
    var updatedAt by CheckpointRestoreEventTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CheckpointRestoreEventTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
