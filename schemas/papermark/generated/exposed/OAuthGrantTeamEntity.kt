package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OAuthGrantTeamEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OAuthGrantTeamEntity>(OAuthGrantTeamTable)

    var grantId by OAuthGrantTeamTable.grantId
    var teamId by OAuthGrantTeamTable.teamId
    var userId by OAuthGrantTeamTable.userId
    var createdAt by OAuthGrantTeamTable.createdAt
}
