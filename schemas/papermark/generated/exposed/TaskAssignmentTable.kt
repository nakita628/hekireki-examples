package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskAssignmentTable : IdTable<String>("\"TaskAssignment\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val taskId =
        reference(
            "taskId",
            TaskTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskAssignment_taskId_fkey\"",
        )
    val viewerId =
        optReference(
            "viewerId",
            ViewerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskAssignment_viewerId_fkey\"",
        )
    val groupId =
        optReference(
            "groupId",
            ViewerGroupTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskAssignment_groupId_fkey\"",
        )
    val linkId =
        optReference(
            "linkId",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskAssignment_linkId_fkey\"",
        )
    val email = text("email").nullable()
    val notifiedAt = pgTimestamp("notifiedAt", 3).nullable()
    val lastReminderAt = pgTimestamp("lastReminderAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TaskAssignment_pkey\"")

    init {
        index("\"TaskAssignment_taskId_idx\"", false, taskId)
        index("\"TaskAssignment_viewerId_idx\"", false, viewerId)
        index("\"TaskAssignment_groupId_idx\"", false, groupId)
        index("\"TaskAssignment_linkId_idx\"", false, linkId)
        uniqueIndex("\"TaskAssignment_taskId_viewerId_key\"", taskId, viewerId)
        uniqueIndex("\"TaskAssignment_taskId_groupId_key\"", taskId, groupId)
        uniqueIndex("\"TaskAssignment_taskId_linkId_key\"", taskId, linkId)
        uniqueIndex("\"TaskAssignment_taskId_email_key\"", taskId, email)
    }
}
