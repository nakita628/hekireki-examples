package calcom.models

import org.jetbrains.exposed.v1.core.IntegerColumnType
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object AvailabilityTable : IdTable<Int>("\"Availability\"") {
    override val id = integer("id").autoIncrement().entityId()
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Availability_userId_fkey\"",
        )
    val eventTypeId =
        optReference(
            "eventTypeId",
            EventTypeTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Availability_eventTypeId_fkey\"",
        )
    val days = array("days", IntegerColumnType()).nullable()
    val startTime = pgTime("startTime")
    val endTime = pgTime("endTime")
    val date = pgDate("date").nullable()
    val scheduleId =
        optReference(
            "scheduleId",
            ScheduleTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Availability_scheduleId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Availability_pkey\"")

    init {
        index("\"Availability_userId_idx\"", false, userId)
        index("\"Availability_eventTypeId_idx\"", false, eventTypeId)
        index("\"Availability_scheduleId_idx\"", false, scheduleId)
    }
}
