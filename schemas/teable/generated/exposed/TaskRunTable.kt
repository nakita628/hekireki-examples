package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskRunTable : IdTable<String>("task_run") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val taskId =
        reference(
            "task_id",
            TaskTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "task_run_task_id_fkey",
        )
    val baseId = text("base_id")
    val status = text("status")
    val snapshot = text("snapshot")
    val dependsOnRunIds = array("depends_on_run_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val spent = integer("spent").nullable()
    val log = text("log").nullable()
    val errorMsg = text("error_msg").nullable()
    val startedTime = pgTimestamp("started_time", 3).nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "task_run_pkey")

    init {
        index("task_run_task_id_status_idx", false, taskId, status)
        index("task_run_status_base_id_created_time_idx", false, status, baseId, createdTime)
        index("task_run_status_last_modified_time_idx", false, status, lastModifiedTime)
    }
}
