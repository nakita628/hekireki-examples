package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PosthogIntegrationTable : IdTable<String>("posthog_integrations") {
    override val id =
        text("project_id")
            .references(
                ProjectTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "posthog_integrations_project_id_fkey",
            ).entityId()
    val encryptedPosthogApiKey = text("encrypted_posthog_api_key")
    val posthogHostName = text("posthog_host_name")
    val lastSyncAt = pgTimestamp("last_sync_at", 3).nullable()
    val enabled = bool("enabled")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val exportSource =
        pgEnum(
            "export_source",
            "\"AnalyticsIntegrationExportSource\"",
            AnalyticsIntegrationExportSource.entries,
            AnalyticsIntegrationExportSource::dbName,
        ).default(AnalyticsIntegrationExportSource.TRACES_OBSERVATIONS)
    val backfill = bool("backfill").default(true)
    val lastError = text("last_error").nullable()
    val lastErrorAt = pgTimestamp("last_error_at", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "posthog_integrations_pkey")
}
