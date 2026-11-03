package rallly.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SpaceMemberInviteTable : IdTable<String>("space_member_invites") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val spaceId =
        reference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "space_member_invites_space_id_fkey",
        )
    val email = text("email")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val role = pgEnum("role", "\"SpaceMemberRole\"", SpaceMemberRole.entries, SpaceMemberRole::dbName).default(SpaceMemberRole.MEMBER)
    val inviterId =
        reference(
            "inviter_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "space_member_invites_inviter_id_fkey",
        )
    override val primaryKey = PrimaryKey(id, name = "space_member_invites_pkey")

    init {
        uniqueIndex("space_member_invites_space_id_email_key", spaceId, email)
    }
}
