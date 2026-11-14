package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object GithubInstallationTable : IdTable<String>("\"GithubInstallation\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val installationId = text("installationId").uniqueIndex("\"GithubInstallation_installationId_key\"")
    val organizationId = text("organizationId")
    val accountLogin = text("accountLogin")
    val accountType = text("accountType")
    val accountId = text("accountId")
    val repositorySelection = text("repositorySelection")
    val repositories = jsonb<String>("repositories", { it }, { it }).nullable()
    val suspendedAt = pgTimestamp("suspendedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GithubInstallation_pkey\"")

    init {
        index("\"GithubInstallation_organizationId_idx\"", false, organizationId)
    }
}
