package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InvitationTable : IdTable<String>("invitation") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val baseId = text("base_id").nullable()
    val spaceId = text("space_id").nullable()
    val type = text("type")
    val role = text("role")
    val invitationCode = text("invitation_code")
    val expiredTime = pgTimestamp("expired_time", 3).nullable()
    val createdBy = text("create_by")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedBy = text("last_modified_by").nullable()
    val deletedTime = pgTimestamp("deleted_time", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "invitation_pkey")

    init {
        index("invitation_base_id_idx", false, baseId)
        index("invitation_space_id_idx", false, spaceId)
    }
}
