package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskRunWaitpointEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskRunWaitpointEntity>(TaskRunWaitpointTable)

    var taskRun by TaskRunEntity referencedOn TaskRunWaitpointTable.taskRunId
    var taskRunId by TaskRunWaitpointTable.taskRunId
    var waitpoint by WaitpointEntity referencedOn TaskRunWaitpointTable.waitpointId
    var waitpointId by TaskRunWaitpointTable.waitpointId
    var project by ProjectEntity referencedOn TaskRunWaitpointTable.projectId
    var projectId by TaskRunWaitpointTable.projectId
    var spanIdToComplete by TaskRunWaitpointTable.spanIdToComplete
    var batchId by TaskRunWaitpointTable.batchId
    var batch by BatchTaskRunEntity optionalReferencedOn TaskRunWaitpointTable.batchId
    var batchIndex by TaskRunWaitpointTable.batchIndex
    var createdAt by TaskRunWaitpointTable.createdAt
    var updatedAt by TaskRunWaitpointTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskRunWaitpointTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
