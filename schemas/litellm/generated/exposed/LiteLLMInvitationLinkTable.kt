package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object LiteLLMInvitationLinkTable : IdTable<String>("\"LiteLLM_InvitationLink\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val userId =
        reference(
            "user_id",
            LiteLLMUserTableTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_InvitationLink_user_id_fkey\"",
        )
    val isAccepted = bool("is_accepted").default(false)
    val acceptedAt = pgTimestamp("accepted_at", 3).nullable()
    val expiresAt = pgTimestamp("expires_at", 3)
    val createdAt = pgTimestamp("created_at", 3)
    val createdBy =
        reference(
            "created_by",
            LiteLLMUserTableTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_InvitationLink_created_by_fkey\"",
        )
    val updatedAt = pgTimestamp("updated_at", 3)
    val updatedBy =
        reference(
            "updated_by",
            LiteLLMUserTableTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_InvitationLink_updated_by_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_InvitationLink_pkey\"")
}
