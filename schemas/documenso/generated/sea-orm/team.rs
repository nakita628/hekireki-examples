use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Team")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub name: String,
    #[sea_orm(unique)]
    pub url: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "avatarImageId")]
    pub avatar_image_id: Option<String>,
    #[sea_orm(column_name = "organisationId")]
    pub organisation_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "teamGlobalSettingsId")]
    pub team_global_settings_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::avatar_image::Entity",
        from = "Column::AvatarImageId",
        to = "super::avatar_image::Column::Id",
        on_delete = "SetNull"
    )]
    AvatarImage,
    #[sea_orm(
        belongs_to = "super::organisation::Entity",
        from = "Column::OrganisationId",
        to = "super::organisation::Column::Id",
        on_delete = "Cascade"
    )]
    Organisation,
    #[sea_orm(
        belongs_to = "super::team_global_settings::Entity",
        from = "Column::TeamGlobalSettingsId",
        to = "super::team_global_settings::Column::Id",
        on_delete = "Cascade"
    )]
    TeamGlobalSettings,
    #[sea_orm(has_many = "super::envelope::Entity")]
    Envelopes,
    #[sea_orm(has_many = "super::folder::Entity")]
    Folders,
    #[sea_orm(has_many = "super::api_token::Entity")]
    ApiTokens,
    #[sea_orm(has_many = "super::webhook::Entity")]
    Webhooks,
    #[sea_orm(has_many = "super::team_group::Entity")]
    TeamGroups,
    #[sea_orm(has_one = "super::team_email::Entity")]
    TeamEmail,
    #[sea_orm(has_one = "super::team_email_verification::Entity")]
    EmailVerification,
    #[sea_orm(has_one = "super::team_profile::Entity")]
    Profile,
}

impl Related<super::avatar_image::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AvatarImage.def()
    }
}

impl Related<super::organisation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organisation.def()
    }
}

impl Related<super::team_global_settings::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TeamGlobalSettings.def()
    }
}

impl Related<super::envelope::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Envelopes.def()
    }
}

impl Related<super::folder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Folders.def()
    }
}

impl Related<super::api_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiTokens.def()
    }
}

impl Related<super::webhook::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Webhooks.def()
    }
}

impl Related<super::team_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TeamGroups.def()
    }
}

impl Related<super::team_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TeamEmail.def()
    }
}

impl Related<super::team_email_verification::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailVerification.def()
    }
}

impl Related<super::team_profile::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Profile.def()
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
        Ok(self)
    }
}
