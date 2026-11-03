package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BackgroundWorkerTaskTable : IdTable<String>("\"BackgroundWorkerTask\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val slug = text("slug")
    val description = text("description").nullable()
    val friendlyId = text("friendlyId").uniqueIndex("\"BackgroundWorkerTask_friendlyId_key\"")
    val filePath = text("filePath")
    val exportName = text("exportName").nullable()
    val workerId =
        reference(
            "workerId",
            BackgroundWorkerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BackgroundWorkerTask_workerId_fkey\"",
        )
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BackgroundWorkerTask_projectId_fkey\"",
        )
    val fileId =
        optReference(
            "fileId",
            BackgroundWorkerFileTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BackgroundWorkerTask_fileId_fkey\"",
        )
    val runtimeEnvironmentId =
        reference(
            "runtimeEnvironmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BackgroundWorkerTask_runtimeEnvironmentId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val queueConfig = jsonb<String>("queueConfig", { it }, { it }).nullable()
    val retryConfig = jsonb<String>("retryConfig", { it }, { it }).nullable()
    val machineConfig = jsonb<String>("machineConfig", { it }, { it }).nullable()
    val gates = jsonb<String>("gates", { it }, { it }).nullable()
    val queueId =
        optReference(
            "queueId",
            TaskQueueTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BackgroundWorkerTask_queueId_fkey\"",
        )
    val maxDurationInSeconds = integer("maxDurationInSeconds").nullable()
    val ttl = text("ttl").nullable()
    val triggerSource =
        pgEnum("triggerSource", "\"TaskTriggerSource\"", TaskTriggerSource.entries, TaskTriggerSource::dbName).default(
            TaskTriggerSource.STANDARD,
        )
    val config = jsonb<String>("config", { it }, { it }).nullable()
    val payloadSchema = jsonb<String>("payloadSchema", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"BackgroundWorkerTask_pkey\"")

    init {
        index("\"BackgroundWorkerTask_projectId_slug_idx\"", false, projectId, slug)
        index("\"BackgroundWorkerTask_projectId_slug_createdAt_idx\"", false, projectId, slug, createdAt)
        index("\"BackgroundWorkerTask_runtimeEnvironmentId_projectId_idx\"", false, runtimeEnvironmentId, projectId)
        index("BackgroundWorkerTask_runtimeEnvironmentId_slug_triggerSourc_idx", false, runtimeEnvironmentId, slug, triggerSource)
        uniqueIndex("\"BackgroundWorkerTask_workerId_slug_key\"", workerId, slug)
    }
}
