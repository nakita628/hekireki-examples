package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MixpanelIntegrationTable : IdTable<String>("mixpanel_integrations") {
    override val id =
        text("project_id")
            .references(
                ProjectTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "mixpanel_integrations_project_id_fkey",
            ).entityId()
    val encryptedMixpanelProjectToken = text("encrypted_mixpanel_project_token")
    val mixpanelRegion = text("mixpanel_region")
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
    override val primaryKey = PrimaryKey(id, name = "mixpanel_integrations_pkey")
}
