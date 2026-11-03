package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProjectAlertEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProjectAlertEntity>(ProjectAlertTable)

    var friendlyId by ProjectAlertTable.friendlyId
    var project by ProjectEntity referencedOn ProjectAlertTable.projectId
    var projectId by ProjectAlertTable.projectId
    var environment by RuntimeEnvironmentEntity referencedOn ProjectAlertTable.environmentId
    var environmentId by ProjectAlertTable.environmentId
    var channel by ProjectAlertChannelEntity referencedOn ProjectAlertTable.channelId
    var channelId by ProjectAlertTable.channelId
    var status by ProjectAlertTable.status
    var type by ProjectAlertTable.type
    var taskRunAttemptId by ProjectAlertTable.taskRunAttemptId
    var taskRunId by ProjectAlertTable.taskRunId
    var workerDeployment by WorkerDeploymentEntity optionalReferencedOn ProjectAlertTable.workerDeploymentId
    var workerDeploymentId by ProjectAlertTable.workerDeploymentId
    var createdAt by ProjectAlertTable.createdAt
    var updatedAt by ProjectAlertTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProjectAlertTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
