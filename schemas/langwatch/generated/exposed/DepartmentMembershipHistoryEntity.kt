package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DepartmentMembershipHistoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DepartmentMembershipHistoryEntity>(DepartmentMembershipHistoryTable)

    var organizationId by DepartmentMembershipHistoryTable.organizationId
    var userId by DepartmentMembershipHistoryTable.userId
    var departmentId by DepartmentMembershipHistoryTable.departmentId
    var validFrom by DepartmentMembershipHistoryTable.validFrom
    var validTo by DepartmentMembershipHistoryTable.validTo
    var createdAt by DepartmentMembershipHistoryTable.createdAt
}
