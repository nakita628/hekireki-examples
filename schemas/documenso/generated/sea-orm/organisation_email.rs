use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganisationEmail")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(unique)]
    pub email: String,
    #[sea_orm(column_name = "emailName")]
    pub email_name: String,
    #[sea_orm(column_name = "emailDomainId")]
    pub email_domain_id: String,
    #[sea_orm(column_name = "organisationId")]
    pub organisation_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::email_domain::Entity",
        from = "Column::EmailDomainId",
        to = "super::email_domain::Column::Id",
        on_delete = "Cascade"
    )]
    EmailDomain,
    #[sea_orm(
        belongs_to = "super::organisation::Entity",
        from = "Column::OrganisationId",
        to = "super::organisation::Column::Id",
        on_delete = "Cascade"
    )]
    Organisation,
    #[sea_orm(has_many = "super::organisation_global_settings::Entity")]
    OrganisationGlobalSettings,
    #[sea_orm(has_many = "super::team_global_settings::Entity")]
    TeamGlobalSettings,
}

impl Related<super::email_domain::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailDomain.def()
    }
}

impl Related<super::organisation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organisation.def()
    }
}

impl Related<super::organisation_global_settings::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganisationGlobalSettings.def()
    }
}

impl Related<super::team_global_settings::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TeamGlobalSettings.def()
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
