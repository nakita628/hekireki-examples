package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OrganizationAccessNoticeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationAccessNoticeEntity>(OrganizationAccessNoticeTable)

    var organizationId by OrganizationAccessNoticeTable.organizationId
    var kind by OrganizationAccessNoticeTable.kind
    var email by OrganizationAccessNoticeTable.email
    var name by OrganizationAccessNoticeTable.name
    var idp by OrganizationAccessNoticeTable.idp
    var dayKey by OrganizationAccessNoticeTable.dayKey
    var idpRefused by OrganizationAccessNoticeTable.idpRefused
    var createdAt by OrganizationAccessNoticeTable.createdAt
    var mailedAt by OrganizationAccessNoticeTable.mailedAt
}
