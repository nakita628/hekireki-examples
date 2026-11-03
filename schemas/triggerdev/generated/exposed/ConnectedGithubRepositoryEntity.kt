package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ConnectedGithubRepositoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConnectedGithubRepositoryEntity>(ConnectedGithubRepositoryTable)

    var project by ProjectEntity referencedOn ConnectedGithubRepositoryTable.projectId
    var projectId by ConnectedGithubRepositoryTable.projectId
    var repository by GithubRepositoryEntity referencedOn ConnectedGithubRepositoryTable.repositoryId
    var repositoryId by ConnectedGithubRepositoryTable.repositoryId
    var branchTracking by ConnectedGithubRepositoryTable.branchTracking
    var previewDeploymentsEnabled by ConnectedGithubRepositoryTable.previewDeploymentsEnabled
    var buildFilters by ConnectedGithubRepositoryTable.buildFilters
    var createdAt by ConnectedGithubRepositoryTable.createdAt
    var updatedAt by ConnectedGithubRepositoryTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ConnectedGithubRepositoryTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
