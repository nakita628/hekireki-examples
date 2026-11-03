package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskListEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskListEntity>(TaskListTable)

    var name by TaskListTable.name
    var orderIndex by TaskListTable.orderIndex
    var dataroomId by TaskListTable.dataroomId
    var dataroom by DataroomEntity referencedOn TaskListTable.dataroomId
    var teamId by TaskListTable.teamId
    var team by TeamEntity referencedOn TaskListTable.teamId
    val tasks by TaskEntity referrersOn TaskTable.taskListId
    var createdAt by TaskListTable.createdAt
    var updatedAt by TaskListTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskListTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
