package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SpaceDataDbMigrationJobTable : IdTable<String>("space_data_db_migration_job") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val spaceId =
        reference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "space_data_db_migration_job_space_id_fkey",
        )
    val sourceConnectionId =
        optReference(
            "source_connection_id",
            DataDbConnectionTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "space_data_db_migration_job_source_connection_id_fkey",
        )
    val targetConnectionId =
        optReference(
            "target_connection_id",
            DataDbConnectionTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "space_data_db_migration_job_target_connection_id_fkey",
        )
    val targetMode = text("target_mode").default("migrate-space")
    val switchOnCompletion = bool("switch_on_completion").default(false)
    val state =
        pgEnum("state", "\"SpaceDataDbMigrationJobState\"", SpaceDataDbMigrationJobState.entries, SpaceDataDbMigrationJobState::dbName)
            .default(SpaceDataDbMigrationJobState.PENDING)
    val targetUrlFingerprint = text("target_url_fingerprint")
    val targetInternalSchema = text("target_internal_schema")
    val inventory = jsonb<String>("inventory", { it }, { it }).nullable()
    val copyStats = jsonb<String>("copy_stats", { it }, { it }).nullable()
    val validationStats = jsonb<String>("validation_stats", { it }, { it }).nullable()
    val lastError = text("last_error").nullable()
    val startedAt = pgTimestamp("started_at", 3).nullable()
    val completedAt = pgTimestamp("completed_at", 3).nullable()
    val createdBy = text("created_by")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "space_data_db_migration_job_pkey")

    init {
        index("space_data_db_migration_job_space_id_state_idx", false, spaceId, state)
        index("space_data_db_migration_job_source_connection_id_idx", false, sourceConnectionId)
        index("space_data_db_migration_job_target_connection_id_idx", false, targetConnectionId)
    }
}
