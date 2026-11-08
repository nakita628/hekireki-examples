package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object TemplateTable : IdTable<String>("templates") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val description = text("description").nullable()
    val subject = text("subject")
    val body = text("body")
    val from = text("from")
    val fromName = text("fromName").nullable()
    val replyTo = text("replyTo").nullable()
    val type = pgEnum("type", "\"TemplateType\"", TemplateType.entries, TemplateType::dbName).default(TemplateType.MARKETING)
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"templates_projectId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "templates_pkey")

    init {
        index("\"templates_projectId_idx\"", false, projectId)
        index("\"templates_projectId_type_idx\"", false, projectId, type)
    }
}
