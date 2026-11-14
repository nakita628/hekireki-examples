package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class BugReportEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BugReportEntity>(BugReportTable)

    var createdAt by BugReportTable.createdAt
    var source by BugReportTable.source1
    var kind by BugReportTable.kind
    var title by BugReportTable.title
    var summary by BugReportTable.summary
    var sessionData by BugReportTable.sessionData
    var sessionTruncated by BugReportTable.sessionTruncated
    var agent by BugReportTable.agent
    var contactEmail by BugReportTable.contactEmail
    var cliVersion by BugReportTable.cliVersion
    var linkedProjectId by BugReportTable.linkedProjectId
    var metadata by BugReportTable.metadata
}
