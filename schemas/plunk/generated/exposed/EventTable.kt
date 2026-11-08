package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object EventTable : IdTable<String>("events") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val data = jsonb<String>("data", { it }, { it }).nullable()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"events_projectId_fkey\"",
        )
    val contactId =
        optReference(
            "contactId",
            ContactTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"events_contactId_fkey\"",
        )
    val emailId =
        optReference(
            "emailId",
            EmailTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"events_emailId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "events_pkey")

    init {
        index("\"events_projectId_name_idx\"", false, projectId, name)
        index("\"events_contactId_idx\"", false, contactId)
        index("\"events_emailId_idx\"", false, emailId)
        index("\"events_createdAt_idx\"", false, createdAt)
        index("\"events_projectId_contactId_name_createdAt_idx\"", false, projectId, contactId, name, createdAt)
        index("\"events_contactId_name_createdAt_idx\"", false, contactId, name, createdAt)
        index("\"events_projectId_name_createdAt_idx\"", false, projectId, name, createdAt)
        index("\"events_contactId_createdAt_idx\"", false, contactId, createdAt)
        index("events_data_idx", false, functions = listOf(indexExpression("\"data\" jsonb_ops")), indexType = "gin")
    }
}
