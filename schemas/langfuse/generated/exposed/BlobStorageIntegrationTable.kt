package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BlobStorageIntegrationTable : IdTable<String>("blob_storage_integrations") {
    override val id =
        text("project_id")
            .references(
                ProjectTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "blob_storage_integrations_project_id_fkey",
            ).entityId()
    val type = pgEnum("type", "\"BlobStorageIntegrationType\"", BlobStorageIntegrationType.entries, BlobStorageIntegrationType::dbName)
    val bucketName = text("bucket_name")
    val prefix = text("prefix")
    val accessKeyId = text("access_key_id").nullable()
    val secretAccessKey = text("secret_access_key").nullable()
    val region = text("region")
    val endpoint = text("endpoint").nullable()
    val forcePathStyle = bool("force_path_style")
    val nextSyncAt = pgTimestamp("next_sync_at", 3).nullable()
    val lastSyncAt = pgTimestamp("last_sync_at", 3).nullable()
    val enabled = bool("enabled")
    val exportFrequency = text("export_frequency")
    val fileType =
        pgEnum(
            "file_type",
            "\"BlobStorageIntegrationFileType\"",
            BlobStorageIntegrationFileType.entries,
            BlobStorageIntegrationFileType::dbName,
        ).default(BlobStorageIntegrationFileType.CSV)
    val exportMode =
        pgEnum("export_mode", "\"BlobStorageExportMode\"", BlobStorageExportMode.entries, BlobStorageExportMode::dbName).default(
            BlobStorageExportMode.FULL_HISTORY,
        )
    val exportStartDate = pgTimestamp("export_start_date", 3).nullable()
    val exportSource =
        pgEnum(
            "export_source",
            "\"AnalyticsIntegrationExportSource\"",
            AnalyticsIntegrationExportSource.entries,
            AnalyticsIntegrationExportSource::dbName,
        ).default(AnalyticsIntegrationExportSource.TRACES_OBSERVATIONS)
    val exportFieldGroups =
        array("export_field_groups", TextColumnType())
            .nullable()
            .databaseDefault(
                "'{\"core\",\"basic\",\"time\",\"io\",\"metadata\",\"model\",\"usage\",\"prompt\",\"metrics\",\"tools\",\"trace_context\"}'",
            ) { listOf("core", "basic", "time", "io", "metadata", "model", "usage", "prompt", "metrics", "tools", "trace_context") }
    val compressed = bool("compressed").default(true)
    val exportTuning = jsonb<String>("export_tuning", { it }, { it }).nullable()
    val runStartedAt = pgTimestamp("run_started_at", 3).nullable()
    val backfill = bool("backfill").default(true)
    val lastError = text("last_error").nullable()
    val lastErrorAt = pgTimestamp("last_error_at", 3).nullable()
    val lastFailureNotificationSentAt = pgTimestamp("last_failure_notification_sent_at", 3).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "blob_storage_integrations_pkey")
}
