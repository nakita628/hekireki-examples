package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MeetingBriefingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MeetingBriefingEntity>(MeetingBriefingTable)

    var createdAt by MeetingBriefingTable.createdAt
    var calendarEventId by MeetingBriefingTable.calendarEventId
    var eventTitle by MeetingBriefingTable.eventTitle
    var eventStartTime by MeetingBriefingTable.eventStartTime
    var guestCount by MeetingBriefingTable.guestCount
    var status by MeetingBriefingTable.status
    var emailAccountId by MeetingBriefingTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn MeetingBriefingTable.emailAccountId
}
