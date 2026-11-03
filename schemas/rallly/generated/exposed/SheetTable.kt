package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SheetTable : IdTable<String>("sheets") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val spaceId =
        reference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "sheets_space_id_fkey",
        )
    val hostId =
        reference(
            "host_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "sheets_host_id_fkey",
        )
    val title = text("title")
    val description = text("description").nullable()
    val urlId = text("url_id").uniqueIndex("sheets_url_id_key")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deleted = bool("deleted").default(false)
    val deletedAt = pgTimestamp("deleted_at", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "sheets_pkey")

    init {
        index("sheets_space_id_idx", false, spaceId)
        index("sheets_host_id_idx", false, hostId)
    }
}
