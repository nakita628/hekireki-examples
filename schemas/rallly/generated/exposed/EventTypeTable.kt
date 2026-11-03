package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EventTypeTable : IdTable<String>("event_types") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val spaceId =
        reference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "event_types_space_id_fkey",
        )
    val hostId =
        reference(
            "host_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "event_types_host_id_fkey",
        )
    val name = text("name")
    val duration = integer("duration_minutes")
    val capacity = integer("capacity").nullable()
    val description = text("description").nullable()
    val location = jsonb<String>("location", { it }, { it }).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deleted = bool("deleted").default(false)
    val deletedAt = pgTimestamp("deleted_at", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "event_types_pkey")

    init {
        index("event_types_space_id_idx", false, spaceId)
        index("event_types_host_id_idx", false, hostId)
    }
}
