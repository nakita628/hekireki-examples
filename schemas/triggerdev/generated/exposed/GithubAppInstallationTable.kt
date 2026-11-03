package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object GithubAppInstallationTable : IdTable<String>("\"GithubAppInstallation\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val appInstallationId = long("appInstallationId").uniqueIndex("\"GithubAppInstallation_appInstallationId_key\"")
    val targetId = long("targetId")
    val targetType = text("targetType")
    val accountHandle = text("accountHandle")
    val permissions = jsonb<String>("permissions", { it }, { it }).nullable()
    val repositorySelection =
        pgEnum("repositorySelection", "\"GithubRepositorySelection\"", GithubRepositorySelection.entries, GithubRepositorySelection::dbName)
    val installedBy = text("installedBy").nullable()
    val installedByUserId = text("installedByUserId").nullable()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"GithubAppInstallation_organizationId_fkey\"",
        )
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val suspendedAt = pgTimestamp("suspendedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GithubAppInstallation_pkey\"")

    init {
        index("\"GithubAppInstallation_organizationId_idx\"", false, organizationId)
    }
}
