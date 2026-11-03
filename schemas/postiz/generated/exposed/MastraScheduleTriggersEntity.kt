package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraScheduleTriggersEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraScheduleTriggersEntity>(MastraScheduleTriggersTable)

    var scheduleId by MastraScheduleTriggersTable.scheduleId
    var runId by MastraScheduleTriggersTable.runId
    var scheduledFireAt by MastraScheduleTriggersTable.scheduledFireAt
    var actualFireAt by MastraScheduleTriggersTable.actualFireAt
    var outcome by MastraScheduleTriggersTable.outcome
    var error by MastraScheduleTriggersTable.error
    var triggerKind by MastraScheduleTriggersTable.triggerKind
    var parentTriggerId by MastraScheduleTriggersTable.parentTriggerId
    var metadata by MastraScheduleTriggersTable.metadata
}
