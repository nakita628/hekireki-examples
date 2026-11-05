package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ShareReportEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ShareReportEntity>(ShareReportTable)

    var reportId by ShareReportTable.reportId
    var report by ReportEntity referencedOn ShareReportTable.reportId
    var organization by OrganizationEntity referencedOn ShareReportTable.organizationId
    var organizationId by ShareReportTable.organizationId
    var projectId by ShareReportTable.projectId
    var project by ProjectEntity referencedOn ShareReportTable.projectId
    var public by ShareReportTable.public
    var password by ShareReportTable.password
    var createdAt by ShareReportTable.createdAt
    var updatedAt by ShareReportTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ShareReportTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
