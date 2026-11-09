package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OidcProviderTable : IdTable<String>("\"OidcProvider\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val issuer = text("issuer")
    val clientId = text("clientId")
    val clientSecret = text("clientSecret")
    val authorizationEndpoint = text("authorizationEndpoint").nullable()
    val tokenEndpoint = text("tokenEndpoint").nullable()
    val userinfoEndpoint = text("userinfoEndpoint").nullable()
    val jwksUri = text("jwksUri").nullable()
    val introspectionEndpoint = text("introspectionEndpoint").nullable()
    val scopes =
        array("scopes", TextColumnType())
            .nullable()
            .databaseDefault("'{\"openid\",\"profile\",\"email\",\"offline_access\"}'") {
                listOf("openid", "profile", "email", "offline_access")
            }
    val audience = text("audience").nullable()
    val prompt = text("prompt").nullable()
    val usernameClaim = text("usernameClaim").default("email")
    val emailClaim = text("emailClaim").default("email")
    val nameClaim = text("nameClaim").default("name")
    val groupClaim = text("groupClaim").default("groups")
    val enabled = bool("enabled").default(true)
    val autoDiscovery = bool("autoDiscovery").default(true)
    override val primaryKey = PrimaryKey(id, name = "\"OidcProvider_pkey\"")

    init {
        uniqueIndex("\"OidcProvider_issuer_clientId_key\"", issuer, clientId)
    }
}
