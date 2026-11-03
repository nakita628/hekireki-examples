package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InvitationRecordTable : IdTable<String>("invitation_record") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val invitationId = text("invitation_id")
    val baseId = text("base_id").nullable()
    val spaceId = text("space_id").nullable()
    val type = text("type")
    val inviter = text("inviter")
    val accepter = text("accepter")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "invitation_record_pkey")

    init {
        index("invitation_record_invitation_id_idx", false, invitationId)
        index("invitation_record_base_id_idx", false, baseId)
        index("invitation_record_space_id_idx", false, spaceId)
    }
}
