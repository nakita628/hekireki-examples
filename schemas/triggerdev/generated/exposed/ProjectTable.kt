package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProjectTable : IdTable<String>("\"Project\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val slug = text("slug").uniqueIndex("\"Project_slug_key\"")
    val name = text("name")
    val externalRef = text("externalRef").uniqueIndex("\"Project_externalRef_key\"")
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Project_organizationId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val initializedAt = pgTimestamp("initializedAt", 3).nullable()
    val defaultRuntime = text("defaultRuntime").nullable()
    val version = pgEnum("version", "\"ProjectVersion\"", ProjectVersion.entries, ProjectVersion::dbName).default(ProjectVersion.V2)
    val engine = pgEnum("engine", "\"RunEngineVersion\"", RunEngineVersion.entries, RunEngineVersion::dbName).default(RunEngineVersion.V1)
    val builderProjectId = text("builderProjectId").nullable()
    val defaultWorkerGroupId =
        optReference(
            "defaultWorkerGroupId",
            WorkerInstanceGroupTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Project_defaultWorkerGroupId_fkey\"",
        )
    val allowedWorkerQueues = array("allowedMasterQueues", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val buildSettings = jsonb<String>("buildSettings", { it }, { it }).nullable()
    val onboardingData = jsonb<String>("onboardingData", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Project_pkey\"")
}
