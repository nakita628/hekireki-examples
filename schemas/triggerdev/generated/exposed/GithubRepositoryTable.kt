package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object GithubRepositoryTable : IdTable<String>("\"GithubRepository\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val githubId = long("githubId")
    val name = text("name")
    val fullName = text("fullName")
    val htmlUrl = text("htmlUrl")
    val private = bool("private")
    val defaultBranch = text("defaultBranch")
    val installationId =
        reference(
            "installationId",
            GithubAppInstallationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"GithubRepository_installationId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GithubRepository_pkey\"")

    init {
        index("\"GithubRepository_installationId_idx\"", false, installationId)
        uniqueIndex("\"GithubRepository_installationId_githubId_key\"", installationId, githubId)
    }
}
