package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object IdentifierTable : IdTable<String>("\"Identifier\"") {
    override val id = text("id").entityId()
    val userId = text("userId")
    val provider = text("provider")
    val value = text("value").nullable()
    val domain = text("domain").nullable()
    val identifierHash = text("identifierHash").nullable()
    val accountId = text("accountId").nullable()
    val providerId = text("providerId").nullable()
    val issuer = text("issuer").nullable()
    val providerAccountId = text("providerAccountId").nullable()
    val state = text("state")
    val connectionId = text("connectionId").nullable()
    val verifiedAt = pgTimestamp("verifiedAt", 3).nullable()
    val attachedAt = pgTimestamp("attachedAt", 3)
    val detachedAt = pgTimestamp("detachedAt", 3).nullable()
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Identifier_pkey\"")

    init {
        index("\"Identifier_userId_idx\"", false, userId)
        index("\"Identifier_identifierHash_idx\"", false, identifierHash)
        index("\"Identifier_value_idx\"", false, value)
        index("\"Identifier_domain_idx\"", false, domain)
        index("\"Identifier_provider_providerAccountId_idx\"", false, provider, providerAccountId)
        index("\"Identifier_issuer_providerAccountId_idx\"", false, issuer, providerAccountId)
        index("\"Identifier_accountId_idx\"", false, accountId)
    }
}
