package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class EmailSuppressionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EmailSuppressionEntity>(EmailSuppressionTable)

    var projectId by EmailSuppressionTable.projectId
    var email by EmailSuppressionTable.email
    var triggerId by EmailSuppressionTable.triggerId
    var reason by EmailSuppressionTable.reason
    var createdAt by EmailSuppressionTable.createdAt
}
