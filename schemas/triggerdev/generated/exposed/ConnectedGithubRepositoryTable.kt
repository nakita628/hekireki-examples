package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConnectedGithubRepositoryTable : IdTable<String>("\"ConnectedGithubRepository\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConnectedGithubRepository_projectId_fkey\"",
        ).uniqueIndex("\"ConnectedGithubRepository_projectId_key\"")
    val repositoryId =
        reference(
            "repositoryId",
            GithubRepositoryTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConnectedGithubRepository_repositoryId_fkey\"",
        )
    val branchTracking = jsonb<String>("branchTracking", { it }, { it })
    val previewDeploymentsEnabled = bool("previewDeploymentsEnabled").default(true)
    val buildFilters = jsonb<String>("buildFilters", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ConnectedGithubRepository_pkey\"")

    init {
        index("\"ConnectedGithubRepository_repositoryId_idx\"", false, repositoryId)
    }
}
