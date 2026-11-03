package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganisationMemberEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganisationMemberEntity>(OrganisationMemberTable)

    var createdAt by OrganisationMemberTable.createdAt
    var updatedAt by OrganisationMemberTable.updatedAt
    var userId by OrganisationMemberTable.userId
    var user by UserEntity referencedOn OrganisationMemberTable.userId
    var organisationId by OrganisationMemberTable.organisationId
    var organisation by OrganisationEntity referencedOn OrganisationMemberTable.organisationId
    val organisationGroupMembers by OrganisationGroupMemberEntity referrersOn OrganisationGroupMemberTable.organisationMemberId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganisationMemberTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
