package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DataDbConnectionTable : IdTable<String>("data_db_connection") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val provider = pgEnum("provider", "\"DataDbProvider\"", DataDbProvider.entries, DataDbProvider::dbName).default(DataDbProvider.POSTGRES)
    val encryptedUrl = text("encrypted_url")
    val urlFingerprint = text("url_fingerprint").uniqueIndex("data_db_connection_url_fingerprint_key")
    val displayHost = text("display_host").nullable()
    val displayDatabase = text("display_database").nullable()
    val internalSchema = text("internal_schema")
    val status =
        pgEnum("status", "\"DataDbConnectionStatus\"", DataDbConnectionStatus.entries, DataDbConnectionStatus::dbName).default(
            DataDbConnectionStatus.PENDING,
        )
    val schemaVersion = text("schema_version").nullable()
    val capabilities = jsonb<String>("capabilities", { it }, { it }).nullable()
    val lastValidatedAt = pgTimestamp("last_validated_at", 3).nullable()
    val lastError = text("last_error").nullable()
    val healthState =
        pgEnum("health_state", "\"DataDbConnectionHealthState\"", DataDbConnectionHealthState.entries, DataDbConnectionHealthState::dbName)
            .default(DataDbConnectionHealthState.HEALTHY)
    val healthReason = text("health_reason").nullable()
    val healthChangedAt = pgTimestamp("health_changed_at", 3).nullable()
    val lastHealthCheckAt = pgTimestamp("last_health_check_at", 3).nullable()
    val consecutiveHealthFailures = integer("consecutive_health_failures").default(0)
    val createdBy = text("created_by")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "data_db_connection_pkey")
}
