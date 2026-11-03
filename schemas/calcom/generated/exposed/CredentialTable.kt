package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object CredentialTable : IdTable<Int>("\"Credential\"") {
    override val id = integer("id").autoIncrement().entityId()
    val type = text("type")
    val key = jsonb<String>("key", { it }, { it })
    val encryptedKey = text("encryptedKey").nullable()
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Credential_userId_fkey\"",
        )
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Credential_teamId_fkey\"",
        )
    val appId =
        optReference(
            "appId",
            AppTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Credential_appId_fkey\"",
        )
    val subscriptionId = text("subscriptionId").nullable()
    val paymentStatus = text("paymentStatus").nullable()
    val billingCycleStart = integer("billingCycleStart").nullable()
    val invalid = bool("invalid").nullable().default(false)
    val delegationCredentialId =
        optReference(
            "delegationCredentialId",
            DelegationCredentialTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Credential_delegationCredentialId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Credential_pkey\"")

    init {
        index("\"Credential_appId_idx\"", false, appId)
        index("\"Credential_subscriptionId_idx\"", false, subscriptionId)
        index("\"Credential_invalid_idx\"", false, invalid)
        index("\"Credential_userId_delegationCredentialId_idx\"", false, userId, delegationCredentialId)
        index("\"Credential_teamId_idx\"", false, teamId)
    }
}
