package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class ErrorIssueEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, ErrorIssueEntity>(ErrorIssueTable)

    var websiteId by ErrorIssueTable.websiteId
    var fingerprint by ErrorIssueTable.fingerprint
    var groupingVersion by ErrorIssueTable.groupingVersion
    var title by ErrorIssueTable.title
    var status by ErrorIssueTable.status
    var resolvedAt by ErrorIssueTable.resolvedAt
    var createdAt by ErrorIssueTable.createdAt
}
