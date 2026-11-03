package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LinkPresetEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LinkPresetEntity>(LinkPresetTable)

    var name by LinkPresetTable.name
    var teamId by LinkPresetTable.teamId
    var team by TeamEntity referencedOn LinkPresetTable.teamId
    var pId by LinkPresetTable.pId
    var enableCustomMetaTag by LinkPresetTable.enableCustomMetaTag
    var metaTitle by LinkPresetTable.metaTitle
    var metaDescription by LinkPresetTable.metaDescription
    var metaImage by LinkPresetTable.metaImage
    var metaFavicon by LinkPresetTable.metaFavicon
    var enableNotification by LinkPresetTable.enableNotification
    var emailProtected by LinkPresetTable.emailProtected
    var emailAuthenticated by LinkPresetTable.emailAuthenticated
    var allowDownload by LinkPresetTable.allowDownload
    var enableAllowList by LinkPresetTable.enableAllowList
    var allowList by LinkPresetTable.allowList
    var enableDenyList by LinkPresetTable.enableDenyList
    var denyList by LinkPresetTable.denyList
    var expiresIn by LinkPresetTable.expiresIn
    var enableScreenshotProtection by LinkPresetTable.enableScreenshotProtection
    var enableConfidentialView by LinkPresetTable.enableConfidentialView
    var expiresAt by LinkPresetTable.expiresAt
    var enablePassword by LinkPresetTable.enablePassword
    var password by LinkPresetTable.password
    var enableWatermark by LinkPresetTable.enableWatermark
    var watermarkConfig by LinkPresetTable.watermarkConfig
    var enableAgreement by LinkPresetTable.enableAgreement
    var agreementId by LinkPresetTable.agreementId
    var enableCustomFields by LinkPresetTable.enableCustomFields
    var customFields by LinkPresetTable.customFields
    var showBanner by LinkPresetTable.showBanner
    var welcomeMessage by LinkPresetTable.welcomeMessage
    var isDefault by LinkPresetTable.isDefault
    var createdAt by LinkPresetTable.createdAt
    var updatedAt by LinkPresetTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LinkPresetTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
