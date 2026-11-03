package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object IntegrationDeploymentTable : IdTable<String>("\"IntegrationDeployment\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val integrationName = text("integrationName")
    val integrationDeploymentId = text("integrationDeploymentId")
    val commitSHA = text("commitSHA")
    val deploymentId =
        optReference(
            "deploymentId",
            WorkerDeploymentTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"IntegrationDeployment_deploymentId_fkey\"",
        )
    val status = text("status").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"IntegrationDeployment_pkey\"")

    init {
        index("\"IntegrationDeployment_commitSHA_idx\"", false, commitSHA)
        index("\"IntegrationDeployment_deploymentId_idx\"", false, deploymentId)
    }
}
