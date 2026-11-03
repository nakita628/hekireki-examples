package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CollaboratorTable : IdTable<String>("collaborator") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val roleName = text("role_name")
    val resourceType = text("resource_type")
    val resourceId = text("resource_id")
    val principalId = text("principal_id")
    val principalType = text("principal_type")
    val createdBy = text("created_by")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedBy = text("last_modified_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "collaborator_pkey")

    init {
        index("collaborator_resource_id_idx", false, resourceId)
        index("collaborator_principal_id_idx", false, principalId)
        uniqueIndex("collaborator_resource_type_resource_id_principal_id_princip_key", resourceType, resourceId, principalId, principalType)
    }
}
