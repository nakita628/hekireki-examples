package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class SystemMigrationEnrollmentEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SystemMigrationEnrollmentEntity>(SystemMigrationEnrollmentTable)

    val organizationId by SystemMigrationEnrollmentTable.organizationId
    val migrationName by SystemMigrationEnrollmentTable.migrationName
    var enrolledByUserId by SystemMigrationEnrollmentTable.enrolledByUserId
    var createdAt by SystemMigrationEnrollmentTable.createdAt
}
