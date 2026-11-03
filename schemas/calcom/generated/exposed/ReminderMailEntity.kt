package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ReminderMailEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, ReminderMailEntity>(ReminderMailTable)

    var referenceId by ReminderMailTable.referenceId
    var reminderType by ReminderMailTable.reminderType
    var elapsedMinutes by ReminderMailTable.elapsedMinutes
    var createdAt by ReminderMailTable.createdAt
}
