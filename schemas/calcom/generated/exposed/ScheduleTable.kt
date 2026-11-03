package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ScheduleTable : IdTable<Int>("\"Schedule\"") {
    override val id = integer("id").autoIncrement().entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Schedule_userId_fkey\"",
        )
    val name = text("name")
    val timeZone = text("timeZone").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Schedule_pkey\"")

    init {
        index("\"Schedule_userId_idx\"", false, userId)
    }
}
