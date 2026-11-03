package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskRunWaitpointTable : IdTable<String>("\"TaskRunWaitpoint\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val taskRunId =
        reference(
            "taskRunId",
            TaskRunTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunWaitpoint_taskRunId_fkey\"",
        )
    val waitpointId =
        reference(
            "waitpointId",
            WaitpointTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunWaitpoint_waitpointId_fkey\"",
        )
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunWaitpoint_projectId_fkey\"",
        )
    val spanIdToComplete = text("spanIdToComplete").nullable()
    val batchId =
        optReference(
            "batchId",
            BatchTaskRunTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunWaitpoint_batchId_fkey\"",
        )
    val batchIndex = integer("batchIndex").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TaskRunWaitpoint_pkey\"")

    init {
        index("\"TaskRunWaitpoint_taskRunId_idx\"", false, taskRunId)
        index("\"TaskRunWaitpoint_waitpointId_idx\"", false, waitpointId)
        uniqueIndex("\"TaskRunWaitpoint_taskRunId_waitpointId_batchIndex_key\"", taskRunId, waitpointId, batchIndex)
    }
}
