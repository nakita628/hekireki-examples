package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ExternalMediaStorageIntegrationTable : IdTable<String>("external_media_storage_integrations") {
    override val id =
        text("project_id")
            .references(
                ProjectTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "external_media_storage_integrations_project_id_fkey",
            ).entityId()
    val type =
        pgEnum(
            "type",
            "\"ExternalMediaStorageIntegrationType\"",
            ExternalMediaStorageIntegrationType.entries,
            ExternalMediaStorageIntegrationType::dbName,
        )
    val bucketName = text("bucket_name")
    val prefix = text("prefix").nullable()
    val authType =
        pgEnum("auth_type", "\"StorageIntegrationAuthType\"", StorageIntegrationAuthType.entries, StorageIntegrationAuthType::dbName)
            .default(StorageIntegrationAuthType.ACCESS_KEYS)
    val accessKeyId = text("access_key_id").nullable()
    val secretAccessKey = text("secret_access_key").nullable()
    val roleArn = text("role_arn").nullable()
    val externalId = text("external_id").nullable()
    val region = text("region")
    val endpoint = text("endpoint").nullable()
    val forcePathStyle = bool("force_path_style")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "external_media_storage_integrations_pkey")
}
