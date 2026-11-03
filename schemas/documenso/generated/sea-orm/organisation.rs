use super::organisation_type::OrganisationType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Organisation")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub r#type: OrganisationType,
    pub name: String,
    #[sea_orm(unique)]
    pub url: String,
    #[sea_orm(column_name = "avatarImageId")]
    pub avatar_image_id: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "customerId")]
    pub customer_id: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "organisationClaimId")]
    pub organisation_claim_id: String,
    #[sea_orm(column_name = "ownerUserId")]
    pub owner_user_id: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "organisationGlobalSettingsId")]
    pub organisation_global_settings_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "organisationAuthenticationPortalId")]
    pub organisation_authentication_portal_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organisation_claim::Entity",
        from = "Column::OrganisationClaimId",
        to = "super::organisation_claim::Column::Id"
    )]
    OrganisationClaim,
    #[sea_orm(
        belongs_to = "super::avatar_image::Entity",
        from = "Column::AvatarImageId",
        to = "super::avatar_image::Column::Id",
        on_delete = "SetNull"
    )]
    AvatarImage,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::OwnerUserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    Owner,
    #[sea_orm(
        belongs_to = "super::organisation_global_settings::Entity",
        from = "Column::OrganisationGlobalSettingsId",
        to = "super::organisation_global_settings::Column::Id"
    )]
    OrganisationGlobalSettings,
    #[sea_orm(
        belongs_to = "super::organisation_authentication_portal::Entity",
        from = "Column::OrganisationAuthenticationPortalId",
        to = "super::organisation_authentication_portal::Column::Id"
    )]
    OrganisationAuthenticationPortal,
    #[sea_orm(has_many = "super::organisation_member::Entity")]
    Members,
    #[sea_orm(has_many = "super::organisation_member_invite::Entity")]
    Invites,
    #[sea_orm(has_many = "super::organisation_group::Entity")]
    Groups,
    #[sea_orm(has_many = "super::team::Entity")]
    Teams,
    #[sea_orm(has_many = "super::email_domain::Entity")]
    EmailDomains,
    #[sea_orm(has_many = "super::organisation_email::Entity")]
    OrganisationEmails,
    #[sea_orm(has_many = "super::organisation_monthly_stat::Entity")]
    MonthlyStats,
    #[sea_orm(has_many = "super::embed_signature::Entity")]
    EmbedSignatures,
    #[sea_orm(has_one = "super::subscription::Entity")]
    Subscription,
}

impl Related<super::organisation_claim::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganisationClaim.def()
    }
}

impl Related<super::avatar_image::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AvatarImage.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Owner.def()
    }
}

impl Related<super::organisation_global_settings::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganisationGlobalSettings.def()
    }
}

impl Related<super::organisation_authentication_portal::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganisationAuthenticationPortal.def()
    }
}

impl Related<super::organisation_member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

impl Related<super::organisation_member_invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invites.def()
    }
}

impl Related<super::organisation_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Groups.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Teams.def()
    }
}

impl Related<super::email_domain::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailDomains.def()
    }
}

impl Related<super::organisation_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganisationEmails.def()
    }
}

impl Related<super::organisation_monthly_stat::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MonthlyStats.def()
    }
}

impl Related<super::embed_signature::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmbedSignatures.def()
    }
}

impl Related<super::subscription::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Subscription.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
