package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ViewerInvitationTable : IdTable<String>("\"ViewerInvitation\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val viewerId =
        reference(
            "viewerId",
            ViewerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ViewerInvitation_viewerId_fkey\"",
        )
    val linkId =
        reference(
            "linkId",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ViewerInvitation_linkId_fkey\"",
        )
    val groupId =
        optReference(
            "groupId",
            ViewerGroupTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ViewerInvitation_groupId_fkey\"",
        )
    val invitedBy = text("invitedBy")
    val customMessage = text("customMessage").nullable()
    val sentAt = pgTimestamp("sentAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val status = pgEnum("status", "\"InvitationStatus\"", InvitationStatus.entries, InvitationStatus::dbName).default(InvitationStatus.SENT)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ViewerInvitation_pkey\"")

    init {
        index("\"ViewerInvitation_viewerId_idx\"", false, viewerId)
        index("\"ViewerInvitation_linkId_idx\"", false, linkId)
        index("\"ViewerInvitation_groupId_idx\"", false, groupId)
    }
}
