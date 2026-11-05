package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProjectEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProjectEntity>(ProjectTable)

    var name by ProjectTable.name
    var organization by OrganizationEntity referencedOn ProjectTable.organizationId
    var organizationId by ProjectTable.organizationId
    var eventsCount by ProjectTable.eventsCount
    var types by ProjectTable.types
    var domain by ProjectTable.domain
    var cors by ProjectTable.cors
    var crossDomain by ProjectTable.crossDomain
    var allowUnsafeRevenueTracking by ProjectTable.allowUnsafeRevenueTracking
    var aiStoreContent by ProjectTable.aiStoreContent
    var filters by ProjectTable.filters
    var firstEventAt by ProjectTable.firstEventAt
    var noDataNotifiedAt by ProjectTable.noDataNotifiedAt
    var dataStoppedNotifiedAt by ProjectTable.dataStoppedNotifiedAt
    val clients by ClientEntity optionalReferrersOn ClientTable.projectId
    val reports by ReportEntity referrersOn ReportTable.projectId
    val dashboards by DashboardEntity referrersOn DashboardTable.projectId
    val share by ShareOverviewEntity optionalBackReferencedOn ShareOverviewTable.projectId
    val shareDashboards by ShareDashboardEntity referrersOn ShareDashboardTable.projectId
    val shareReports by ShareReportEntity referrersOn ShareReportTable.projectId
    val shareWidgets by ShareWidgetEntity referrersOn ShareWidgetTable.projectId
    val meta by EventMetaEntity referrersOn EventMetaTable.projectId
    val customEvents by CustomEventEntity referrersOn CustomEventTable.projectId
    val references by ReferenceEntity referrersOn ReferenceTable.projectId
    val memberProjects by MemberProjectEntity referrersOn MemberProjectTable.projectId
    val notificationRules by NotificationRuleEntity referrersOn NotificationRuleTable.projectId
    val notifications by NotificationEntity referrersOn NotificationTable.projectId
    val imports by ImportEntity referrersOn ImportTable.projectId
    val gscConnection by GscConnectionEntity optionalBackReferencedOn GscConnectionTable.projectId
    val cohorts by CohortEntity referrersOn CohortTable.projectId
    val exportWatermarks by ExportWatermarkEntity referrersOn ExportWatermarkTable.projectId
    val integrations by IntegrationEntity optionalReferrersOn IntegrationTable.projectId
    var deleteAt by ProjectTable.deleteAt
    var createdAt by ProjectTable.createdAt
    var updatedAt by ProjectTable.updatedAt
    val conversations by ConversationEntity referrersOn ConversationTable.projectId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProjectTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
