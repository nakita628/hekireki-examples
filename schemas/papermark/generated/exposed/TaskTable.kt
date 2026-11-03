package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskTable : IdTable<String>("\"Task\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val title = text("title")
    val description = text("description").nullable()
    val type = text("type").default("TODO")
    val status = text("status").default("OPEN")
    val category = text("category").nullable()
    val dueDate = pgTimestamp("dueDate", 3).nullable()
    val orderIndex = integer("orderIndex").nullable()
    val taskListId =
        reference(
            "taskListId",
            TaskListTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Task_taskListId_fkey\"",
        )
    val dataroomId =
        reference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Task_dataroomId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Task_teamId_fkey\"",
        )
    val uploadFolderId =
        optReference(
            "uploadFolderId",
            DataroomFolderTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Task_uploadFolderId_fkey\"",
        )
    val createdByUserId =
        optReference(
            "createdByUserId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Task_createdByUserId_fkey\"",
        )
    val lastActivityNotifiedAt = pgTimestamp("lastActivityNotifiedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Task_pkey\"")

    init {
        index("\"Task_taskListId_idx\"", false, taskListId)
        index("\"Task_dataroomId_idx\"", false, dataroomId)
        index("\"Task_teamId_idx\"", false, teamId)
        index("\"Task_uploadFolderId_idx\"", false, uploadFolderId)
        index("\"Task_taskListId_orderIndex_idx\"", false, taskListId, orderIndex)
    }
}
