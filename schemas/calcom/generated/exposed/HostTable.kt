package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object HostTable : CompositeIdTable("\"Host\"") {
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Host_userId_fkey\"",
        )
    val eventTypeId =
        reference(
            "eventTypeId",
            EventTypeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Host_eventTypeId_fkey\"",
        )
    val isFixed = bool("isFixed").default(false)
    val priority = integer("priority").nullable()
    val weight = integer("weight").nullable()
    val weightAdjustment = integer("weightAdjustment").nullable()
    val scheduleId =
        optReference(
            "scheduleId",
            ScheduleTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Host_scheduleId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val groupId =
        optReference(
            "groupId",
            HostGroupTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Host_groupId_fkey\"",
        )
    val memberId =
        optReference(
            "memberId",
            MembershipTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Host_memberId_fkey\"",
        )
    override val primaryKey = PrimaryKey(userId, eventTypeId, name = "\"Host_pkey\"")

    init {
        addIdColumn(userId)
        addIdColumn(eventTypeId)
        index("\"Host_memberId_idx\"", false, memberId)
        index("\"Host_userId_idx\"", false, userId)
        index("\"Host_eventTypeId_idx\"", false, eventTypeId)
        index("\"Host_scheduleId_idx\"", false, scheduleId)
    }
}
