package superproductivity.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object UserSyncStateTable : IdTable<Int>("user_sync_state") {
    override val id =
        integer("user_id")
            .references(
                UserTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "user_sync_state_user_id_fkey",
            ).entityId()
    val lastSeq = integer("last_seq").default(0)
    val lastSnapshotSeq = integer("last_snapshot_seq").nullable()
    val snapshotData = binary("snapshot_data").nullable()
    val snapshotAt = long("snapshot_at").nullable()
    val snapshotSchemaVersion = integer("snapshot_schema_version").nullable().default(1)
    val latestFullStateSeq = integer("latest_full_state_seq").nullable()
    val latestFullStateVectorClock = jsonb<String>("latest_full_state_vector_clock", { it }, { it }).nullable()
    val latestStateReplacementSeq = integer("latest_state_replacement_seq").nullable()
    override val primaryKey = PrimaryKey(id, name = "user_sync_state_pkey")
}
