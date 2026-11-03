package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProjectAlertTable : IdTable<String>("\"ProjectAlert\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"ProjectAlert_friendlyId_key\"")
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ProjectAlert_projectId_fkey\"",
        )
    val environmentId =
        reference(
            "environmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ProjectAlert_environmentId_fkey\"",
        )
    val channelId =
        reference(
            "channelId",
            ProjectAlertChannelTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ProjectAlert_channelId_fkey\"",
        )
    val status =
        pgEnum("status", "\"ProjectAlertStatus\"", ProjectAlertStatus.entries, ProjectAlertStatus::dbName).default(
            ProjectAlertStatus.PENDING,
        )
    val type = pgEnum("type", "\"ProjectAlertType\"", ProjectAlertType.entries, ProjectAlertType::dbName)
    val taskRunAttemptId = text("taskRunAttemptId").nullable()
    val taskRunId = text("taskRunId").nullable()
    val workerDeploymentId =
        optReference(
            "workerDeploymentId",
            WorkerDeploymentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ProjectAlert_workerDeploymentId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ProjectAlert_pkey\"")

    init {
        index("\"ProjectAlert_channelId_idx\"", false, channelId)
    }
}
