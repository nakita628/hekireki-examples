package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class BookingDenormalizedEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, BookingDenormalizedEntity>(BookingDenormalizedTable)

    var uid by BookingDenormalizedTable.uid
    var eventTypeId by BookingDenormalizedTable.eventTypeId
    var title by BookingDenormalizedTable.title
    var description by BookingDenormalizedTable.description
    var startTime by BookingDenormalizedTable.startTime
    var endTime by BookingDenormalizedTable.endTime
    var createdAt by BookingDenormalizedTable.createdAt
    var updatedAt by BookingDenormalizedTable.updatedAt
    var location by BookingDenormalizedTable.location
    var paid by BookingDenormalizedTable.paid
    var status by BookingDenormalizedTable.status
    var rescheduled by BookingDenormalizedTable.rescheduled
    var userId by BookingDenormalizedTable.userId
    var teamId by BookingDenormalizedTable.teamId
    var eventLength by BookingDenormalizedTable.eventLength
    var eventParentId by BookingDenormalizedTable.eventParentId
    var userEmail by BookingDenormalizedTable.userEmail
    var userName by BookingDenormalizedTable.userName
    var userUsername by BookingDenormalizedTable.userUsername
    var ratingFeedback by BookingDenormalizedTable.ratingFeedback
    var rating by BookingDenormalizedTable.rating
    var noShowHost by BookingDenormalizedTable.noShowHost
    var isTeamBooking by BookingDenormalizedTable.isTeamBooking
}
