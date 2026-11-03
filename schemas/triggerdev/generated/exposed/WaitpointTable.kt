package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WaitpointTable : IdTable<String>("\"Waitpoint\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"Waitpoint_friendlyId_key\"")
    val type = pgEnum("type", "\"WaitpointType\"", WaitpointType.entries, WaitpointType::dbName)
    val status = pgEnum("status", "\"WaitpointStatus\"", WaitpointStatus.entries, WaitpointStatus::dbName).default(WaitpointStatus.PENDING)
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val idempotencyKey = text("idempotencyKey")
    val userProvidedIdempotencyKey = bool("userProvidedIdempotencyKey")
    val idempotencyKeyExpiresAt = pgTimestamp("idempotencyKeyExpiresAt", 3).nullable()
    val inactiveIdempotencyKey = text("inactiveIdempotencyKey").nullable()
    val completedByTaskRunId =
        optReference(
            "completedByTaskRunId",
            TaskRunTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Waitpoint_completedByTaskRunId_fkey\"",
        ).uniqueIndex("\"Waitpoint_completedByTaskRunId_key\"")
    val completedAfter = pgTimestamp("completedAfter", 3).nullable()
    val completedByBatchId =
        optReference(
            "completedByBatchId",
            BatchTaskRunTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Waitpoint_completedByBatchId_fkey\"",
        )
    val output = text("output").nullable()
    val outputType = text("outputType").default("application/json")
    val outputIsError = bool("outputIsError").default(false)
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Waitpoint_projectId_fkey\"",
        )
    val environmentId =
        reference(
            "environmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Waitpoint_environmentId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val tags = array("tags", TextColumnType()).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Waitpoint_pkey\"")

    init {
        index("\"Waitpoint_completedByBatchId_idx\"", false, completedByBatchId)
        index(
            "\"Waitpoint_environmentId_type_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"environmentId\""), indexExpression("\"type\""), indexExpression("\"createdAt\" DESC")),
        )
        index("\"Waitpoint_environmentId_type_status_idx\"", false, environmentId, type, status)
        index(
            "\"Waitpoint_environmentId_type_id_idx\"",
            false,
            functions = listOf(indexExpression("\"environmentId\""), indexExpression("\"type\""), indexExpression("\"id\" DESC")),
        )
        uniqueIndex("\"Waitpoint_environmentId_idempotencyKey_key\"", environmentId, idempotencyKey)
    }
}
