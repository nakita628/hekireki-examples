package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkerInstanceGroupTable : IdTable<String>("\"WorkerInstanceGroup\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val type = pgEnum("type", "\"WorkerInstanceGroupType\"", WorkerInstanceGroupType.entries, WorkerInstanceGroupType::dbName)
    val name = text("name")
    val masterQueue = text("masterQueue").uniqueIndex("\"WorkerInstanceGroup_masterQueue_key\"")
    val description = text("description").nullable()
    val hidden = bool("hidden").default(false)
    val tokenId =
        reference(
            "tokenId",
            WorkerGroupTokenTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkerInstanceGroup_tokenId_fkey\"",
        ).uniqueIndex("\"WorkerInstanceGroup_tokenId_key\"")
    val organizationId =
        optReference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkerInstanceGroup_organizationId_fkey\"",
        )
    val projectId =
        optReference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkerInstanceGroup_projectId_fkey\"",
        )
    val cloudProvider = text("cloudProvider").nullable()
    val location = text("location").nullable()
    val staticIPs = text("staticIPs").nullable()
    val workloadType =
        pgEnum("workloadType", "\"WorkloadType\"", WorkloadType.entries, WorkloadType::dbName).default(WorkloadType.CONTAINER)
    val region = text("region").nullable()
    val enableFastPath = bool("enableFastPath").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"WorkerInstanceGroup_pkey\"")
}
