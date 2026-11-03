package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkerDeploymentTable : IdTable<String>("\"WorkerDeployment\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val contentHash = text("contentHash")
    val friendlyId = text("friendlyId").uniqueIndex("\"WorkerDeployment_friendlyId_key\"")
    val shortCode = text("shortCode")
    val version = text("version")
    val runtime = text("runtime").nullable()
    val runtimeVersion = text("runtimeVersion").nullable()
    val cliVersion = text("cliVersion").nullable()
    val imageReference = text("imageReference").nullable()
    val imagePlatform = text("imagePlatform").default("linux/amd64")
    val templateKey = text("templateKey").nullable()
    val indexObjectKey = text("indexObjectKey").nullable()
    val indexSha256 = text("indexSha256").nullable()
    val indexFormatVersion = integer("indexFormatVersion").nullable()
    val externalBuildData = jsonb<String>("externalBuildData", { it }, { it }).nullable()
    val buildServerMetadata = jsonb<String>("buildServerMetadata", { it }, { it }).nullable()
    val buildEnvVars = jsonb<String>("buildEnvVars", { it }, { it }).nullable()
    val status =
        pgEnum("status", "\"WorkerDeploymentStatus\"", WorkerDeploymentStatus.entries, WorkerDeploymentStatus::dbName).default(
            WorkerDeploymentStatus.PENDING,
        )
    val type =
        pgEnum("type", "\"WorkerDeploymentType\"", WorkerDeploymentType.entries, WorkerDeploymentType::dbName).default(
            WorkerDeploymentType.V1,
        )
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkerDeployment_projectId_fkey\"",
        )
    val environmentId =
        reference(
            "environmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkerDeployment_environmentId_fkey\"",
        )
    val workerId =
        optReference(
            "workerId",
            BackgroundWorkerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkerDeployment_workerId_fkey\"",
        ).uniqueIndex("\"WorkerDeployment_workerId_key\"")
    val triggeredById =
        optReference(
            "triggeredById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkerDeployment_triggeredById_fkey\"",
        )
    val triggeredVia = text("triggeredVia").nullable()
    val commitSHA = text("commitSHA").nullable()
    val externalId = text("externalId").nullable()
    val startedAt = pgTimestamp("startedAt", 3).nullable()
    val installedAt = pgTimestamp("installedAt", 3).nullable()
    val builtAt = pgTimestamp("builtAt", 3).nullable()
    val deployedAt = pgTimestamp("deployedAt", 3).nullable()
    val failedAt = pgTimestamp("failedAt", 3).nullable()
    val errorData = jsonb<String>("errorData", { it }, { it }).nullable()
    val canceledAt = pgTimestamp("canceledAt", 3).nullable()
    val canceledReason = text("canceledReason").nullable()
    val git = jsonb<String>("git", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"WorkerDeployment_pkey\"")

    init {
        index("\"WorkerDeployment_commitSHA_idx\"", false, commitSHA)
        index("\"WorkerDeployment_environmentId_createdAt_idx\"", false, environmentId, createdAt)
        index("\"WorkerDeployment_environmentId_status_id_idx\"", false, environmentId, status, id)
        index("\"WorkerDeployment_environmentId_externalId_idx\"", false, environmentId, externalId)
        uniqueIndex("\"WorkerDeployment_projectId_shortCode_key\"", projectId, shortCode)
        uniqueIndex("\"WorkerDeployment_environmentId_version_key\"", environmentId, version)
    }
}
