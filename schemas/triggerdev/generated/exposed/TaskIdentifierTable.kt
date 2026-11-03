package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskIdentifierTable : IdTable<String>("\"TaskIdentifier\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val runtimeEnvironmentId =
        reference(
            "runtimeEnvironmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskIdentifier_runtimeEnvironmentId_fkey\"",
        )
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskIdentifier_projectId_fkey\"",
        )
    val slug = text("slug")
    val currentTriggerSource =
        pgEnum("currentTriggerSource", "\"TaskTriggerSource\"", TaskTriggerSource.entries, TaskTriggerSource::dbName).default(
            TaskTriggerSource.STANDARD,
        )
    val currentWorkerId =
        optReference(
            "currentWorkerId",
            BackgroundWorkerTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskIdentifier_currentWorkerId_fkey\"",
        )
    val firstSeenAt = pgTimestamp("firstSeenAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastSeenAt = pgTimestamp("lastSeenAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val isInLatestDeployment = bool("isInLatestDeployment").default(true)
    override val primaryKey = PrimaryKey(id, name = "\"TaskIdentifier_pkey\"")

    init {
        index("\"TaskIdentifier_runtimeEnvironmentId_isInLatestDeployment_idx\"", false, runtimeEnvironmentId, isInLatestDeployment)
        uniqueIndex("\"TaskIdentifier_runtimeEnvironmentId_slug_key\"", runtimeEnvironmentId, slug)
    }
}
