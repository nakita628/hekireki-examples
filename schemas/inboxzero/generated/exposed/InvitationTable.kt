package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object InvitationTable : IdTable<String>("invitation") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"invitation_organizationId_fkey\"",
        )
    val email = text("email")
    val role = text("role").nullable()
    val status = text("status")
    val expiresAt = pgTimestamp("expiresAt", 3)
    val inviterId =
        reference(
            "inviterId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"invitation_inviterId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "invitation_pkey")

    init {
        index("\"invitation_organizationId_idx\"", false, organizationId)
        index("\"invitation_inviterId_idx\"", false, inviterId)
    }
}
