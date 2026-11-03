package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BackgroundWorkerTable : IdTable<String>("\"BackgroundWorker\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"BackgroundWorker_friendlyId_key\"")
    val engine = pgEnum("engine", "\"RunEngineVersion\"", RunEngineVersion.entries, RunEngineVersion::dbName).default(RunEngineVersion.V1)
    val contentHash = text("contentHash")
    val sdkVersion = text("sdkVersion").default("unknown")
    val cliVersion = text("cliVersion").default("unknown")
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BackgroundWorker_projectId_fkey\"",
        )
    val runtimeEnvironmentId =
        reference(
            "runtimeEnvironmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BackgroundWorker_runtimeEnvironmentId_fkey\"",
        )
    val version = text("version")
    val metadata = jsonb<String>("metadata", { it }, { it })
    val runtime = text("runtime").nullable()
    val runtimeVersion = text("runtimeVersion").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val workerGroupId =
        optReference(
            "workerGroupId",
            WorkerInstanceGroupTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BackgroundWorker_workerGroupId_fkey\"",
        )
    val supportsLazyAttempts = bool("supportsLazyAttempts").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"BackgroundWorker_pkey\"")

    init {
        index("\"BackgroundWorker_runtimeEnvironmentId_idx\"", false, runtimeEnvironmentId)
        index(
            "\"BackgroundWorker_runtimeEnvironmentId_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"runtimeEnvironmentId\""), indexExpression("\"createdAt\" DESC")),
        )
        uniqueIndex("\"BackgroundWorker_projectId_runtimeEnvironmentId_version_key\"", projectId, runtimeEnvironmentId, version)
    }
}
