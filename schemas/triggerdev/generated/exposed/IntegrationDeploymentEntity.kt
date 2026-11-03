package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class IntegrationDeploymentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IntegrationDeploymentEntity>(IntegrationDeploymentTable)

    var integrationName by IntegrationDeploymentTable.integrationName
    var integrationDeploymentId by IntegrationDeploymentTable.integrationDeploymentId
    var commitSHA by IntegrationDeploymentTable.commitSHA
    var deploymentId by IntegrationDeploymentTable.deploymentId
    var status by IntegrationDeploymentTable.status
    var workerDeployment by WorkerDeploymentEntity optionalReferencedOn IntegrationDeploymentTable.deploymentId
    var createdAt by IntegrationDeploymentTable.createdAt
    var updatedAt by IntegrationDeploymentTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(IntegrationDeploymentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
