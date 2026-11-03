package rallly.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SpaceMemberTable : IdTable<String>("space_members") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val spaceId =
        reference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "space_members_space_id_fkey",
        )
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "space_members_user_id_fkey",
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val role = pgEnum("role", "\"SpaceMemberRole\"", SpaceMemberRole.entries, SpaceMemberRole::dbName).default(SpaceMemberRole.MEMBER)
    val lastSelectedAt =
        pgTimestamp("last_selected_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "space_members_pkey")

    init {
        index("space_members_user_id_idx", false, userId)
        uniqueIndex("space_members_space_id_user_id_key", spaceId, userId)
    }
}
