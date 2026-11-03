package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object TravelScheduleTable : IdTable<Int>("\"TravelSchedule\"") {
    override val id = integer("id").autoIncrement().entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TravelSchedule_userId_fkey\"",
        )
    val timeZone = text("timeZone")
    val startDate = pgTimestamp("startDate", 3)
    val endDate = pgTimestamp("endDate", 3).nullable()
    val prevTimeZone = text("prevTimeZone").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"TravelSchedule_pkey\"")

    init {
        index("\"TravelSchedule_startDate_idx\"", false, startDate)
        index("\"TravelSchedule_endDate_idx\"", false, endDate)
    }
}
