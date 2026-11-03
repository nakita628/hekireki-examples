package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class ResponseQuotaLinkEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<ResponseQuotaLinkEntity>(ResponseQuotaLinkTable)

    val response by ResponseEntity referencedOn ResponseQuotaLinkTable.responseId
    val responseId by ResponseQuotaLinkTable.responseId
    val quota by SurveyQuotaEntity referencedOn ResponseQuotaLinkTable.quotaId
    val quotaId by ResponseQuotaLinkTable.quotaId
    var status by ResponseQuotaLinkTable.status
}
