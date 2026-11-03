package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskRunCheckpointEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskRunCheckpointEntity>(TaskRunCheckpointTable)

    var friendlyId by TaskRunCheckpointTable.friendlyId
    var type by TaskRunCheckpointTable.type
    var location by TaskRunCheckpointTable.location
    var imageRef by TaskRunCheckpointTable.imageRef
    var reason by TaskRunCheckpointTable.reason
    var metadata by TaskRunCheckpointTable.metadata
    var project by ProjectEntity referencedOn TaskRunCheckpointTable.projectId
    var projectId by TaskRunCheckpointTable.projectId
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn TaskRunCheckpointTable.runtimeEnvironmentId
    var runtimeEnvironmentId by TaskRunCheckpointTable.runtimeEnvironmentId
    val executionSnapshot by TaskRunExecutionSnapshotEntity optionalReferrersOn TaskRunExecutionSnapshotTable.checkpointId
    var createdAt by TaskRunCheckpointTable.createdAt
    var updatedAt by TaskRunCheckpointTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskRunCheckpointTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
