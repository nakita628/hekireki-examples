package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object OauthConsentTable : IdTable<String>("\"OauthConsent\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val clientId =
        text("clientId").references(
            OauthClientTable.clientId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OauthConsent_clientId_fkey\"",
        )
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OauthConsent_userId_fkey\"",
        )
    val referenceId = text("referenceId").nullable()
    val resources = array("resources", TextColumnType()).nullable()
    val requestedUserInfoClaims = array("requestedUserInfoClaims", TextColumnType()).nullable()
    val scopes = array("scopes", TextColumnType()).nullable()
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"OauthConsent_pkey\"")

    init {
        index("\"OauthConsent_userId_idx\"", false, userId)
        uniqueIndex("\"OauthConsent_clientId_userId_key\"", clientId, userId)
    }
}
