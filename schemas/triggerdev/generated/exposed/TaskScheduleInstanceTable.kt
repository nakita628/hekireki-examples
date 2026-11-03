package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskScheduleInstanceTable : IdTable<String>("\"TaskScheduleInstance\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val taskScheduleId =
        reference(
            "taskScheduleId",
            TaskScheduleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskScheduleInstance_taskScheduleId_fkey\"",
        )
    val environmentId =
        reference(
            "environmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskScheduleInstance_environmentId_fkey\"",
        )
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskScheduleInstance_projectId_fkey\"",
        )
    val schedulePhase = integer("schedulePhase").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val active = bool("active").default(true)
    val lastScheduledTimestamp = pgTimestamp("lastScheduledTimestamp", 3).nullable()
    val nextScheduledTimestamp = pgTimestamp("nextScheduledTimestamp", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"TaskScheduleInstance_pkey\"")

    init {
        index("\"TaskScheduleInstance_environmentId_idx\"", false, environmentId)
        index("\"TaskScheduleInstance_projectId_active_idx\"", false, projectId, active)
        uniqueIndex("\"TaskScheduleInstance_taskScheduleId_environmentId_key\"", taskScheduleId, environmentId)
    }
}
