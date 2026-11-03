package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BaseDataDbMoveJobTable : IdTable<String>("base_data_db_move_job") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val baseId =
        reference(
            "base_id",
            BaseTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "base_data_db_move_job_base_id_fkey",
        )
    val sourceSpaceId =
        reference(
            "source_space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "base_data_db_move_job_source_space_id_fkey",
        )
    val targetSpaceId =
        reference(
            "target_space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "base_data_db_move_job_target_space_id_fkey",
        )
    val sourceConnectionId = text("source_connection_id").nullable()
    val targetConnectionId = text("target_connection_id").nullable()
    val state =
        pgEnum("state", "\"BaseDataDbMoveJobState\"", BaseDataDbMoveJobState.entries, BaseDataDbMoveJobState::dbName).default(
            BaseDataDbMoveJobState.PENDING,
        )
    val inventory = jsonb<String>("inventory", { it }, { it }).nullable()
    val copyStats = jsonb<String>("copy_stats", { it }, { it }).nullable()
    val validationStats = jsonb<String>("validation_stats", { it }, { it }).nullable()
    val lastError = text("last_error").nullable()
    val startedAt = pgTimestamp("started_at", 3).nullable()
    val completedAt = pgTimestamp("completed_at", 3).nullable()
    val createdBy = text("created_by")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "base_data_db_move_job_pkey")

    init {
        index("base_data_db_move_job_base_id_state_idx", false, baseId, state)
        index("base_data_db_move_job_state_idx", false, state)
        index("base_data_db_move_job_source_space_id_state_idx", false, sourceSpaceId, state)
        index("base_data_db_move_job_target_space_id_state_idx", false, targetSpaceId, state)
    }
}
