package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object OauthClientTable : IdTable<String>("\"oauthClient\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val clientId = text("clientId").uniqueIndex("\"oauthClient_clientId_key\"")
    val clientSecret = text("clientSecret").nullable()
    val disabled = bool("disabled").nullable().default(false)
    val skipConsent = bool("skipConsent").nullable()
    val enableEndSession = bool("enableEndSession").nullable()
    val subjectType = text("subjectType").nullable()
    val scopes = array("scopes", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"oauthClient_userId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).nullable()
    val name = text("name").nullable()
    val uri = text("uri").nullable()
    val icon = text("icon").nullable()
    val contacts = array("contacts", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val tos = text("tos").nullable()
    val policy = text("policy").nullable()
    val softwareId = text("softwareId").nullable()
    val softwareVersion = text("softwareVersion").nullable()
    val softwareStatement = text("softwareStatement").nullable()
    val redirectUris = array("redirectUris", TextColumnType()).nullable()
    val postLogoutRedirectUris = array("postLogoutRedirectUris", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val tokenEndpointAuthMethod = text("tokenEndpointAuthMethod").nullable()
    val grantTypes = array("grantTypes", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val responseTypes = array("responseTypes", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val public = bool("public").nullable()
    val type = text("type").nullable()
    val requirePKCE = bool("requirePKCE").nullable()
    val referenceId = text("referenceId").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val clientDiscoveryId = text("clientDiscoveryId").nullable()
    val clientCredentialsScopes = array("clientCredentialsScopes", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val backchannelLogoutUri = text("backchannelLogoutUri").nullable()
    val backchannelLogoutSessionRequired = bool("backchannelLogoutSessionRequired").nullable()
    val applicationType = text("applicationType").nullable()
    val jwks = text("jwks").nullable()
    val jwksUri = text("jwksUri").nullable()
    val dpopBoundAccessTokens = bool("dpopBoundAccessTokens").nullable().default(false)
    override val primaryKey = PrimaryKey(id, name = "\"oauthClient_pkey\"")

    init {
        index("\"oauthClient_userId_idx\"", false, userId)
    }
}
