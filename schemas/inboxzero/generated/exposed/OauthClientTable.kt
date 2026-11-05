package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object OauthClientTable : IdTable<String>("\"OauthClient\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val clientId = text("clientId").uniqueIndex("\"OauthClient_clientId_key\"")
    val clientSecret = text("clientSecret").nullable()
    val clientDiscoveryId = text("clientDiscoveryId").nullable()
    val disabled = bool("disabled").nullable().default(false)
    val skipConsent = bool("skipConsent").nullable()
    val enableEndSession = bool("enableEndSession").nullable()
    val subjectType = text("subjectType").nullable()
    val scopes = array("scopes", TextColumnType()).nullable()
    val clientCredentialsScopes = array("clientCredentialsScopes", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OauthClient_userId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).nullable()
    val name = text("name").nullable()
    val uri = text("uri").nullable()
    val icon = text("icon").nullable()
    val contacts = array("contacts", TextColumnType()).nullable()
    val tos = text("tos").nullable()
    val policy = text("policy").nullable()
    val softwareId = text("softwareId").nullable()
    val softwareVersion = text("softwareVersion").nullable()
    val softwareStatement = text("softwareStatement").nullable()
    val redirectUris = array("redirectUris", TextColumnType()).nullable()
    val postLogoutRedirectUris = array("postLogoutRedirectUris", TextColumnType()).nullable()
    val backchannelLogoutUri = text("backchannelLogoutUri").nullable()
    val backchannelLogoutSessionRequired = bool("backchannelLogoutSessionRequired").nullable()
    val tokenEndpointAuthMethod = text("tokenEndpointAuthMethod").nullable()
    val applicationType = text("applicationType").nullable()
    val jwks = text("jwks").nullable()
    val jwksUri = text("jwksUri").nullable()
    val grantTypes = array("grantTypes", TextColumnType()).nullable()
    val responseTypes = array("responseTypes", TextColumnType()).nullable()
    val requirePKCE = bool("requirePKCE").nullable()
    val dpopBoundAccessTokens = bool("dpopBoundAccessTokens").nullable().default(false)
    val referenceId = text("referenceId").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"OauthClient_pkey\"")

    init {
        index("\"OauthClient_userId_idx\"", false, userId)
    }
}
