package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object BookingDenormalizedTable : IdTable<Int>("\"BookingDenormalized\"") {
    override val id = integer("id").entityId()
    val uid = text("uid")
    val eventTypeId = integer("eventTypeId").nullable()
    val title = text("title")
    val description = text("description").nullable()
    val startTime = pgTimestamp("startTime", 3)
    val endTime = pgTimestamp("endTime", 3)
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3).nullable()
    val location = text("location").nullable()
    val paid = bool("paid")
    val status = pgEnum("status", "\"BookingStatus\"", BookingStatus.entries, BookingStatus::dbName)
    val rescheduled = bool("rescheduled").nullable()
    val userId = integer("userId").nullable()
    val teamId = integer("teamId").nullable()
    val eventLength = integer("eventLength").nullable()
    val eventParentId = integer("eventParentId").nullable()
    val userEmail = text("userEmail").nullable()
    val userName = text("userName").nullable()
    val userUsername = text("userUsername").nullable()
    val ratingFeedback = text("ratingFeedback").nullable()
    val rating = integer("rating").nullable()
    val noShowHost = bool("noShowHost").nullable()
    val isTeamBooking = bool("isTeamBooking")
    override val primaryKey = PrimaryKey(id, name = "\"BookingDenormalized_pkey\"")

    init {
        uniqueIndex("\"BookingDenormalized_id_key\"", id)
        index("\"BookingDenormalized_userId_idx\"", false, userId)
        index("\"BookingDenormalized_createdAt_idx\"", false, createdAt)
        index("\"BookingDenormalized_eventTypeId_idx\"", false, eventTypeId)
        index("\"BookingDenormalized_eventParentId_idx\"", false, eventParentId)
        index("\"BookingDenormalized_teamId_idx\"", false, teamId)
        index("\"BookingDenormalized_startTime_idx\"", false, startTime)
        index("\"BookingDenormalized_endTime_idx\"", false, endTime)
        index("\"BookingDenormalized_status_idx\"", false, status)
        index("\"BookingDenormalized_teamId_isTeamBooking_idx\"", false, teamId, isTeamBooking)
        index("\"BookingDenormalized_userId_isTeamBooking_idx\"", false, userId, isTeamBooking)
        index("\"BookingDenormalized_startTime_endTime_idx\"", false, startTime, endTime)
    }
}
