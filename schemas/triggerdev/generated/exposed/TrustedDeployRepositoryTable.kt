package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TrustedDeployRepositoryTable : IdTable<String>("\"TrustedDeployRepository\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TrustedDeployRepository_projectId_fkey\"",
        ).uniqueIndex("\"TrustedDeployRepository_projectId_key\"")
    val issuer = text("issuer").default("https://token.actions.githubusercontent.com")
    val repositoryOwner = text("repositoryOwner")
    val repositoryName = text("repositoryName")
    val repositoryId = text("repositoryId").nullable()
    val repositoryOwnerId = text("repositoryOwnerId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TrustedDeployRepository_pkey\"")
}
