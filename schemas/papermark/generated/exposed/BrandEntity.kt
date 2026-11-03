package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BrandEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BrandEntity>(BrandTable)

    var logo by BrandTable.logo
    var hideLogo by BrandTable.hideLogo
    var banner by BrandTable.banner
    var brandColor by BrandTable.brandColor
    var accentColor by BrandTable.accentColor
    var accentButtonColor by BrandTable.accentButtonColor
    var applyAccentColorToDataroomView by BrandTable.applyAccentColorToDataroomView
    var welcomeMessage by BrandTable.welcomeMessage
    var ctaLabel by BrandTable.ctaLabel
    var ctaUrl by BrandTable.ctaUrl
    var privacyPolicyUrl by BrandTable.privacyPolicyUrl
    var cardLayout by BrandTable.cardLayout
    var showFolderTree by BrandTable.showFolderTree
    var viewerLayoutPreset by BrandTable.viewerLayoutPreset
    var viewerHeaderStyle by BrandTable.viewerHeaderStyle
    var hideFolderIconsInMain by BrandTable.hideFolderIconsInMain
    var customLinkPreviewEnabled by BrandTable.customLinkPreviewEnabled
    var linkPreviewTitle by BrandTable.linkPreviewTitle
    var linkPreviewDescription by BrandTable.linkPreviewDescription
    var linkPreviewImage by BrandTable.linkPreviewImage
    var linkPreviewFavicon by BrandTable.linkPreviewFavicon
    var defaultLanguage by BrandTable.defaultLanguage
    var name by BrandTable.name
    var teamId by BrandTable.teamId
    var team by TeamEntity referencedOn BrandTable.teamId
    val defaultForTeam by TeamEntity optionalReferrersOn TeamTable.defaultBrandId
    val links by LinkEntity optionalReferrersOn LinkTable.brandId
    val datarooms by DataroomEntity optionalReferrersOn DataroomTable.brandId
    var createdAt by BrandTable.createdAt
    var updatedAt by BrandTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BrandTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
