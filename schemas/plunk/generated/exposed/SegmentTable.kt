package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SegmentTable : IdTable<String>("segments") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val description = text("description").nullable()
    val type = pgEnum("type", "\"SegmentType\"", SegmentType.entries, SegmentType::dbName).default(SegmentType.DYNAMIC)
    val condition = jsonb<String>("condition", { it }, { it }).nullable()
    val trackMembership = bool("trackMembership").default(false)
    val memberCount = integer("memberCount").default(0)
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"segments_projectId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "segments_pkey")

    init {
        index("\"segments_projectId_idx\"", false, projectId)
        index("segments_condition_idx", false, functions = listOf(indexExpression("\"condition\" jsonb_ops")), indexType = "gin")
    }
}
