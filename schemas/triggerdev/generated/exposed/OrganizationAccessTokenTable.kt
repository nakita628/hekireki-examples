package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationAccessTokenTable : IdTable<String>("\"OrganizationAccessToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val type =
        pgEnum("type", "\"OrganizationAccessTokenType\"", OrganizationAccessTokenType.entries, OrganizationAccessTokenType::dbName).default(
            OrganizationAccessTokenType.USER,
        )
    val hashedToken = text("hashedToken").uniqueIndex("\"OrganizationAccessToken_hashedToken_key\"")
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganizationAccessToken_organizationId_fkey\"",
        )
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val lastAccessedAt = pgTimestamp("lastAccessedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationAccessToken_pkey\"")

    init {
        index(
            "\"OrganizationAccessToken_organizationId_type_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"organizationId\""), indexExpression("\"type\""), indexExpression("\"createdAt\" DESC")),
        )
    }
}
