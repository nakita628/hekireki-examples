package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object SelectedSlotsTable : IdTable<Int>("\"SelectedSlots\"") {
    override val id = integer("id").autoIncrement().entityId()
    val eventTypeId = integer("eventTypeId")
    val userId = integer("userId")
    val slotUtcStartDate = pgTimestamp("slotUtcStartDate", 3)
    val slotUtcEndDate = pgTimestamp("slotUtcEndDate", 3)
    val uid = text("uid")
    val releaseAt = pgTimestamp("releaseAt", 3)
    val isSeat = bool("isSeat").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"SelectedSlots_pkey\"")

    init {
        uniqueIndex("\"SelectedSlots_userId_slotUtcStartDate_slotUtcEndDate_uid_key\"", userId, slotUtcStartDate, slotUtcEndDate, uid)
    }
}
