package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SlackIntegrationTable : IdTable<String>("\"SlackIntegration\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val name = text("name")
    val kind = pgEnum("kind", "\"SlackIntegrationKind\"", SlackIntegrationKind.entries, SlackIntegrationKind::dbName)
    val scopeType =
        pgEnum("scopeType", "\"SlackIntegrationScopeType\"", SlackIntegrationScopeType.entries, SlackIntegrationScopeType::dbName)
    val scopeId = text("scopeId")
    val organizationId = text("organizationId")
    val botTokenEncrypted = text("botTokenEncrypted").nullable()
    val webhookUrlEncrypted = text("webhookUrlEncrypted").nullable()
    val secretFingerprint = text("secretFingerprint")
    val secretHint = text("secretHint")
    val slackTeamId = text("slackTeamId").nullable()
    val slackTeamName = text("slackTeamName").nullable()
    val createdById = text("createdById")
    val updatedById = text("updatedById")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SlackIntegration_pkey\"")

    init {
        index("\"SlackIntegration_scopeType_scopeId_idx\"", false, scopeType, scopeId)
        uniqueIndex(
            "SlackIntegration_organizationId_scopeType_scopeId_secretFin_key",
            organizationId,
            scopeType,
            scopeId,
            secretFingerprint,
        )
    }
}
