package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SpaceDataDbBindingTable : IdTable<String>("space_data_db_binding") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val spaceId =
        reference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "space_data_db_binding_space_id_fkey",
        ).uniqueIndex("space_data_db_binding_space_id_key")
    val dataDbConnectionId =
        optReference(
            "data_db_connection_id",
            DataDbConnectionTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "space_data_db_binding_data_db_connection_id_fkey",
        )
    val mode =
        pgEnum("mode", "\"SpaceDataDbBindingMode\"", SpaceDataDbBindingMode.entries, SpaceDataDbBindingMode::dbName).default(
            SpaceDataDbBindingMode.DEFAULT,
        )
    val state =
        pgEnum("state", "\"SpaceDataDbBindingState\"", SpaceDataDbBindingState.entries, SpaceDataDbBindingState::dbName).default(
            SpaceDataDbBindingState.READY,
        )
    val createdBy = text("created_by")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastHistoryFlushedAt = pgTimestamp("last_history_flushed_at", 3).nullable()
    val lastRemovalFlushedAt = pgTimestamp("last_removal_flushed_at", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "space_data_db_binding_pkey")

    init {
        index("space_data_db_binding_data_db_connection_id_idx", false, dataDbConnectionId)
    }
}
