package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object ProcessManagerOutboxTable : IdTable<String>("\"ProcessManagerOutbox\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val processName = text("processName")
    val projectId = text("projectId")
    val processKey = text("processKey")
    val tenantId = text("tenantId")
    val userId = text("userId").nullable()
    val messageKey = text("messageKey")
    val intentType = text("intentType")
    val payload = jsonb<String>("payload", { it }, { it })
    val traceCarrier = jsonb<String>("traceCarrier", { it }, { it })
    val sourceEventId = text("sourceEventId").nullable()
    val status =
        pgEnum("status", "\"ProcessManagerOutboxStatus\"", ProcessManagerOutboxStatus.entries, ProcessManagerOutboxStatus::dbName).default(
            ProcessManagerOutboxStatus.PENDING,
        )
    val attempts = integer("attempts").default(0)
    val nextAttemptAt = pgTimestamptz("nextAttemptAt", 3)
    val leasedUntil = pgTimestamptz("leasedUntil", 3).nullable()
    val leaseToken = text("leaseToken").nullable().uniqueIndex("\"ProcessManagerOutbox_leaseToken_key\"")
    val dispatchedAt = pgTimestamptz("dispatchedAt", 3).nullable()
    val createdAt = pgTimestamptz("createdAt", 3)
    val updatedAt = pgTimestamptz("updatedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"ProcessManagerOutbox_pkey\"")

    init {
        index("ProcessManagerOutbox_processName_projectId_processKey_creat_idx", false, processName, projectId, processKey, createdAt)
        index("\"ProcessManagerOutbox_status_nextAttemptAt_leasedUntil_idx\"", false, status, nextAttemptAt, leasedUntil)
        index("\"ProcessManagerOutbox_status_processName_nextAttemptAt_idx\"", false, status, processName, nextAttemptAt)
        uniqueIndex("\"ProcessManagerOutbox_processName_projectId_messageKey_key\"", processName, projectId, messageKey)
    }
}
