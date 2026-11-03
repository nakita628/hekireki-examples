package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrgMemberEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrgMemberEntity>(OrgMemberTable)

    var organization by OrganizationEntity referencedOn OrgMemberTable.organizationId
    var organizationId by OrgMemberTable.organizationId
    var user by UserEntity referencedOn OrgMemberTable.userId
    var userId by OrgMemberTable.userId
    var role by OrgMemberTable.role
    var createdAt by OrgMemberTable.createdAt
    var updatedAt by OrgMemberTable.updatedAt
    val environments by RuntimeEnvironmentEntity optionalReferrersOn RuntimeEnvironmentTable.orgMemberId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrgMemberTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
