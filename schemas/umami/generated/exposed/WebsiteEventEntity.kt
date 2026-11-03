package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class WebsiteEventEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, WebsiteEventEntity>(WebsiteEventTable)

    var websiteId by WebsiteEventTable.websiteId
    var sessionId by WebsiteEventTable.sessionId
    var visitId by WebsiteEventTable.visitId
    var createdAt by WebsiteEventTable.createdAt
    var urlPath by WebsiteEventTable.urlPath
    var urlQuery by WebsiteEventTable.urlQuery
    var utmSource by WebsiteEventTable.utmSource
    var utmMedium by WebsiteEventTable.utmMedium
    var utmCampaign by WebsiteEventTable.utmCampaign
    var utmContent by WebsiteEventTable.utmContent
    var utmTerm by WebsiteEventTable.utmTerm
    var referrerPath by WebsiteEventTable.referrerPath
    var referrerQuery by WebsiteEventTable.referrerQuery
    var referrerDomain by WebsiteEventTable.referrerDomain
    var pageTitle by WebsiteEventTable.pageTitle
    var gclid by WebsiteEventTable.gclid
    var fbclid by WebsiteEventTable.fbclid
    var msclkid by WebsiteEventTable.msclkid
    var ttclid by WebsiteEventTable.ttclid
    var lifatid by WebsiteEventTable.lifatid
    var twclid by WebsiteEventTable.twclid
    var eventType by WebsiteEventTable.eventType
    var eventName by WebsiteEventTable.eventName
    var tag by WebsiteEventTable.tag
    var hostname by WebsiteEventTable.hostname
    var lcp by WebsiteEventTable.lcp
    var inp by WebsiteEventTable.inp
    var cls by WebsiteEventTable.cls
    var fcp by WebsiteEventTable.fcp
    var ttfb by WebsiteEventTable.ttfb
}
