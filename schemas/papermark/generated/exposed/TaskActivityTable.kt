package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskActivityTable : IdTable<String>("\"TaskActivity\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val taskId =
        reference(
            "taskId",
            TaskTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskActivity_taskId_fkey\"",
        )
    val type = text("type")
    val comment = text("comment").nullable()
    val fromStatus = text("fromStatus").nullable()
    val toStatus = text("toStatus").nullable()
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskActivity_userId_fkey\"",
        )
    val viewerId =
        optReference(
            "viewerId",
            ViewerTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskActivity_viewerId_fkey\"",
        )
    val viewId =
        optReference(
            "viewId",
            ViewTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskActivity_viewId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TaskActivity_pkey\"")

    init {
        index("\"TaskActivity_taskId_idx\"", false, taskId)
        index("\"TaskActivity_userId_idx\"", false, userId)
        index("\"TaskActivity_viewerId_idx\"", false, viewerId)
        index("\"TaskActivity_viewId_idx\"", false, viewId)
        index("\"TaskActivity_taskId_createdAt_idx\"", false, taskId, createdAt)
    }
}
