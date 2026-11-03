package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskRunExecutionSnapshotTable : IdTable<String>("\"TaskRunExecutionSnapshot\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val engine = pgEnum("engine", "\"RunEngineVersion\"", RunEngineVersion.entries, RunEngineVersion::dbName).default(RunEngineVersion.V2)
    val executionStatus =
        pgEnum("executionStatus", "\"TaskRunExecutionStatus\"", TaskRunExecutionStatus.entries, TaskRunExecutionStatus::dbName)
    val description = text("description")
    val isValid = bool("isValid").default(true)
    val error = text("error").nullable()
    val previousSnapshotId = text("previousSnapshotId").nullable()
    val runId =
        reference(
            "runId",
            TaskRunTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunExecutionSnapshot_runId_fkey\"",
        )
    val runStatus = pgEnum("runStatus", "\"TaskRunStatus\"", TaskRunStatus.entries, TaskRunStatus::dbName)
    val batchId = text("batchId").nullable()
    val attemptNumber = integer("attemptNumber").nullable()
    val environmentId = text("environmentId")
    val environmentType =
        pgEnum("environmentType", "\"RuntimeEnvironmentType\"", RuntimeEnvironmentType.entries, RuntimeEnvironmentType::dbName)
    val projectId = text("projectId")
    val organizationId = text("organizationId")
    val completedWaitpointOrder = array("completedWaitpointOrder", TextColumnType()).nullable()
    val checkpointId =
        optReference(
            "checkpointId",
            TaskRunCheckpointTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunExecutionSnapshot_checkpointId_fkey\"",
        )
    val workerId = text("workerId").nullable()
    val runnerId = text("runnerId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastHeartbeatAt = pgTimestamp("lastHeartbeatAt", 3).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"TaskRunExecutionSnapshot_pkey\"")

    init {
        index(
            "\"TaskRunExecutionSnapshot_runId_isValid_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"runId\""), indexExpression("\"isValid\""), indexExpression("\"createdAt\" DESC")),
        )
    }
}
