package superproductivity.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object OperationTable : IdTable<String>("operations") {
    override val id = text("id").entityId()
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "operations_user_id_fkey",
        )
    val clientId = text("client_id")
    val serverSeq = integer("server_seq")
    val actionType = text("action_type")
    val opType = text("op_type")
    val entityType = text("entity_type")
    val entityId = text("entity_id").nullable()
    val entityIds = array("entity_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val payload = jsonb<String>("payload", { it }, { it })
    val payloadBytes = long("payload_bytes").default(0L)
    val vectorClock = jsonb<String>("vector_clock", { it }, { it })
    val schemaVersion = integer("schema_version")
    val clientTimestamp = long("client_timestamp")
    val receivedAt = long("received_at")
    val isPayloadEncrypted = bool("is_payload_encrypted").default(false)
    val syncImportReason = text("sync_import_reason").nullable()
    val repairBaseServerSeq = integer("repair_base_server_seq").nullable()
    override val primaryKey = PrimaryKey(id, name = "operations_pkey")

    init {
        index("operations_user_id_entity_type_entity_id_server_seq_idx", false, userId, entityType, entityId, serverSeq)
        index("operations_user_id_received_at_server_seq_idx", false, userId, receivedAt, serverSeq)
        index("operations_entity_ids_gin", false, entityIds, indexType = "gin")
        uniqueIndex("operations_user_id_server_seq_key", userId, serverSeq)
    }
}
