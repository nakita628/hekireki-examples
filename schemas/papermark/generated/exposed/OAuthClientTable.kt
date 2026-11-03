package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OAuthClientTable : IdTable<String>("\"OAuthClient\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val clientId = text("clientId").uniqueIndex("\"OAuthClient_clientId_key\"")
    val clientName = text("clientName")
    val clientUri = text("clientUri").nullable()
    val logoUri = text("logoUri").nullable()
    val redirectUris = array("redirectUris", TextColumnType()).nullable()
    val grantTypes = array("grantTypes", TextColumnType()).nullable()
    val scopes = text("scopes")
    val tokenAuthMethod = text("tokenAuthMethod")
    val hashedSecret = text("hashedSecret").nullable()
    val registrationType = text("registrationType")
    val metadataUrl = text("metadataUrl").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OAuthClient_pkey\"")

    init {
        index("\"OAuthClient_clientId_idx\"", false, clientId)
    }
}
