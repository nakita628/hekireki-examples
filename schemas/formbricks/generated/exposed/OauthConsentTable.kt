package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object OauthConsentTable : IdTable<String>("\"oauthConsent\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val clientId =
        text("clientId").references(
            OauthClientTable.clientId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"oauthConsent_clientId_fkey\"",
        )
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"oauthConsent_userId_fkey\"",
        )
    val referenceId = text("referenceId").nullable()
    val scopes = array("scopes", TextColumnType()).nullable()
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    val resources = array("resources", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val requestedUserInfoClaims = array("requestedUserInfoClaims", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    override val primaryKey = PrimaryKey(id, name = "\"oauthConsent_pkey\"")

    init {
        index("\"oauthConsent_clientId_idx\"", false, clientId)
        index("\"oauthConsent_userId_idx\"", false, userId)
    }
}
