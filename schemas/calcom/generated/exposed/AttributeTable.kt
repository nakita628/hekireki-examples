package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AttributeTable : IdTable<String>("\"Attribute\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Attribute_teamId_fkey\"",
        )
    val type = pgEnum("type", "\"AttributeType\"", AttributeType.entries, AttributeType::dbName)
    val name = text("name")
    val slug = text("slug")
    val enabled = bool("enabled").default(true)
    val usersCanEditRelation = bool("usersCanEditRelation").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val isWeightsEnabled = bool("isWeightsEnabled").default(false)
    val isLocked = bool("isLocked").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"Attribute_pkey\"")

    init {
        index("\"Attribute_teamId_idx\"", false, teamId)
        uniqueIndex("\"Attribute_teamId_slug_key\"", teamId, slug)
    }
}
