package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class BatchTaskRunErrorEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BatchTaskRunErrorEntity>(BatchTaskRunErrorTable)

    var batchTaskRun by BatchTaskRunEntity referencedOn BatchTaskRunErrorTable.batchTaskRunId
    var batchTaskRunId by BatchTaskRunErrorTable.batchTaskRunId
    var index by BatchTaskRunErrorTable.index
    var taskIdentifier by BatchTaskRunErrorTable.taskIdentifier
    var payload by BatchTaskRunErrorTable.payload
    var options by BatchTaskRunErrorTable.options1
    var error by BatchTaskRunErrorTable.error
    var errorCode by BatchTaskRunErrorTable.errorCode
    var createdAt by BatchTaskRunErrorTable.createdAt
}
