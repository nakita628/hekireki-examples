package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ReferralInviteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ReferralInviteEntity>(ReferralInviteTable)

    var userId by ReferralInviteTable.userId
    var user by UserEntity referencedOn ReferralInviteTable.userId
    var teamId by ReferralInviteTable.teamId
    var team by TeamEntity optionalReferencedOn ReferralInviteTable.teamId
    var scopeId by ReferralInviteTable.scopeId
    var email by ReferralInviteTable.email
    var referralLink by ReferralInviteTable.referralLink
    var sentAt by ReferralInviteTable.sentAt
}
